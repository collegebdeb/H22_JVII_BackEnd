using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DarkTonic.MasterAudio;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;



public class EventDialog : MonoBehaviour
{

    [InlineEditor, Sirenix.OdinInspector.Required]
    public List<SDialog> sDialogs;

    private void Awake()
    {
        if(sDialogs==null) Debug.LogError("No Dialog selected at" + name );
        CheckValue();
    }
    
    [Sirenix.OdinInspector.Button]
    private void AddDialogs()
    {
        foreach (SDialog sDialog in sDialogs)
        {
            foreach (Dialog dialog in sDialog.dialogs)
            {
                dialog.AddInQueue();
            }
         
        }
    }
    
    [System.Serializable]
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

    public bool destroyAfterTriggered;

    #region TriggerEnter

    [ShowInInspector]
    private bool _triggerOnTriggerEnter;
    
    [ShowIf("_triggerOnTriggerEnter", true)]
    [TagSelector]
    public string tagFilter = "";

    [ShowIf("_triggerOnTriggerEnter", true)]
    [Required("Add a Collider Component")]
    [ShowInInspector, ReadOnly] private Collider collider;
    
    

    private void OnTriggerEnter(Collider other)
    {
        print(_triggerOnTriggerEnter);
        print(tagFilter);
        if (!_triggerOnTriggerEnter) return;

        if (other.CompareTag(tagFilter))
        {
            AddDialogs();
        }
        
        if(destroyAfterTriggered) Destroy(gameObject, 2f);
    }

    #endregion

    #region ValidateEditorUpdate

    //Update the bool that block or allow the ontriggerenter trigger method by checking the enum state in the inspector
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

    //Enum changed
    private void OnValidate()
    {
        CheckValue();
    }

    #endregion
    
    /**
    [Button(ButtonSizes.Large)]
    [GUIColor("@Color.Lerp(Color.red, Color.green, Mathf.Abs(Mathf.Sin((float)EditorApplication.timeSinceStartup)))")]
    private static void MagnifiqueAlexandreBelovtournetasourisautourdeceboutonetcliquedessus()
    {
        while (true)
        {
            for (int i = 0; i < 1000000; i++)
            {
                print("Alexandre Belov" + i);
            }
            
        };
    }
    
    **/
}
    
    
