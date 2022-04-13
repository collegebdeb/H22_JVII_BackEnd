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
    public bool testLevel;
    
    

    private void OnEnable()
    {
        Platform.OnPlatformInitialized += AddPlatform;
        if (testLevel)
        {
            Debug.LogError("Un des niveaux est en mode test. Desactiver le mode test lorsque fini de tester. level : " + gameObject.name);
            GameManager.i.currentLevel = this;
            GameManager.i.playerReal = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    private void Start()
    {
       
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
