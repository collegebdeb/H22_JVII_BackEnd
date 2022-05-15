using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DarkTonic.MasterAudio;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog1", menuName = "Dialog/content", order = 10)]
public class SDialog : ScriptableObject
{
    [SerializeField] [ShowInInspector, HideLabel]
    public List<Dialog> dialogs;
}


[System.Serializable, Sirenix.OdinInspector.TitleGroup("Text")]
public struct DialogContent
{
    
    [HideLabel]
    [TextArea(3, 50), SerializeField]
    [Description("Dialog content")]
    public string text;

    public DialogContent(string text)
    {
        this.text = text;
    }
}


[System.Serializable, Toggle("Enabled",CollapseOthersOnExpand = false)]
public struct AudioDialog
{
    public bool Enabled;
    [SoundGroupAttribute] public string audioGroupName;
    
    [Sirenix.OdinInspector.Button]
    public void PlayAudio()
    {
        if (!Enabled)
        {
            Debug.Log("Audio is not enabled");
            return;
        }
        MasterAudio.PlaySound(audioGroupName);
        //MasterAudio.FireCustomEvent("Dialog1", new Vector3(0,0,0));
    }

    [Button]
    public void AddDialogToQueue()
    {
        
    }
    
  
}

[System.Serializable, Toggle("Enabled", CollapseOthersOnExpand = false)]
public struct CustomParameters
{
    public bool Enabled;
    
    [ShowInInspector]
    public DialogParameters parameters;
    
  
    private Color GetButtonParameterColor()
    {
        return Enabled ?  new Color(0.77f, 0.78f, 1f) : Color.white;
    }
}