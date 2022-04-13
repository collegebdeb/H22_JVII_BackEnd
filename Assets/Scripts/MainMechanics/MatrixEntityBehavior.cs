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
    public static event Action OnMatrixEntityReload;

    public Vector3 OriginalPosition;

    private void OnEnable()
    {
        LevelManager.OnFinishedLevelSubmerge += RegisterSelfPosition;
        RegisterSelfPosition();
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
        if (!enabled) return;
        transform.position = OriginalPosition;
        OnMatrixEntityReload?.Invoke();
        transform.position = OriginalPosition;
    }


}