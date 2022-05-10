using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DarkTonic.MasterAudio;
using JetBrains.Annotations;
using Sirenix.OdinInspector;

public class EventDialog : MonoBehaviour
{
    
    [InlineEditor, Sirenix.OdinInspector.Required]
    public SDialog sDialog;

    public static event Action<Dialog> OnDialogTriggered;

    [Sirenix.OdinInspector.Button]
    private void TriggerDialog()
    {
        Dialog dialog = new Dialog(sDialog);
        OnDialogTriggered?.Invoke(dialog);
    }
    
    private void Awake()
    {
        if(sDialog==null) Debug.LogError("No Dialog selected at" + name );
    }

    [System.Flags]
    public enum TriggerMethods
    {
        TriggerEnter = 1 << 1,
        B = 1 << 2,
        C = 1 << 3,
        All = TriggerEnter | B | C
    }
    
    [EnumToggleButtons, HideLabel]
    public TriggerMethods triggerMethods;

    private bool triggerOnTriggerEnter;
    
    [ShowIf("triggerOnTriggerEnter", true)]
    [TagSelector]
    public string TagFilter = "";

    [ShowIf("triggerOnTriggerEnter", true)]
    [Required("Add a Collider Component")]
    [ShowInInspector, ReadOnly] private Collider collider;
    
    public void CheckValue()
    {
        if ((triggerMethods & TriggerMethods.TriggerEnter) == TriggerMethods.TriggerEnter)
        {
            triggerOnTriggerEnter = true;
            collider = GetComponent<Collider>();
        }
        else
        {
            triggerOnTriggerEnter = false;
        }
    }

    private void OnValidate()
    {
        CheckValue();
    }
    
    
    
    
    
    
}
    
    
