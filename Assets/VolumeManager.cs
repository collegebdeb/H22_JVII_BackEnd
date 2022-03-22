using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Sirenix.OdinInspector;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Volume real;
    [SerializeField] private Volume transition;
    [SerializeField] private Volume matrix;


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    [Button]
    public void TransitionBetweenVolumes(Volume vol1, Volume vol2, float blendTime)
    {
        StartCoroutine(CoTransitionBetweenVolumes(vol1, vol2, blendTime));
    }


    IEnumerator CoTransitionBetweenVolumes(Volume vol1, Volume vol2, float blendTime)
    {
        float value = 0;
        float rate = 1f / blendTime;

        while (value <= 1f)
        {
            value += Time.deltaTime * rate;
            vol1.weight = 1-value;
            vol2.weight = value;
            yield return new WaitForEndOfFrame();
        }
        print("Volume updated after " + value);
    
    }
}
