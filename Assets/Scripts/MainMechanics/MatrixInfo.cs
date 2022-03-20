using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[InlineEditor, System.Serializable]
public class MatrixInfo {

    [ShowInInspector]
    public Vector3 MatrixPosition { get; }
    
    [ShowInInspector]
    public Quaternion MatrixRotation { get; }
    
    public MatrixInfo(Vector3 pos, Quaternion rot) {
        MatrixPosition = pos;
        MatrixRotation = rot;
    }
}
