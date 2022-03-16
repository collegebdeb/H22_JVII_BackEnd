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
           transform.position = recordedMatrixEntity.Dequeue().MatrixPosition;
           yield return new WaitForEndOfFrame();
       }

    }

    private IEnumerator CoStartRecording()
    {
        while (true) { 

            recordedMatrixEntity.Enqueue(new MatrixEntity(transform.position));
            yield return new WaitForEndOfFrame();
            print("single");
        }
    }

    [Button]
    public void PlayRecording()
    {
        StartCoroutine(CoPlayRecording());
    }

    [Button]
    public void StopRecording() {
        StopAllCoroutines();
    }

    [Button]
    public void StartRecording()
    {
        StartCoroutine(CoStartRecording());
    }

}

[InlineEditor, System.Serializable]
public class MatrixEntity {

    [ShowInInspector]
    public Vector3 MatrixPosition { get; }

    public MatrixEntity(Vector3 pos) {
        MatrixPosition = pos;
    }
}