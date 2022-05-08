using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DarkTonic.MasterAudio;
using ExternalPropertyAttributes;
using JetBrains.Annotations;
using Sirenix.OdinInspector;

public class EventDialog : MonoBehaviour
{
    [InlineEditor, Sirenix.OdinInspector.Required]
    public Dialog dialog;
    [ShowInInspector]
    public DialogInfo dialogInfo;

    public static event Action<Dialog, DialogInfo> OnDialogTriggered;

    [Sirenix.OdinInspector.Button]
    private void TriggerDialog()
    {
        OnDialogTriggered?.Invoke(dialog, dialogInfo);
    }

    private void Awake()
    {
        if(dialog==null) Debug.LogError("No Dialog selected at" + name );
    }
}
    
    
