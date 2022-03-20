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
        if (transform.parent.gameObject.GetComponent<Interactables>()==null)
        {
            //Debug.LogError("Un objet matrix doit être enfant de Interactables. Erreur sur " + gameObject.name);
        }
       
    }

    [ShowInInspector]
    public Queue<MatrixInfo> recordedMatrixInfo = new Queue<MatrixInfo>();
    
    
    
}