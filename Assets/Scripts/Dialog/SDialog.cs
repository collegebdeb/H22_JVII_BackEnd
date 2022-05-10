using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DarkTonic.MasterAudio;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog1", menuName = "Dialog", order = 1)]
public class SDialog : ScriptableObject
{
    [ShowInInspector]
    public DialogContent content;
    
    [ShowInInspector]
    public DialogInfo info;
    
    [Toggle("useAudio")]
    public AudioDialog audioDialog;

    public Transform ttransform;
}


[System.Serializable]
public class DialogContent
{
    [HideLabel]
    [TextArea(3, 50), SerializeField]
    [Description("Dialog content")]
    public string text = " ";

    public DialogContent(string text)
    {
        this.text = text;
    }
}

[System.Serializable]
public class DialogInfo
{
    public enum ContinuationMethod { Auto }
    public ContinuationMethod method;

    public DialogInfo(ContinuationMethod method)
    {
        this.method = method;
    }
}

[System.Serializable]
public class AudioDialog
{
    public bool useAudio;
    [SoundGroupAttribute] public string audioGroupName;
    
    [Button]
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