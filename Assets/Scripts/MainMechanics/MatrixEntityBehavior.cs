using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class MatrixEntityBehavior : MonoBehaviour
{
    public static event Action<MatrixEntityBehavior> OnRegisterMatrixEntity;
    public static event Action<MatrixEntityBehavior> OnRemoveMatrixEntity;

    public Vector3 OriginalPosition;

    private void OnEnable()
    {
        LevelManager.OnFinishedLevelSubmerge += RegisterSelfPosition;
    }
    
    

    private void Start()
    {
        OnRegisterMatrixEntity?.Invoke(this);
    }

    private void OnDisable()
    {
        OnRemoveMatrixEntity?.Invoke(this);
        LevelManager.OnFinishedLevelSubmerge -= RegisterSelfPosition;
    }

    [ShowInInspector]
    public Queue<MatrixInfo> recordedMatrixInfo = new Queue<MatrixInfo>();
    
    public enum MatrixState {Normal, ReversePlaying, Recording, Playing}

    [ShowInInspector, ReadOnly]
    public MatrixState state;

    public void RegisterSelfPosition()
    {
        OriginalPosition = transform.position;
    }
    public void ReloadSelfPosition()
    {
        transform.position = OriginalPosition;
    }


}