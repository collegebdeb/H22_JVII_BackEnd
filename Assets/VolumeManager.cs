using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Sirenix.OdinInspector;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Volume real;
    public AnimationCurve realCurve;
    [SerializeField] private Volume transition;
    public AnimationCurve transitionCurve;
    [SerializeField] private Volume matrix;
    public AnimationCurve matrixCurve;

    public float fromRealToMatrixBlendTime = 1f;
    public float fromMatrixToTransitionBlendTime = 1f;
    public float fromTransitionRealBlendTime = 1f;
    private void OnEnable()
    {
        MatrixManager.OnMatrixActivated += FromRealToMatrix;
        MatrixManager.OnTransitionActivated += FromMatrixToTransition;
        MatrixManager.OnRealWorldActivated += FromTransitionToReal;
    }

    private void OnDisable()
    {
        MatrixManager.OnMatrixActivated -= FromRealToMatrix;
        MatrixManager.OnTransitionActivated -= FromMatrixToTransition;
        MatrixManager.OnRealWorldActivated -= FromTransitionToReal;
    }

    public void FromRealToMatrix()
    {
        TransitionBetweenVolumes(real,matrix,fromRealToMatrixBlendTime, realCurve);
    }
    
    public void FromMatrixToTransition()
    {
        TransitionBetweenVolumes(matrix,transition,fromMatrixToTransitionBlendTime, matrixCurve);
    }
    
    public void FromTransitionToReal()
    {
        TransitionBetweenVolumes(transition,real,fromTransitionRealBlendTime, transitionCurve);
    }


    [Button]
    public void TransitionBetweenVolumes(Volume vol1, Volume vol2, float blendTime, AnimationCurve curve)
    {
        StartCoroutine(CoTransitionBetweenVolumes(vol1, vol2, blendTime, curve));
    }


    IEnumerator CoTransitionBetweenVolumes(Volume vol1, Volume vol2, float blendTime, AnimationCurve curve)
    {
        float value = 0;
        float rate = 1f / blendTime;

        while (value <= 1f)
        {
            value += Time.deltaTime * rate;
            vol1.weight = 1-(value*curve.Evaluate(value));
            vol2.weight = value*curve.Evaluate(value);
            yield return new WaitForEndOfFrame();
        }

    }
}
