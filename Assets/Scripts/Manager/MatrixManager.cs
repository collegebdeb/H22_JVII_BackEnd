using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using devziie.Inputs;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using System.Linq;

public class MatrixManager : MonoBehaviour
{

    [ShowInInspector] private List<MatrixEntityBehavior> _matrixEntities = new List<MatrixEntityBehavior>();
    
    
    public static event Action OnTriggerToReal; //From matrix to Real
    public static event Action OnTriggerToMatrix; //From real to Matrix (include a transition)

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
        print("aaa1 : " + _matrixEntities.Count);
        if (_matrixEntities.Count <= 0) yield break;
        print("aaa2");
        
        print(_matrixEntities[0].recordedMatrixInfo.Count);
        
        foreach (MatrixEntityBehavior matrixEntity in _matrixEntities)
        {
            PlayRecording(matrixEntity);
        }
        
        
        for (int i = _matrixEntities[0].recordedMatrixInfo.Count; i >= 0; i--)
        {
            foreach (MatrixEntityBehavior matrixEntity in _matrixEntities)
            {
                //UpdateMatrixEntity(matrixEntity, i);
                //print(i);
                //yield return new WaitForEndOfFrame();
            }
        }
        worldState = WorldState.Matrix;
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

    private void UpdateMatrixEntity(MatrixEntityBehavior matrixEntity)
    {
        MatrixInfo recorded = matrixEntity.recordedMatrixInfo.Dequeue();
        matrixEntity.transform.position = recorded.MatrixPosition;
        matrixEntity.transform.rotation = recorded.MatrixRotation;
    }
    
    private void UpdateMatrixEntity(MatrixEntityBehavior matrixEntity, int index)
    {
        MatrixInfo recorded = matrixEntity.recordedMatrixInfo.ToArray()[index];
        matrixEntity.transform.position = recorded.MatrixPosition;
        matrixEntity.transform.rotation = recorded.MatrixRotation;
    }
    
    public void ReversePlayRecording()
    {
        
    }
    
    private IEnumerator CoPlayRecording(MatrixEntityBehavior matrixEntity)
    {
        // MatrixEntity t = recordedMatrixEntity.Dequeue();
        while (matrixEntity.recordedMatrixInfo.Count > 0)
        {
            UpdateMatrixEntity(matrixEntity);
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
        StartCoroutine(CoStartRecording(matrixEntity));
    }

    [Button]
    public void StopRecording()
    {
       //StopCoroutine(CoStartRecording);
    }
    
    [Button]
    public void PlayRecording(MatrixEntityBehavior matrixEntity)
    {
        StartCoroutine(CoPlayRecording(matrixEntity));
    }

    #endregion
 

    

   
}
