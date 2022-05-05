using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    
    [SerializeField] private MonoDialog monoDialog;
    private Queue<Dialog> dialogQueue;

    private void OnEnable()
    {
        EventDialog.OnDialogTriggered += NewDialog;
    }

    private void NewDialog(Dialog dialog, DialogInfo dialogInfo)
    {
        AddDialogToQueue(dialog);
        monoDialog.UpdateNewDisplayText(dialog.dialog);
        //Lean.Pool.LeanPool.Spawn(monoDialog);
    }

    private void AddDialogToQueue(Dialog dialog)
    {
        dialogQueue.Enqueue(dialog);
    }

    private void Update()
    {
        
    }
}


