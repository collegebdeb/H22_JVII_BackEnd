using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private DialogDisplay dialogDisplay;
    public static Queue<Dialog> dialogQueue;

    #endregion

    #region Enable

    private void OnEnable()
    {
        EventDialog.OnDialogTriggered += NewDialog;
    }
    private void OnDisable()
    {
        EventDialog.OnDialogTriggered -= NewDialog;
    }

    #endregion
    
    public void NewDialog(Dialog dialog)
    {
        AddDialogToQueue(dialog);
        dialogDisplay.UpdateNewDisplayText(dialog.Content.text);
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


