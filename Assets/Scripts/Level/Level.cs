using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static event Action<Level> OnStartCurrentLevel;
    private void Start()
    {
        OnStartCurrentLevel?.Invoke(this);
    }
}
