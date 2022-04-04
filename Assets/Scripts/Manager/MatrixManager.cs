using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using devziie.Inputs;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using TMPro;

public class MatrixManager : MonoBehaviour
{
    #region Events
    public static event Action OnRealWorldActivated; //From matrix to Real
    public static event Action OnMatrixActivated; //From real to Matrix (include a transition)
    public static event Action OnTransitionActivated; //From Matrix To Transition

    public static event Action<float> OnUpdateReverseValue; //EveryTime a new reverse record value is played
    
    #endregion
    
    #region Variables
    
    [ShowInInspector] private List<MatrixEntityBehavior> _matrixEntities = new List<MatrixEntityBehavior>();
    public enum WorldState
    {
        Real,
        TransitioningToReal,
        Matrix
    }
    [ShowInInspector, ReadOnly] public static WorldState worldState;
    
    [SerializeField, ReadOnly] private bool isMatrixPlaying;
    [SerializeField, ReadOnly] private bool recordingAllowed;

    public List<IEnumerator> reversePlay = new List<IEnumerator>();

    public TextMeshProUGUI display;

    #endregion
    
    [SerializeField, ShowInInspector] private PlayerControl playerCtrl;
    
    #region MonoBehavior

    private void Start()
    {
        playerCtrl = new PlayerControl(GameManager.i.playerReal, GameManager.i.playerMatrix);
        playerCtrl.SetCurrentPlayer(playerCtrl.realPlayer);
        //UpdateMatrixEntitiesList();
    }

    public void Update()
    {
        if(display!=null) display.text = worldState.ToString();
    }

    #endregion
    
    #region Input and Transition

    public void OnEnable()
    {
        InputManager.Controls.Player.ToggleBackEnd.started += OnToggleBackEnd;
        MatrixEntityBehavior.OnRegisterMatrixEntity += RegisterMatrixEntity;
    }
    public void OnDisable()
    {
        InputManager.Controls.Player.ToggleBackEnd.started -= OnToggleBackEnd;
        MatrixEntityBehavior.OnRemoveMatrixEntity += UnRegisterMatrixEntity;
    }
    
    //Player Click on Toggle Matrix
    private void OnToggleBackEnd(InputAction.CallbackContext context)
    {
        switch (worldState)
        {
            //FROM REAL WORLD TO MATRIX
            case WorldState.Real:
                
                //Cant go inside matrix if recording is playing
                if (isMatrixPlaying)
                {
                    SoundEvents.onCannotSwitchToMatrix?.Invoke(AudioList.Sound.Unknown, gameObject);
                    return;
                }

                recordingAllowed = true;
                worldState = WorldState.Matrix;
                OnMatrixActivated?.Invoke();
                //UpdateMatrixEntitiesList(); //Find all Matrix Entities on Scene
                SoundEvents.onSwitchToMatrix?.Invoke(AudioList.Sound.Unknown, gameObject);
                StartRecordingAllMatrixEntities(); //Start recording

                break;
            
            //FROM MATRIX TO TRANSITION
            case WorldState.Matrix:
                TransitionMatrixToTransition();
                break;
            
            case WorldState.TransitioningToReal:
                
                break;
         
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void TransitionMatrixToTransition()
    {
        worldState = WorldState.TransitioningToReal;
        StopRecording();
        StartCoroutine(CoTransitionFromMatrixToReal());
    }

    #endregion
    
    #region MatrixEntities in Scene

    private void RegisterMatrixEntity(MatrixEntityBehavior matrixEntity)
    {
        _matrixEntities.Add(matrixEntity);
    }
    
    private void UnRegisterMatrixEntity(MatrixEntityBehavior matrixEntity)
    {
        _matrixEntities.Remove(matrixEntity);
    }
    
    /**
    [Button]  //Put all the matrix objet that can be rolleable in a list
    private void UpdateMatrixEntitiesList()
    {
        _matrixEntities.Clear();
        foreach (MatrixEntityBehavior matrixEntity in FindObjectsOfType<MatrixEntityBehavior>())  //Maybe needs optimisations//
        {
            _matrixEntities.Add(matrixEntity);
        }
    }
    **/

    #endregion
    
    #region Rollback Fonctions

    #region UpdateMatrixEntity

    private void UpdateMatrixQueueEntity(MatrixEntityBehavior matrixEntity)
    {
        MatrixInfo recorded = matrixEntity.recordedMatrixInfo.Dequeue();
        matrixEntity.transform.position = recorded.MatrixPosition;
        matrixEntity.transform.rotation = recorded.MatrixRotation;
    }
    private void UpdateMatrixEntity(MatrixEntityBehavior matrixEntity, List<MatrixInfo> info, int index)
    {
        matrixEntity.transform.position = info[index].MatrixPosition;
        matrixEntity.transform.rotation = info[index].MatrixRotation;
    }

    #endregion
    
    #region Record
    
        [Button]
        public void StartRecordingAllMatrixEntities()
        {
            StartCoroutine(CoStartMatrixTimer());
            
            foreach (MatrixEntityBehavior matrixEntity in _matrixEntities)
            {
                StartRecording(matrixEntity);
            }
        }
        public void StartRecording(MatrixEntityBehavior matrixEntity)
        {
            matrixEntity.state = MatrixEntityBehavior.MatrixState.Recording;
            StartCoroutine(CoStartRecording(matrixEntity));
        }
        private IEnumerator CoStartRecording(MatrixEntityBehavior matrixEntity)
        {
            while (recordingAllowed) {
                matrixEntity.recordedMatrixInfo.Enqueue(new MatrixInfo(matrixEntity.transform.position, matrixEntity.transform.rotation));
                yield return new WaitForEndOfFrame();
            }
            
        }
        IEnumerator CoStartMatrixTimer()
        {
            GameManager.i.currentTimeInMatrix = 0;

            while (recordingAllowed)
            {
                GameManager.i.currentTimeInMatrix += Time.deltaTime;
           
                if (GameManager.i.currentTimeInMatrix >= GameManager.i.maximumTimeInMatrix)
                {
                    recordingAllowed = false;
                    TransitionMatrixToTransition();
                    yield break;
                }
               
                yield return new WaitForEndOfFrame();
            }
        
        
       
        }
    
    
    #endregion

    #region ReverseRecord

    public AnimationCurve transitionCurve;
    public float blendTime;
    
    private IEnumerator CoReverseRecord()
    {
        float allFrames = _matrixEntities[0].recordedMatrixInfo.Count-1;
        float valueCurrentFrame = 0;
        float valueCurrentCurvedFrame = 0;
        float rate = allFrames / blendTime;

        while (valueCurrentFrame <= allFrames)
        {
            foreach (var matrixEntity in _matrixEntities)
            {
                List<MatrixInfo> info = matrixEntity.recordedMatrixInfo.ToList();
                UpdateMatrixEntity(matrixEntity, info, (int) (allFrames - valueCurrentCurvedFrame));
            }
            
            valueCurrentFrame += Time.deltaTime * rate;
            valueCurrentCurvedFrame = valueCurrentFrame * transitionCurve.Evaluate(valueCurrentFrame / allFrames);
            OnUpdateReverseValue?.Invoke(valueCurrentCurvedFrame / allFrames);
            yield return new WaitForEndOfFrame();
        }
        
        foreach (MatrixEntityBehavior matrixEntity in _matrixEntities)
        {
            UpdateMatrixEntity(matrixEntity, matrixEntity.recordedMatrixInfo.ToList(), 0);
        }
    }
    
    //FROM TRANSITION TO REAL
    IEnumerator CoTransitionFromMatrixToReal()
    {
        //No Matrix Element in the scene; nothing to transition
        if (_matrixEntities.Count <= 0)
        {
            worldState = WorldState.Real;
            yield break;
        }

        playerCtrl.LockPlayerControl();
        OnTransitionActivated?.Invoke();
        
        yield return CoReverseRecord();
        
        playerCtrl.UnlockPlayerControl();
        OnRealWorldActivated?.Invoke();
        worldState = WorldState.Real;
        isMatrixPlaying = true;
        
        foreach (MatrixEntityBehavior matrixEntity in _matrixEntities)
        {
            PlayRecordingQueue(matrixEntity);
        }
        
    }

    #endregion

    #region PlayRecord
    
    public void PlayRecordingQueue(MatrixEntityBehavior matrixEntity)
    {
        StartCoroutine(CoPlayRecordingQueue(matrixEntity));
    }
    
    private IEnumerator CoPlayRecordingQueue(MatrixEntityBehavior matrixEntity)
    {
        while (matrixEntity.recordedMatrixInfo.Count > 0)
        {
            UpdateMatrixQueueEntity(matrixEntity);
            yield return new WaitForEndOfFrame();
        }

        //Can be put twice to false in the space of two frames but shouldnt be a problem
        isMatrixPlaying = false;
    }
    
    #endregion
    
    #region StopRecord
    [Button]
    public void StopRecording()
    {
        StopAllCoroutines();
    }
   
    #endregion
    
    #endregion

}
