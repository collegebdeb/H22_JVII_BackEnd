using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MatrixSkinSwitch : MonoBehaviour
{
    public List<ShaderMatrixSwitch> switches;
    public float showMatrixSkinAfter;
    public float showRealSkinAfter;
    public float hideTransitionAfter;
    private void OnEnable()
    {
        MatrixManager.OnMatrixActivated += SwitchToMatrixSkin;
        MatrixManager.OnRealWorldActivated += SwitchToRealSkin;
    }

    private void OnDisable()
    {
        MatrixManager.OnMatrixActivated -= SwitchToMatrixSkin;
        MatrixManager.OnRealWorldActivated -= SwitchToRealSkin;
    }

    private void Start()
    {
        SetUpMaterialValues();
    }

    void SetUpMaterialValues()
    {
        foreach (ShaderMatrixSwitch shaderSwitch in switches)
        {
            shaderSwitch.mat.SetFloat(shaderSwitch.shaderPropertyName, shaderSwitch.shaderPropertyValue.x);
        }
    }

    private void SwitchToMatrixSkin()
    {
        ShowTransition();
        StartCoroutine(CoHideTransition());
        StartCoroutine(CoShowMatrixSkin());
        
        foreach (ShaderMatrixSwitch shaderSwitch in switches)
        {
            StartCoroutine(CoSwitch(shaderSwitch.mat, shaderSwitch.shaderPropertyName, shaderSwitch.shaderPropertyValue.x,
                shaderSwitch.shaderPropertyValue.y, shaderSwitch.timeToTransitionToMatrix,
                shaderSwitch.curveToMatrix, shaderSwitch.delay));
        }
    }
    
    private void SwitchToRealSkin()
    {
        StartCoroutine(CoShowRealSkin());
        
        foreach (ShaderMatrixSwitch shaderSwitch in switches)
        {
            StartCoroutine(CoSwitch(shaderSwitch.mat, shaderSwitch.shaderPropertyName, shaderSwitch.shaderPropertyValue.y,
                shaderSwitch.shaderPropertyValue.x, shaderSwitch.timeToTransitionToReal,
                shaderSwitch.curveToReal, shaderSwitch.delay));
        }
    }
    
    private IEnumerator CoHideTransition()
    {
        yield return new WaitForSeconds(hideTransitionAfter);
        
        foreach (Platform platform in GameManager.i.currentLevel.platforms)
        {
            platform.switchSkin.SetActive(false);
        }
    }

    private void ShowAllTransition()
    {
        foreach (Platform platform in GameManager.i.currentLevel.platforms)
        {
            platform.switchSkin.SetActive(false);
        }
    }
    
    private void ShowTransition()
    {
        foreach (Platform platform in GameManager.i.currentLevel.platforms)
        {
            platform.switchSkin.SetActive(true);
        }
    } 

    private IEnumerator CoShowMatrixSkin()
    {
        yield return new WaitForSeconds(showMatrixSkinAfter);
        
        foreach (Platform platform in GameManager.i.currentLevel.platforms)
        {
            platform.realSkin.SetActive(false);
            platform.matrixSkin.SetActive(true);
        }
    } 
    
    private IEnumerator CoShowRealSkin()
    {
        yield return new WaitForSeconds(showRealSkinAfter);
        
        foreach (Platform platform in GameManager.i.currentLevel.platforms)
        {
            platform.realSkin.SetActive(true);
            platform.matrixSkin.SetActive(false);
        }
    } 

    
    private IEnumerator CoSwitch(Material mat, string shaderPropertyName, float initial, float final,float blendTime, AnimationCurve curve, float delay)
    {
        yield return new WaitForSeconds(delay);
        float currentPropertyValue = initial;
        float currentPropertyCurvedValue = initial;
        float propertyRange = final - initial;
        float rate = (propertyRange) / blendTime;
        
        float increment = 0; 
        float incrementRate = 1f / blendTime;
        
        while (increment <= 1) // currentPropertyValue <= final
        {
            currentPropertyCurvedValue = currentPropertyValue * curve.Evaluate(increment);
            mat.SetFloat(shaderPropertyName, currentPropertyCurvedValue);
            increment += Time.deltaTime * incrementRate;
            currentPropertyValue += Time.deltaTime * rate;
            yield return new WaitForEndOfFrame();
        }

        /**
        WITH TIMER
        float increment = 0; 
        float incrementRate = 1f / blendTime;

        while (currentPropertyValue <= final)
        {
          
            
            print("cal" + currentPropertyValue * curve.Evaluate((currentPropertyValue-initial)/final));
            print("time" + currentPropertyCurvedValue);
            
            mat.SetFloat(shaderPropertyName, currentPropertyValue);
            currentPropertyValue += Time.deltaTime * rate;
            increment += Time.deltaTime * incrementRate;
            
            currentPropertyCurvedValue = currentPropertyValue * curve.Evaluate(increment);
       
            yield return new WaitForEndOfFrame();
        }
        **/

    }
    
    //Reset the material to the default initial value
    #if UnityEditor
    void PlayStateNotifier()
    {
        EditorApplication.playmodeStateChanged += ModeChanged;
    }
 
    void ModeChanged ()
    {
        if (!EditorApplication.isPlayingOrWillChangePlaymode &&
            EditorApplication.isPlaying )
        {
            SetUpMaterialValues();
        }
    }
    #endif
}



[Serializable]
public class ShaderMatrixSwitch
{
    public Material mat;
    public string shaderPropertyName;
    public Vector2 shaderPropertyValue;
    public AnimationCurve curveToMatrix;
    public float timeToTransitionToMatrix;
    public AnimationCurve curveToReal;
    public float timeToTransitionToReal;
    public float delay;
}
