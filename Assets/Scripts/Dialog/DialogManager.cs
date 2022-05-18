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

    public static bool dialogPlaying;
    
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
        TryShowDialog();
    }

    public void TryShowDialog()
    {
        if (dialogPlaying) return;
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
            uiLogsEffect.Send?.Invoke("Plus de dialog dans la liste");
            Debug.Log("No More dialog in queue");
            return;
        }

        dialogPlaying = true;
        Dialog dialog = dialogQueue.Dequeue();
        SpawnDialog(dialog);
    }

    private void SpawnDialog(Dialog dialog)
    {
        DialogDisplay dialogDisplay = Instantiate(dialogDisplayPrefab);
        dialogDisplay.transform.SetParent(parentSpawnPos, false);

        dialogDisplay.OnTextFinished += HandleDialogTextFinished;
        dialogDisplay.OnTextStarted += HandleTextStarted;
        //dialogDisplay.enabled = false;
        //dialogDisplay.textAnimator.AssignSharedAppearancesData(dialog.customParameters.parameters.customAppearanceValues);
        //dialogDisplay.enabled = true;
        dialogDisplay.ConstructAndDisplayDialog(dialog);
    }
    
    //What to do when the text start showing
    private void HandleTextStarted(DialogDisplay dialogDisplay)
    {
        PlayAudio(dialogDisplay);
    }

    private void PlayAudio(DialogDisplay dialogDisplay)
    {
        dialogDisplay.AudioPeer._audioSource.clip = dialogDisplay.Dialog.Audio.clip;
        dialogDisplay.AudioPeer._audioSource.Play();
    }

    //What to do when the text finished showing
    private void HandleDialogTextFinished(DialogDisplay dialogDisplay)
    {
        StartCoroutine(DeSpawnDialog(dialogDisplay));
        dialogPlaying = false;
        TryShowDialog();
    }

    //Kill dialog
    private IEnumerator DeSpawnDialog(DialogDisplay dialogDisplay)
    {
        yield return new WaitForEndOfFrame();
        Destroy(dialogDisplay.gameObject);
    }
    
    
}


