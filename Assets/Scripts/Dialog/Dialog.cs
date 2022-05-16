using System;
using System.Collections;
using System.Collections.Generic;
using ExternalPropertyAttributes;
using Febucci.UI.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;


[System.Serializable]
public class Dialog
{
    
    #region Content
    
    [HideInInspector, SerializeField]
    private DialogContent content;
    
    [ShowInInspector, HideLabel]
    public DialogContent Content
    {
        get => content;
        set => content = value;
    }
    
    #endregion
    
    #region Parameters
    
    [PropertyOrder(1)]
    public CustomParameters customParameters;
    
    [HideInInspector]
    
    [Foldout("TextAnimator")] [InlineEditor]
    public BuiltinAppearancesDataScriptable customAppearanceValues;
    
    #endregion
    
    #region Audio
    
    [PropertyOrder(2)]
    [HideInInspector, SerializeField]
    private AudioDialog audio;

    [ShowInInspector, HideLabel]
    public AudioDialog Audio
    {
        get => audio;
        set => audio = value;
    }

    #endregion
    
    public static event Action<Dialog> RequestToQueue;

    public Dialog(string content)
    {
        Content = new DialogContent(content);
    }

    public Dialog(string content, DialogParameters parameters)
    {
        Content = new DialogContent(content);
        customParameters.parameters = parameters;
    }

    public Dialog(DialogContent content, DialogParameters parameters, AudioDialog audio)
    {
        Content = content;
        customParameters.parameters = parameters;
        Audio = audio;
    }

    public void AddInQueue()
    {
        RequestToQueue?.Invoke(this);
    }
}




