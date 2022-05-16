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

    [SceneObjectsOnly] 
    public Transform parentSpawnPos;

    [InlineEditor]
    public DialogParameters currentDefaultDialogParameters;

    public List<DialogDisplay> currentDialogDisplays;
    #endregion

    #region Enable

    private void OnEnable()
    {
        Dialog.RequestToQueue += HandleNewDialogRequest; //Add a new dialog to the queue

    }
    private void OnDisable()
    {
        Dialog.RequestToQueue -= HandleNewDialogRequest;
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
        DialogDisplay dialogDisplay = Instantiate(dialogDisplayPrefab);
        dialogDisplay.transform.SetParent(parentSpawnPos, false);

        dialogDisplay.OnTextShowed += HandleDialogTextShowed;
        dialogDisplay.OnTextStarted += HandleTextStarted;
        //dialogDisplay.enabled = false;
        //dialogDisplay.textAnimator.AssignSharedAppearancesData(dialog.customParameters.parameters.customAppearanceValues);
        //dialogDisplay.enabled = true;
        dialogDisplay.ConstructAndDisplayDialog(dialog);
    }
    
    //Fired the typewriter start typing
    private void HandleTextStarted(DialogDisplay dialogDisplay)
    {
        //dialogDisplay.Dialog.Audio.PlayAudio();
    }

    //Fired when the text is done showing
    private void HandleDialogTextShowed(DialogDisplay dialogDisplay)
    {
        StartCoroutine(DeSpawnDialog(dialogDisplay));
    }

    private IEnumerator DeSpawnDialog(DialogDisplay dialogDisplay)
    {
        yield return new WaitForEndOfFrame();
        Destroy(dialogDisplay.gameObject);
    }
    
    
}


