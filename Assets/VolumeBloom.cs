using System;
using System.Collections;
using System.Collections.Generic;
using ExternalPropertyAttributes;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeBloom : MonoBehaviour
{
    public Volume volume1;
    public Volume volumeBloom;
    public AnimationCurve curve;
    public float blendTime = 5f;

    private void OnEnable()
    {
        bulletScript.StartShake += StartTransition;
    }

    public void StartTransition()
    {
        TransitionBetweenVolumes(volume1, volumeBloom, blendTime, curve);
        Application.targetFrameRate = 24;
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
