using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float slowDownFactor;
    [SerializeField] private float speedUpFactor;
    private void OnEnable()
    {
        //MatrixManager.OnStartReversePlayRecord += SpeedTime;
    }
    
    private void OnDisable()
    {
        //MatrixManager.OnStartReversePlayRecord -= SpeedTime;
    }

    [Button]
    public void SlowTime()
    {
        Time.timeScale = slowDownFactor;
    }

    [Button]
    public void NormalTime()
    {
        //Time.timeScale = 1;
    }
    
    [Button]
    public void SpeedTime()
    {
        Time.timeScale = speedUpFactor;
    }
}
