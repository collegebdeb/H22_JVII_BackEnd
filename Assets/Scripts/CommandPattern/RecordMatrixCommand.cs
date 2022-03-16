using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[InlineEditor, System.Serializable]

public class RecordMatrixCommand : ICommand
{

    [ShowInInspector] public Vector3 matrixPosition;

    public RecordMatrixCommand(Vector3 matrixPosition) {
        this.matrixPosition = matrixPosition;
    }

    public void Execute()
    {
        
    }
}

