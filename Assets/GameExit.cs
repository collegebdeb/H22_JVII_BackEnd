using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class GameExit : MonoBehaviour
{
    public static event Action<Level, Vector3> OnLevelFinished ;
    public GameObject explosion;
    public GameObject visuals;
    
    public UnityEvent evenet;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    
    private void Start()
    {
        //transform.DOShakePosition(15000f,0.2f);
    }

    [Button]
    public void BreakGame()
    {
        Time.timeScale = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        TriggerVFX();
        evenet?.Invoke();
        if (MatrixManager.worldState == MatrixManager.WorldState.Matrix) return;

      

    }

    public void TriggerVFX()
    {
        explosion?.SetActive(true);
        visuals?.SetActive(false);
    }
}
