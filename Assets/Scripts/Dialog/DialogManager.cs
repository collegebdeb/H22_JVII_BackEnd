using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Sirenix.OdinInspector;

public class DialogManager : MonoBehaviour
{
    #region Variables
    
    [ShowInInspector]
    public Queue<Dialog> dialogQueue = new Queue<Dialog>();
    public Queue<Dialog> dialogPriorityQueue = new Queue<Dialog>();
    
    [AssetsOnly]
    public DialogDisplay dialogDisplayPrefab;

    [ShowInInspector]
    public DialogParameters currentDefaultDialogParameters;
    #endregion

    #region Enable

    private void OnEnable()
    {
        Dialog.OnAddToQueue += HandleNewDialogRequest; //Add a new dialog to the queue
        DialogDisplay.OnTextShowed += HandleDialogTextShowed; //When all the characters are shown
    }
    private void OnDisable()
    {
        Dialog.OnAddToQueue -= HandleNewDialogRequest;
    }

    #endregion
    
    public void HandleNewDialogRequest(Dialog dialog)
    {
        AddDialogToQueue(dialog);
        ShowDialogFromQueue();
    }

    private void AddDialogToQueue(Dialog dialog)
    {
        dialogQueue.Enqueue(dialog);
    }

    [Button]
    private void ShowDialogFromQueue()
    {
        if (dialogQueue.Count == 0)
        {
            Debug.LogError("This is not suppose to happen");
            return;
        }
        
        Dialog dialog = dialogQueue.Dequeue();
        SpawnDialog(dialog);
    }

    private void SpawnDialog(Dialog dialog)
    {
        DialogDisplay dialogDisplay = LeanPool.Spawn(dialogDisplayPrefab);
        dialogDisplay.ConstructAndDisplayDialog(dialog);
    }

    private void HandleDialogTextShowed(DialogDisplay dialogDisplay)
    {
        DeSpawnDialog(dialogDisplay);
    }

    private void DeSpawnDialog(DialogDisplay dialogDisplay)
    {
        LeanPool.Despawn(dialogDisplay);
    }

    [Button]
    public void TestDialogDefault()
    {
        Dialog dialog = new Dialog("Bonjour", DialogParameters.NormalParameters());
        Dialog dialog2 = new Dialog("Bonjour2");
        
        print(dialog.Parameters.delayAfterFinish);
        print(dialog2.Parameters.delayAfterFinish);
    }
    
}


