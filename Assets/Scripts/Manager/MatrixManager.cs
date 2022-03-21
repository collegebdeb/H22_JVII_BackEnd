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

    [ShowInInspector] private List<MatrixEntityBehavior> _matrixEntities = new List<MatrixEntityBehavior>();
    
    
    public static event Action OnTriggerToReal; //From matrix to Real
    public static event Action OnTriggerToMatrix; //From real to Matrix (include a transition)
    public static event Action OnStartReversePlayRecord; //From real to Matrix (include a transition)

    public enum WorldState
    {
        Real,
        TransitioningToReal,
        Matrix
    }

    [ShowInInspector, ReadOnly] public static WorldState worldState;
    [ShowInInspector, ReadOnly] private bool _isMatrixRecordingPlaying;

    public float maximumTimeInMatrix;
    public float transitionTimeFromRealToMatrix;
    private float _currentTimerInMatrix;
    private float _currentTimerInTransitionFromRealToMatrix;

    
    public List<IEnumerator> reversePlay = new List<IEnumerator>();

    public TextMeshProUGUI display;
    public void Update()
    {
        if(display!=null) display.text = worldState.ToString();
        print(Screen.currentResolution.refreshRate);

    }

    #region Input

    public void OnEnable()
    {
        InputManager.Controls.Player.ToggleBackEnd.started += OnToggleBackEnd;
    }
    
    public void OnDisable()
    {
        InputManager.Controls.Player.ToggleBackEnd.started -= OnToggleBackEnd;
    }

    #endregion
    
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
      
    }

    public int matrixReverseSpeed = 15;

    public int timeToTransition;

    

    private IEnumerator CoReverseRecord()
    {
        int allFrames = _matrixEntities[0].recordedMatrixInfo.Count-1;
        int framesToTransition = timeToTransition * Screen.currentResolution.refreshRate;
        int dropValue = allFrames / framesToTransition;

     float timer = 0;
        print(dropValue);

        for (int i = _matrixEntities[0].recordedMatrixInfo.Count-1; i > 0; i=i-dropValue)
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
        
        print(timer);
    }

    //Put all the matrix objet that can be rollable in a list
    [Button]
    private void UpdateMatrixEntitiesList()
    {
        foreach (MatrixEntityBehavior matrixEntity in FindObjectsOfType<MatrixEntityBehavior>())  //Maybe needs optimisations//
        {
            _matrixEntities.Add(matrixEntity);
        }
    }
    private void Start()
    {
        OnTriggerToReal?.Invoke();
        UpdateMatrixEntitiesList();
    }
    #region Recording Fonctions
    private void UpdateMatrixQueueEntity(MatrixEntityBehavior matrixEntity)
    {
        //print(matrixEntity.recordedMatrixInfo.Count);
        MatrixInfo recorded = matrixEntity.recordedMatrixInfo.Dequeue();
        matrixEntity.transform.position = recorded.MatrixPosition;
        matrixEntity.transform.rotation = recorded.MatrixRotation;
    }
    private void UpdateMatrixEntity(MatrixEntityBehavior matrixEntity, List<MatrixInfo> info, int index)
    {
        matrixEntity.transform.position = info[index].MatrixPosition;
        matrixEntity.transform.rotation = info[index].MatrixRotation;
    }
    private IEnumerator CoUseRecording(MatrixEntityBehavior matrixEntity)
    {
        // MatrixEntity t = recordedMatrixEntity.Dequeue();
        while (matrixEntity.recordedMatrixInfo.Count > 0)
        {
            UpdateMatrixQueueEntity(matrixEntity);
            yield return new WaitForEndOfFrame();
        }
    }
    public float intervalReverseTime;
    #region ReverseRecord
    [Button]
    public void PlayReverseRecording(MatrixEntityBehavior matrixEntity)
    {
        matrixEntity.state = MatrixEntityBehavior.MatrixState.ReversePlaying;
        StartCoroutine(CoPlayReverseRecording(matrixEntity));
    }
    
    private IEnumerator CoPlayReverseRecording(MatrixEntityBehavior matrixEntity)
    {
        List<MatrixInfo> info = matrixEntity.recordedMatrixInfo.ToList();
        
        for (int i = matrixEntity.recordedMatrixInfo.Count-1; i > 0; i--)
        {
            UpdateMatrixEntity(matrixEntity, info, i);
            //yield return new WaitForSeconds(intervalReverseTime/1000f);
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion
    

    private IEnumerator CoStartRecording(MatrixEntityBehavior matrixEntity)
    {
        while (true) {
            matrixEntity.recordedMatrixInfo.Enqueue(new MatrixInfo(matrixEntity.transform.position, matrixEntity.transform.rotation));
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartRecordingAllMatrixEntities()
    {
        foreach (MatrixEntityBehavior matrixEntity in _matrixEntities)
        {
            StartRecording(matrixEntity);
        }
    }
    
    [Button]
    public void StartRecording(MatrixEntityBehavior matrixEntity)
    {
        matrixEntity.state = MatrixEntityBehavior.MatrixState.Recording;
        StartCoroutine(CoStartRecording(matrixEntity));
    }

    [Button]
    public void StopRecording()
    {
       StopAllCoroutines();
    }
    
    [Button]
    public void UseRecording(MatrixEntityBehavior matrixEntity)
    {
        StartCoroutine(CoUseRecording(matrixEntity));
    }
   

    #endregion
 

    

   
}
