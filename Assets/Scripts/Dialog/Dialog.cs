using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    private SDialog _sDialog;
    
    private DialogContent _content;
    private DialogInfo _info;
    private AudioDialog _audioDialog;
    public SDialog SDialog
    {
        get => _sDialog;
        set => _sDialog = value;
    }
    public DialogContent Content
    {
        get => _content;
        set => _content = value;
    }
    public DialogInfo Info
    {
        get => _info;
        set => _info = value;
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
        Info = sDialog.info;
        AudioDialog = sDialog.audioDialog;
    }

    public Dialog(DialogContent content, DialogInfo info, AudioDialog audioDialog)
    {
        Content = content;
        Info = info;
        AudioDialog = audioDialog;
    }

    public void AddInQueue()
    {
        OnAddToQueue?.Invoke(this);
    }
}


