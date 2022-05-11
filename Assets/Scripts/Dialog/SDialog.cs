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
    [ShowInInspector, HideLabel]
    public Dialog dialog;

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
        MasterAudio.PlaySound(audioGroupName);
        //MasterAudio.FireCustomEvent("Dialog1", new Vector3(0,0,0));
    }
    
    public AudioDialog(bool useAudio, string audioGroupName="")
    {
        this.Enabled = useAudio;
        this.audioGroupName = audioGroupName;
    }
}

[System.Serializable, Toggle("Enabled", CollapseOthersOnExpand = false)]
public class CustomParameters
{
    public bool Enabled;
    
    private DialogParameters _parameters;
    
    [ShowInInspector, HideLabel] public DialogParameters Parameters
    {
        get => _parameters;
        set => _parameters = value;
    }
    private Color GetButtonParameterColor()
    {
        return Enabled ?  new Color(0.77f, 0.78f, 1f) : Color.white;
    }
}