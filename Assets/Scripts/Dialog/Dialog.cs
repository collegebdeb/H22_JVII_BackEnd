using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    private SDialog _sDialog;
    
    private DialogContent _content;
    private DialogParameters _parameters;
    private AudioDialog _audioDialog;
    public SDialog SDialog
    {
        get => _sDialog;
        set => _sDialog = value;
    }
    [ShowInInspector]
    public DialogContent Content
    {
        get => _content;
        set => _content = value;
    }
    public DialogParameters Parameters
    {
        get => _parameters;
        set => _parameters = value;
    }
    public AudioDialog AudioDialog
    {
        get => _audioDialog;
        set => _audioDialog = value;
    }

    public static event Action<Dialog> OnAddToQueue;
    
    public Dialog(SDialog sDialog)
    {
        SDialog = _sDialog;
        Content = sDialog.content;
        Parameters = sDialog.parameters;
        AudioDialog = sDialog.audioDialog;
    }

    public Dialog(string content)
    {
        Content = new DialogContent(content);
        Parameters = DialogParameters.NormalParameters();
    }

    public Dialog(string content, DialogParameters parameters)
    {
        Content = new DialogContent(content);
        Parameters = parameters;
    }

    public Dialog(DialogContent content, DialogParameters parameters, AudioDialog audioDialog)
    {
        Content = content;
        Parameters = parameters;
        AudioDialog = audioDialog;
    }

    public void AddInQueue()
    {
        OnAddToQueue?.Invoke(this);
    }
}


