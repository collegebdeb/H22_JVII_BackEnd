using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    public static event Action<Level, Vector3> OnLevelFinished ;
    public GameObject explosion;
    public GameObject visuals;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void Start()
    {
        
    }

    [Button]
    public void BreakGame()
    {
        Time.timeScale = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (MatrixManager.worldState == MatrixManager.WorldState.Matrix) return;
        
        
    }

    public void TriggerVFX()
    {
        explosion?.SetActive(true);
        visuals?.SetActive(false);
    }
}
