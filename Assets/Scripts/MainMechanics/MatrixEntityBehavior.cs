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
    public Vector3 QuickSavePosition;

    public bool AllowProjectileDeath;
    public Projectile projectile;

    private void OnEnable()
    {
       
        LevelManager.OnFinishedLevelSubmerge += RegisterSelfPosition;
        Player.OnQuickSave += RegisterQuickSave;
        RegisterSelfPosition();
        QuickSavePosition = OriginalPosition;
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
    
    public void RegisterQuickSave()
    {
        QuickSavePosition = transform.position;
    }
    public void ReloadQuickSavePositionToPosition()
    {
        if (!enabled) return;
        transform.position = QuickSavePosition;
        OnMatrixEntityReload?.Invoke();
        transform.position = QuickSavePosition;
    }

    public void SetFakeLife(bool aliveState)
    {
        if (aliveState)
        {
            projectile.Destroy();
        }
        else
        {
            projectile.Alive();
        }
    }


}