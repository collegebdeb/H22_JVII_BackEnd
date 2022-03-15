using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MatrixRecord : MonoBehaviour
{

    [ShowInInspector]
    public List<MatrixTransform> matrixTransforms;

    private IEnumerator CoPlayRecording()
    {
        foreach (var t in matrixTransforms)
        {
            transform.position = t.MatrixPosition;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CoStartRecording()
    {
        while (true) { 

            matrixTransforms.Add(new MatrixTransform(transform.position));
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
public class MatrixTransform {

    [ShowInInspector]
    public Vector3 MatrixPosition { get; }

    public MatrixTransform(Vector3 pos) {
        MatrixPosition = pos;
    }
}