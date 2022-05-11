using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Parameters1", menuName = "Dialog/parameter", order = 100)]
public class DialogParameters : ScriptableObject
{

    #region Parameters

    [HideLabel]
    [HorizontalGroup("ParametersOption")]
    public bool useDefaultParameters = true;
    
    [Sirenix.OdinInspector.Button]
    [HorizontalGroup("ParametersOption", MinWidth = 0.9f)]
    [GUIColor("GetButtonParameterColor")]
    public void UseDefaultParameters()
    {
        useDefaultParameters = !useDefaultParameters;
    }

    private Color GetButtonParameterColor()
    {
        return useDefaultParameters ? new Color(0.77f, 0.78f, 1f) : Color.white;
    }


    #endregion
  
    
    public enum ContinuationMethod { Auto }
    
    [HideIfGroup("useDefaultParameters")]
    [HorizontalGroup("useDefaultParameters/Parameters")]
    public ContinuationMethod method;
    
    [HorizontalGroup("useDefaultParameters/Parameters")]
    public float delayAfterFinish;
    
    public DialogParameters(ContinuationMethod method, float delayAfterFinish)
    {
        this.method = method;
        this.delayAfterFinish = delayAfterFinish;
        useDefaultParameters = false;
    }
    
    public static DialogParameters NormalParameters()
    {
        return new DialogParameters(ContinuationMethod.Auto, 2f);
    }
}