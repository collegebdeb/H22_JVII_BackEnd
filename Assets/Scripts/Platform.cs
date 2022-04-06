using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject matrixSkin;
    public GameObject realSkin;
    public GameObject switchSkin;

    public static event Action<Platform> OnPlatformInitialized;

    private void Start()
    {
        OnPlatformInitialized?.Invoke(this);
    }
}