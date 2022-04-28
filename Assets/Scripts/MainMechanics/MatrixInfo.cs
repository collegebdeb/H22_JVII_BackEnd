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
    
    [ShowInInspector]
    public bool Alive { get; }
    
    public MatrixInfo(Vector3 pos, Quaternion rot, bool alive) {
        MatrixPosition = pos;
        MatrixRotation = rot;
        Alive = alive;
    }
}
