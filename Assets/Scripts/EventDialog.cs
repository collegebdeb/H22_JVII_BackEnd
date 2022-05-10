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
    public List<SDialog> sDialog;

    private void Awake()
    {
        if(sDialog==null) Debug.LogError("No Dialog selected at" + name );
    }
    
    [Sirenix.OdinInspector.Button]
    private void AddDialogs()
    {
        foreach (SDialog sDialog in sDialog)
        {
            Dialog dialog = new Dialog(sDialog);
            dialog.AddInQueue();
        }
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

    #region TriggerEnter

    private bool _triggerOnTriggerEnter;
    
    [ShowIf("_triggerOnTriggerEnter", true)]
    [TagSelector]
    public string tagFilter = "";

    [ShowIf("_triggerOnTriggerEnter", true)]
    [Required("Add a Collider Component")]
    [ShowInInspector, ReadOnly] private Collider collider;

    private void OnTriggerEnter(Collider other)
    {
        if (!_triggerOnTriggerEnter) return;

        if (other.CompareTag(tagFilter))
        {
            AddDialogs();
        }
    }

    #endregion

    #region ValidateEditorUpdate

    public void CheckValue()
    {
        if ((triggerMethods & TriggerMethods.TriggerEnter) == TriggerMethods.TriggerEnter)
        {
            _triggerOnTriggerEnter = true;
            collider = GetComponent<Collider>();
        }
        else
        {
            _triggerOnTriggerEnter = false;
        }
    }

    private void OnValidate()
    {
        CheckValue();
    }

    #endregion
    
    
}
    
    
