using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using devziie.Inputs;
using Newtonsoft.Json;
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
    
    [ShowInInspector] public List<MatrixEntityBehavior> _matrixEntities = new List<MatrixEntityBehavior>();
    public enum WorldState
    {
        Real,
        TransitioningToReal,
        Matrix
    }
    [ShowInInspector, ReadOnly] public static WorldState worldState;
    
    [SerializeField, ReadOnly] public static bool isMatrixPlaying;
    [SerializeField, ReadOnly] private bool recordingAllowed;

    public List<IEnumerator> reversePlay = new List<IEnumerator>();

    public TextMeshProUGUI display;

    public bool fastForward = false;

    #endregion
    
    [SerializeField, ShowInInspector] private PlayerControl playerCtrl;
    
    #region MonoBehavior

    private void Start()
    {
        playerCtrl = new PlayerControl(GameManager.i.playerReal, GameManager.i.playerMatrix);
        playerCtrl.SetRealPlayer();
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
        //InputManager.Controls.Player.FastForward.started += context => OnFastForward();
        InputManager.Controls.Player.FastForward.performed += OnFastForward;
        InputManager.Controls.Player.FastForward.canceled += OnFastForward;
        MatrixEntityBehavior.OnRegisterMatrixEntity += RegisterMatrixEntity;
    }
    public void OnDisable()
    {
        InputManager.Controls.Player.ToggleBackEnd.started -= OnToggleBackEnd;
        MatrixEntityBehavior.OnRemoveMatrixEntity -= UnRegisterMatrixEntity;
    }

    private void OnFastForward(InputAction.CallbackContext ctx)
    {
        if (worldState == WorldState.Matrix || worldState == WorldState.TransitioningToReal) return;

        FastForward(ctx.ReadValueAsButton());
    }

    private void FastForward(bool forwarding)
    {
        fastForward = forwarding;
    }

    private void DisableFastForward()
    {
        
    }
    
    //Player Click on Toggle Matrix
    private void OnToggleBackEnd(InputAction.CallbackContext context)
    {
        switch (worldState)
        {
            //FROM REAL WORLD TO MATRIX
            case WorldState.Real:

                //Cant go inside matrix if recording is playing
                if (isMatrixPlaying || GameManager.i.playerReal.movement.connectedToPlatform ||  !GameManager.i.playerReal.movement.IsGrounded)
                {
                    if (DialogManager.dialogPlaying)
                    {
                        return;
                    }
                    if (GameManager.i.playerReal.movement.connectedToPlatform)
                    {
                        uiLogsEffect.Send?.Invoke("Error - Can't toggle BackEnd");
                        Dialog dialog =
                            new Dialog(
                                "Mmmhhh.. il semble etre impossible de rentrer dans la matrice en etant sur une boite.");
                        dialog.AddInQueue();
                    }

                    if (isMatrixPlaying)
                    {
                        uiLogsEffect.Send?.Invoke("Error - Matrix is playing");
                        Dialog dialog =
                            new Dialog(
                                "Mmmhhh.. l'enregistrement est en train d'etre jouer. Il est mieu d'attendre avant de l'activé pour ne pas causer de problème ");
                        dialog.AddInQueue();
                    }

                    if (!GameManager.i.playerReal.movement.IsGrounded)
                    {
                        uiLogsEffect.Send?.Invoke("Error - playing is the air");
                        Dialog dialog =
                            new Dialog(
                                "Espece de fou.. n'essaye jamais de glitcher le jeux en etant dans les airs... ");
                        dialog.AddInQueue();
                    }

                    //SoundEvents.onCannotSwitchToMatrix?.Invoke(AudioList.Sound.OnCannotSwitchToMatrix, gameObject);
                    return;
                }
                playerCtrl.ChangePlayerToMatrixState();
                recordingAllowed = true;
                worldState = WorldState.Matrix;
                OnMatrixActivated?.Invoke();
                uiLogsEffect.Send?.Invoke("BackEnd activated ... ");
                //UpdateMatrixEntitiesList(); //Find all Matrix Entities on Scene
                //SoundEvents.OnMatrixActivated?.Invoke(AudioList.Sound.OnMatrixActivated, gameObject);
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
        uiLogsEffect.Send?.Invoke("Going back to real world ... ");
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
        OnUpdatePlayValue?.Invoke(1-matrixEntity.recordedMatrixInfo.Count/ totalFrameRecorded);
        print(1-matrixEntity.recordedMatrixInfo.Count/ totalFrameRecorded);
        MatrixInfo recorded = matrixEntity.recordedMatrixInfo.Dequeue();
        
        
        
        matrixEntity.transform.position = recorded.MatrixPosition;
        matrixEntity.transform.rotation = recorded.MatrixRotation;
        if (matrixEntity.AllowProjectileDeath)
        {
            matrixEntity.SetFakeLife(!recorded.Alive);
        }

        if (fastForward)
        {
            if (matrixEntity.recordedMatrixInfo.Count > 4)
            {
                matrixEntity.recordedMatrixInfo.Dequeue();
                matrixEntity.recordedMatrixInfo.Dequeue();
            }
        }
       
        
       // if(currentFastForward)
        
    }
    private void UpdateMatrixEntity(MatrixEntityBehavior matrixEntity, List<MatrixInfo> info, int index)
    {
        try
        {
            matrixEntity.transform.position = info[index].MatrixPosition;
            matrixEntity.transform.rotation = info[index].MatrixRotation;
        }
        catch
        {
            // ignored
        }
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
                if (matrixEntity.AllowProjectileDeath)
                {
                    matrixEntity.recordedMatrixInfo.Enqueue(new MatrixInfo(matrixEntity.transform.position, matrixEntity.transform.rotation, matrixEntity.projectile.alive));
                }
                else
                {
                    matrixEntity.recordedMatrixInfo.Enqueue(new MatrixInfo(matrixEntity.transform.position, matrixEntity.transform.rotation, true));

                }
                
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

    private float recordedTime;
    
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
        //SoundEvents.OnTransitionActivated?.Invoke(AudioList.Sound.OnTransitionActivated, gameObject);
        
        yield return CoReverseRecord();
        
        playerCtrl.UnlockPlayerControl();
        OnRealWorldActivated?.Invoke();
        //SoundEvents.OnRealWorldActivated?.Invoke(AudioList.Sound.OnRealWorldActivated, gameObject);
        worldState = WorldState.Real;
        
        
        //yield return new WaitForSeconds(3f);
        uiLogsEffect.Send?.Invoke("Back to real world ");
        uiLogsEffect.Send?.Invoke("Playing recording ... ");
        foreach (MatrixEntityBehavior matrixEntity in _matrixEntities)
        {
            PlayRecordingQueue(matrixEntity);
        }
        
        isMatrixPlaying = true;
        
    }

    #endregion

    #region PlayRecord
    
    public void PlayRecordingQueue(MatrixEntityBehavior matrixEntity)
    {
        
        StartCoroutine(CoPlayRecordingQueue(matrixEntity));
    }

    private float totalFrameRecorded;
    public static event Action<float> OnUpdatePlayValue;
    
    private IEnumerator CoPlayRecordingQueue(MatrixEntityBehavior matrixEntity)
    {
        
        totalFrameRecorded = matrixEntity.recordedMatrixInfo.Count;

        while (matrixEntity.recordedMatrixInfo.Count > 0)
        {
            if (matrixEntity.CompareTag("Player"))
            {
                matrixEntity.recordedMatrixInfo.Clear();
                yield break;

            }
            UpdateMatrixQueueEntity(matrixEntity);
            yield return new WaitForEndOfFrame();
        }

        //Can be put twice to false in the space of two frames but shouldnt be a problem
        isMatrixPlaying = false;
        OnStopMatrixPlayingInRealWorld?.Invoke();
        playerCtrl.ChangePlayerToMatrixState();
    }
    
    public static event Action OnStopMatrixPlayingInRealWorld;
    
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
