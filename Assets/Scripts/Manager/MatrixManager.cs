using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using devziie.Inputs;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using System.Linq;
using TMPro;
using UnityEngine.PlayerLoop;

public class MatrixManager : MonoBehaviour
{
    #region Events
    public static event Action OnTriggerToReal; //From matrix to Real
    public static event Action OnTriggerToMatrix; //From real to Matrix (include a transition)
    public static event Action OnStartReversePlayRecord; //From real to Matrix (include a transition)
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
    [ShowInInspector, ReadOnly] private bool _isMatrixRecordingPlaying;

    public float maximumTimeInMatrix;
    private float _currentTimerInMatrix;
    private float _currentTimerInTransitionFromRealToMatrix;
    
    public List<IEnumerator> reversePlay = new List<IEnumerator>();

    public TextMeshProUGUI display;
    
    public int timeToTransition;
    
    #endregion
    
    #region MonoBehavior

    private void Start()
    {
        OnTriggerToReal?.Invoke();
        UpdateMatrixEntitiesList();
    }
    
    public void Update()
    {
        if(display!=null) display.text = worldState.ToString();
    }

    #endregion
    
    #region Input

    public void OnEnable()
    {
        InputManager.Controls.Player.ToggleBackEnd.started += OnToggleBackEnd;
    }
    public void OnDisable()
    {
        InputManager.Controls.Player.ToggleBackEnd.started -= OnToggleBackEnd;
    }
    
    //Player Click on Toggle Matrix
    private void OnToggleBackEnd(InputAction.CallbackContext context)
    {
        switch (worldState)
        {
            //From Real To Matrix
            case WorldState.Real:
                
                if (_isMatrixRecordingPlaying)
                {
                    SoundEvents.onCannotSwitchToMatrix?.Invoke();
                    return;
                }
                
                UpdateMatrixEntitiesList();
                StartRecordingAllMatrixEntities();
                OnTriggerToMatrix?.Invoke();
                worldState = WorldState.Matrix;
                break;
            
            case WorldState.Matrix:
                StopRecording();
                OnTriggerToReal?.Invoke();
                worldState = WorldState.TransitioningToReal;
                StartCoroutine(CoTransitionFromMatrixToReal());
                break;
            
            case WorldState.TransitioningToReal:
                
                break;
         
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion

    #region MatrixEntities in Scene
   
    [Button]  //Put all the matrix objet that can be rolleable in a list
    private void UpdateMatrixEntitiesList()
    {
        _matrixEntities.Clear();
        foreach (MatrixEntityBehavior matrixEntity in FindObjectsOfType<MatrixEntityBehavior>())  //Maybe needs optimisations//
        {
            _matrixEntities.Add(matrixEntity);
        }
    }

    #endregion
    
    #region Recording Fonctions

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
            while (true) {
                matrixEntity.recordedMatrixInfo.Enqueue(new MatrixInfo(matrixEntity.transform.position, matrixEntity.transform.rotation));
                yield return new WaitForEndOfFrame();
            }
        }
    
    
    #endregion
    
    #region ReverseRecord
    private IEnumerator CoReverseRecord()
    {
        int allFrames = _matrixEntities[0].recordedMatrixInfo.Count-1;
        int framesToTransition = timeToTransition * Screen.currentResolution.refreshRate;
        int dropValue =  allFrames/ framesToTransition;

        float timer = 0;
        print("Drop value : " + dropValue);

        for (int i = _matrixEntities[0].recordedMatrixInfo.Count-1; i > 0; i-=dropValue)
        {
            foreach (var matrixEntity in _matrixEntities) 
            {
                List<MatrixInfo> info = matrixEntity.recordedMatrixInfo.ToList();
                UpdateMatrixEntity(matrixEntity, info, i);
            }
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }     

        foreach (MatrixEntityBehavior matrixEntity in _matrixEntities)
        {
            UpdateMatrixEntity(matrixEntity, matrixEntity.recordedMatrixInfo.ToList(), 0);
        }
        
        print("Transition took : " + timer);
    }
    IEnumerator CoTransitionFromMatrixToReal()
    {
        //No Matrix Element in the scene; nothing to transition
        if (_matrixEntities.Count <= 0)
        {
            worldState = WorldState.Real;
            yield break;
        }

        OnStartReversePlayRecord?.Invoke();
        yield return CoReverseRecord();

        print("All Done");
        
        worldState = WorldState.Real;
        
       
        foreach (MatrixEntityBehavior matrixEntity in _matrixEntities)
        {
            PlayRecordingQueue(matrixEntity);
        }
        
    }

    #endregion

    #region PlayRecord
    
    [Button]
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
