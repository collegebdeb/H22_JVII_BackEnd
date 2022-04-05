using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Platform> platforms;
    public static event Action<Level> OnStartCurrentLevel;

    private void OnEnable()
    {
        Platform.OnPlatformInitialized += AddPlatform;
    }

    private void Start()
    {
        OnStartCurrentLevel?.Invoke(this);
    }

    private void AddPlatform(Platform platform)
    {
        platforms.Add(platform);
    }
}
