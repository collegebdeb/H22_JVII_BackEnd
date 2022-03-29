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
    private void Start()
    {
        OnRegisterMatrixEntity?.Invoke(this);
    }

    private void OnDisable()
    {
        OnRemoveMatrixEntity?.Invoke(this);
    }

    [ShowInInspector]
    public Queue<MatrixInfo> recordedMatrixInfo = new Queue<MatrixInfo>();
    
    public enum MatrixState {Normal, ReversePlaying, Recording, Playing}

    [ShowInInspector, ReadOnly]
    public MatrixState state;


}