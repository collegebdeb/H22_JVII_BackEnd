using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;



[System.Serializable]
public class Dialog
{
    
    #region Content
    
    private DialogContent _content;
    
    [PropertyOrder(-1)]
    [ShowInInspector, HideLabel]
    public DialogContent Content
    {
        get => _content;
        set => _content = value;
    }
    
    #endregion

    #region Parameters
    
    [VerticalGroup("Options")]
    [ShowInInspector]
    public CustomParameters customParameters;
    
    #endregion
    
    #region Audio

    private AudioDialog _audio;
    
    [VerticalGroup("Options")]
    [ShowInInspector]
    public AudioDialog Audio
    {
        get => _audio;
        set => _audio = value;
    }

    #endregion
    
    public static event Action<Dialog> OnAddToQueue;

    public Dialog(string content)
    {
        Content = new DialogContent(content);
    }

    public Dialog(string content, DialogParameters parameters)
    {
        Content = new DialogContent(content);
        customParameters.Parameters = parameters;
    }

    public Dialog(DialogContent content, DialogParameters parameters, AudioDialog audio)
    {
        Content = content;
        customParameters.Parameters = parameters;
        Audio = audio;
    }

    public void AddInQueue()
    {
        OnAddToQueue?.Invoke(this);
    }
}




