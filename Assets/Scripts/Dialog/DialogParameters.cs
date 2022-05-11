using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Parameters1", menuName = "Dialog/parameter", order = 100)]
public class DialogParameters : ScriptableObject
{
    
    public enum ContinuationMethod { Auto }
    
 
    public ContinuationMethod method;
    
    
    public float delayAfterFinish;
    
    public DialogParameters(ContinuationMethod method, float delayAfterFinish)
    {
        this.method = method;
        this.delayAfterFinish = delayAfterFinish;
    }
    
    public static DialogParameters NormalParameters()
    {
        return new DialogParameters(ContinuationMethod.Auto, 2f);
    }
}