using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DarkTonic.MasterAudio;
using ExternalPropertyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog1", menuName = "Dialog/content", order = 10)]
public class SDialog : ScriptableObject
{
    [ShowInInspector]
    public Dialog dialog;

    [ShowInInspector]
    public DialogContent content;
    
    [ShowInInspector]
    public DialogParameters parameters;
    
    [Toggle("useAudio")]
    public AudioDialog audioDialog;
    
}


[System.Serializable]
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


[System.Serializable]
public struct AudioDialog
{
    public bool useAudio;
    [SoundGroupAttribute] public string audioGroupName;
    
    [Sirenix.OdinInspector.Button]
    public void PlayAudio()
    {
        MasterAudio.PlaySound(audioGroupName);
        //MasterAudio.FireCustomEvent("Dialog1", new Vector3(0,0,0));
    }
    
    public AudioDialog(bool useAudio, string audioGroupName="")
    {
        this.useAudio = useAudio;
        this.audioGroupName = audioGroupName;
    }
}