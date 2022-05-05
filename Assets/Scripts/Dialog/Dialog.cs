using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog1", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{

    [TextArea(3, 50), SerializeField]
    public string dialog = " ";

    [FoldoutGroup("Audio")]
    [Toggle("useAudio")]
    public AudioDialog audioDialog;
    
    [FoldoutGroup("Audio")]
    [Button]
    public void PlayAudio()
    {
        MasterAudio.PlaySound(audioDialog.audioGroupName);
        //MasterAudio.FireCustomEvent("Dialog1", new Vector3(0,0,0));
    }
}

[System.Serializable]
public class AudioDialog
{
    public bool useAudio;
    [SoundGroupAttribute] public string audioGroupName;
}
