using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;


public class MatrixEntityBehavior : MonoBehaviour
{
    private void Start()
    {
        Interactable inter;
        transform.parent.gameObject.TryGetComponent(out inter);
        if (inter == null)
        {
            //Debug.LogError("Un objet matrix doit être enfant de Interactables. Erreur sur " + gameObject.name);
        }
       
    }

    [ShowInInspector]
    public Queue<MatrixInfo> recordedMatrixInfo = new Queue<MatrixInfo>();
    
    public enum MatrixState {Normal, ReversePlaying, Recording, Playing}

    [ShowInInspector, ReadOnly]
    public MatrixState state;


}