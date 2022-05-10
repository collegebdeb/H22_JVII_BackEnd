using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DarkTonic.MasterAudio;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog1", menuName = "Dialog", order = 1)]
public class SDialog : ScriptableObject
{

    [TextArea(3, 50), SerializeField]
    [Description("Dialog content")]
    public string text = " ";
    
    [Toggle("useAudio")]
    public AudioDialog audioDialog;
    
    [ShowInInspector]
    public DialogInfo info;
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
}
