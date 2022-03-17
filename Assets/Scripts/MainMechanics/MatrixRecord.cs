using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class MatrixRecord : MonoBehaviour
{

    [ShowInInspector]
    public Queue<MatrixEntity> recordedMatrixEntity;


    private void Awake()
    {
        recordedMatrixEntity = new Queue<MatrixEntity>();
    }

    private IEnumerator CoPlayRecording()
    {
       // MatrixEntity t = recordedMatrixEntity.Dequeue();
       while (recordedMatrixEntity.Count > 0)
       {
           MatrixEntity recorded = recordedMatrixEntity.Dequeue();
           transform.position = recorded.MatrixPosition;
           transform.rotation = recorded.MatrixRotation;
           yield return new WaitForEndOfFrame();
       }

    }

    private IEnumerator CoStartRecording()
    {
        while (true) { 

            recordedMatrixEntity.Enqueue(new MatrixEntity(transform.position, transform.rotation));
            yield return new WaitForEndOfFrame();
        }
    }

    [Button]
    public void StartRecording()
    {
        StartCoroutine(CoStartRecording());
    }

    [Button]
    public void StopRecording() {
        StopAllCoroutines();
    }
    
    [Button]
    public void PlayRecording()
    {
        StartCoroutine(CoPlayRecording());
    }


   

}

[InlineEditor, System.Serializable]
public class MatrixEntity {

    [ShowInInspector]
    public Vector3 MatrixPosition { get; }
    
    [ShowInInspector]
    public Quaternion MatrixRotation { get; }


    public MatrixEntity(Vector3 pos, Quaternion rot) {
        MatrixPosition = pos;
        MatrixRotation = rot;
      
    }
}