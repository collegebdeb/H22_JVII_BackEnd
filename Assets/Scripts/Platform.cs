using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Platform : MonoBehaviour
{
    public GameObject matrixSkin;
    public GameObject realSkin;
    public GameObject switchSkin;

    public static event Action<Platform> OnPlatformInitialized;

    private void OnEnable()
    {
        OnPlatformInitialized?.Invoke(this);
    }
}
