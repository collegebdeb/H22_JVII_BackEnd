using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixSkinSwitch : MonoBehaviour
{
    public Material mat;

    public List<ShaderMatrixSwitch> switches;
    private void OnEnable()
    {
        MatrixManager.OnMatrixActivated += SwitchToMatrixSkin;
    }

    private void Start()
    {
        mat.SetFloat("Height", 100);
    }

    private void OnDisable()
    {
        //MatrixManager.OnRealWorldActivated += SwitchToRealSkin;
    }

    private void SwitchToMatrixSkin()
    {
        foreach (ShaderMatrixSwitch shaderSwitch in switches)
        {
            StartCoroutine(CoSwitch(shaderSwitch.shaderPropertyName, shaderSwitch.shaderPropertyValue.x,
                shaderSwitch.shaderPropertyValue.y, shaderSwitch.timeToTransitionToMatrix,
                shaderSwitch.transitionCurve));
        }
    }
    
    private IEnumerator CoSwitch(string shaderPropertyName, float initial, float final,float blendTime, AnimationCurve curve)
    {
        float currentPropertyValue = initial;
        float currentPropertyCurvedValue = initial;
        float propertyRange = final - initial;
        float rate = (propertyRange) / blendTime;

        while (currentPropertyValue <= final)
        {
            print(currentPropertyCurvedValue);
            mat.SetFloat(shaderPropertyName, currentPropertyValue);
            currentPropertyValue += Time.deltaTime * rate;
            currentPropertyCurvedValue = currentPropertyValue * curve.Evaluate(currentPropertyValue/final);
            yield return new WaitForEndOfFrame();
        }
        
    }
}

[Serializable]
public class ShaderMatrixSwitch
{
    public AnimationCurve transitionCurve;
    public string shaderPropertyName;
    public Vector2 shaderPropertyValue;
    public float timeToTransitionToMatrix;
}
