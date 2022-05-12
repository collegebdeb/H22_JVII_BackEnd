using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Febucci;
using Febucci.UI.Core;


[CreateAssetMenu(fileName = "Parameters", menuName = "Dialog/parameter", order = 100)]
public class DialogParameters : ScriptableObject
{
    
    #region TextAnimator

    [BoxGroup("TextAnimator")] [InlineEditor]
    public BuiltinAppearancesDataScriptable appearanceDefaultValues;
    
    #endregion
    
    public enum ContinuationMethod { Auto }
    
 
    public ContinuationMethod method;
    
    [ShowInInspector]
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