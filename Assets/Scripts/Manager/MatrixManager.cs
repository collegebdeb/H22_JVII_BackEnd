using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using devziie.Inputs;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class MatrixManager : MonoBehaviour
{

    private List<MatrixEntityBehavior> MatrixEntities;
    public static event Action OnTriggerToReal;
    public static event Action OnTriggerToMatrix;

    public enum WorldState
    {
        Real,
        TransitioningToReal,
        Matrix
    }

    [ShowInInspector, ReadOnly] private WorldState _worldState;
    [ShowInInspector, ReadOnly] private bool _isMatrixRecordingPlaying;

    public float maximumTimeInMatrix;
    public float transitionTimeFromRealToMatrix;
    private float _currentTimerInMatrix;
    private float _currentTimerInTransitionFromRealToMatrix;
    
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
        switch (_worldState)
        {
            //From Real To Matrix
            case WorldState.Real:
                
                if (_isMatrixRecordingPlaying)
                {
                    SoundEvents.onCannotSwitchToMatrix?.Invoke();
                    return;
                }
                
                OnTriggerToMatrix?.Invoke();
                _worldState = WorldState.Matrix;
                break;
            
            case WorldState.Matrix:
                OnTriggerToReal?.Invoke();
                _worldState = WorldState.Matrix;
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
        yield return new WaitForEndOfFrame();
    }

    //Maybe needs optimisations//
    [Button]
    private void UpdateMatrixEntitiesList()
    {
        foreach (MatrixEntityBehavior matrixEntity in  FindObjectsOfType<MatrixEntityBehavior>())
        {
            print("matrixxx");
        }
    }
    

    private void Start()
    {
        UpdateMatrixEntitiesList();
    }

    #region Recording Fonctions

    private IEnumerator CoPlayRecording(MatrixEntityBehavior matrixEntity)
    {
        // MatrixEntity t = recordedMatrixEntity.Dequeue();
        while (matrixEntity.recordedMatrixInfo.Count > 0)
        {
            MatrixInfo recorded = matrixEntity.recordedMatrixInfo.Dequeue();
            matrixEntity.transform.position = recorded.MatrixPosition;
            matrixEntity.transform.rotation = recorded.MatrixRotation;
            yield return new WaitForEndOfFrame();
        }

    }

    private IEnumerator CoStartRecording(MatrixEntityBehavior matrixEntity)
    {
        while (true) {
            matrixEntity.recordedMatrixInfo.Enqueue(new MatrixInfo(matrixEntity.transform.position, matrixEntity.transform.rotation));
            yield return new WaitForEndOfFrame();
        }
    }

    [Button]
    public void StartRecording(MatrixEntityBehavior matrixEntity)
    {
        StartCoroutine(CoStartRecording(matrixEntity));
    }

    [Button]
    public void StopRecording()
    {
        StopAllCoroutines();
    }
    
    [Button]
    public void PlayRecording(MatrixEntityBehavior matrixEntity)
    {
        StartCoroutine(CoPlayRecording(matrixEntity));
    }

    #endregion
 

    

   
}
