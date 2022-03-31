using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public static event Action<Level> OnLevelFinished ;
    public GameObject explosion;
    public GameObject visuals;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerVFX();
            OnLevelFinished?.Invoke(GameManager.i.currentLevel);
        }
    }

    public void TriggerVFX()
    {
        SoundEvents.onCollideLevelExit?.Invoke(AudioList.Sound.Unknown, gameObject);
        explosion?.SetActive(true);
        visuals?.SetActive(false);
    }
}
