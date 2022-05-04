using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog1", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{

    [TextArea(3, 50), SerializeField]
    public string dialog = " ";

    public bool useAudio;
    [SoundGroupAttribute] public string audioGroupName;

    public void PlayAudio()
    {
        MasterAudio.PlaySound(audioGroupName);
        //MasterAudio.FireCustomEvent("Dialog1", new Vector3(0,0,0));
    }
    
 
}
