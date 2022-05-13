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
        foreach (EventDialog eventDialog in FindObjectsOfType<EventDialog>())
        {
            foreach (SDialog sDialog in eventDialog.sDialogs)
            {
                foreach (Dialog dialog in sDialog.dialogs)
                {
                    DialogsOnScene.Add(new DialogObjectTextFinder(eventDialog.gameObject, eventDialog.sDialogs, dialog.Content.text));
                }
                
            }
        }
    }

}

[System.Serializable]
public struct DialogObjectTextFinder
{
    public GameObject SceneObject;
    public List<SDialog> dialogInScene;
    public string contentText;

    public DialogObjectTextFinder(GameObject sceneObject, List<SDialog> dialogInScene, string contentText)
    {
        SceneObject = sceneObject;
        this.dialogInScene = dialogInScene;
        this.contentText = contentText;
    }
    
}
