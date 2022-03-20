using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public static event Action<Level> OnLevelFinished ;
    private void OnEnable()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnLevelFinished?.Invoke(GameManager.i.currentLevel);
        }
    }
}
