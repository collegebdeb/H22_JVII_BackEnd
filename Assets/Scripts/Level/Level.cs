using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using Sirenix.OdinInspector;

public class Level : MonoBehaviour
{
    public List<Platform> platforms;
    public GameObject transition;
    public float submergeLevel;
    public AnimationCurve curve;
    public bool initialized;
    public static event Action<Level> OnStartCurrentLevel;
    public static event Action<Level> OnInitializeLevel;

    private void OnEnable()
    {
        Platform.OnPlatformInitialized += AddPlatform;
    }

    private void Start()
    {
        if (!initialized)
        {
            initialized = true;
            return;
        }
        OnStartCurrentLevel?.Invoke(this);
    }

    private void OnDisable()
    {
        Platform.OnPlatformInitialized -= AddPlatform;
    }
    

    private void AddPlatform(Platform platform)
    {
        platforms.Add(platform);
    }
}
