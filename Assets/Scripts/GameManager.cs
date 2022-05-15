using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using devziie.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Level currentLevel;

    [Unity.Collections.ReadOnly, HideInInspector] public float currentTimeInMatrix;
    public float maximumTimeInMatrix;

    public Player playerReal;
    public Player playerMatrix;

    public float time;
    public float levelTransitionTime=5f;

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void OnEnable()
    {
        LevelExit.OnLevelFinished += LevelFinished;
        Level.OnStartCurrentLevel += NewLevel;
    }

    private void OnDisable()
    {
        LevelExit.OnLevelFinished -= LevelFinished;
        Level.OnStartCurrentLevel -= NewLevel;
    }

    private void Start()
    {
        InputManager.Controls.Player2D.Disable();
        InputManager.Controls.MinigameUI.Disable();
        
        Application.targetFrameRate = 144;
    }

    public void LevelFinished(Level level, Vector3 pos)
    {
        
    }

    public void SetCurrentLevel(Level level)
    {
        currentLevel = level;
    }
    
    public void NewLevel(Level level)
    {
        //currentLevel = level;
    }
    
    #region Singleton

    // Singleton instance.
    public static GameManager i = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (i == null)
        {
            i = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (i != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    #endregion
    
    //Level Manager
    
}
