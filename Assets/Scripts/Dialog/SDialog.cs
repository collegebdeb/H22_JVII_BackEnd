using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DarkTonic.MasterAudio;
using ExternalPropertyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog1", menuName = "Dialog", order = 1)]
public class SDialog : ScriptableObject
{
    [ShowInInspector]
    public DialogContent content;
    
    [ShowInInspector]
    public DialogParameters parameters;
    
    [Foldout("Audio")]
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
public class DialogParameters
{

    #region Parameters

    [HideLabel]
    [HorizontalGroup("ParametersOption")]
    public bool useDefaultParameters = true;
    
    [Sirenix.OdinInspector.Button]
    [HorizontalGroup("ParametersOption", MinWidth = 0.9f)]
    [GUIColor("GetButtonParameterColor")]
    public void UseDefaultParameters()
    {
        useDefaultParameters = !useDefaultParameters;
    }

    private Color GetButtonParameterColor()
    {
        return useDefaultParameters ? new Color(0.77f, 0.78f, 1f) : new Color(1f, 0.93f, 0.94f); }


    #endregion
  
    
    public enum ContinuationMethod { Auto }
    
    [HideIfGroup("useDefaultParameters")]
    [HorizontalGroup("useDefaultParameters/Parameters")]
    public ContinuationMethod method;
    
    [HorizontalGroup("useDefaultParameters/Parameters")]
    public float delayAfterFinish;
    
    public DialogParameters(ContinuationMethod method, float delayAfterFinish)
    {
        this.method = method;
        this.delayAfterFinish = delayAfterFinish;
        useDefaultParameters = false;
    }
    
    public static DialogParameters NormalParameters()
    {
        return new DialogParameters(ContinuationMethod.Auto, 2f);
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