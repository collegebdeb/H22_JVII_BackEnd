using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DialogFinder : MonoBehaviour
{
    [InfoBox("Va chercher tout les dialogs avec le texte écrit dans la scène donc avec les EventDialog")]
    [Searchable] public List<DialogObjectTextFinder> DialogsOnScene;
   // [Searchable] public List<SDialog> DialogsInProject;

    [Button]
    public void FindDialogsOnScene()
    {
        DialogsOnScene.Clear();
        foreach (EventDialog edialog in FindObjectsOfType<EventDialog>())
        {
            foreach (SDialog sDialog in edialog.sDialog)
            {
                DialogsOnScene.Add(new DialogObjectTextFinder(sDialog, sDialog.dialog.Content.text));
            }
        }
    }

}

[System.Serializable]
public struct DialogObjectTextFinder
{
    public SDialog dialogInScene;
    public string contentText;

    public DialogObjectTextFinder(SDialog dialogInScene, string contentText)
    {
        this.dialogInScene = dialogInScene;
        this.contentText = contentText;
    }
}
