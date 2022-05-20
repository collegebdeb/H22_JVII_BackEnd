using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

public class TriggerEndGame : MonoBehaviour
{
    public VolumeManager volumeManager;
    public Volume volume;
    private bool used;


    public void Change()
    {
        volumeManager.gameObject.SetActive(false);
        volume.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !used)
        {
            Change();
            StartCoroutine(CoTransitionBetweenVolumes(volume, 10f));
            used = false;
        }
    }

    [Button]
    public void tes()
    {
        Change();
        StartCoroutine(CoTransitionBetweenVolumes(volume, 10f));
    }
    
    IEnumerator CoTransitionBetweenVolumes(Volume vol1, float blendTime)
    {
        float value = 0;
        float rate = 1f / blendTime;

        while (value <= 1f)
        {
            value += Time.deltaTime * rate;
            vol1.weight = (value);
            yield return new WaitForEndOfFrame();
        }

    }
}
