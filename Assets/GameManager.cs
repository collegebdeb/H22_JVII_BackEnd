using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Scene> levels = new List<Scene>();
    public Level currentLevel;

    
    private void OnEnable()
    {
        LevelExit.OnLevelFinished += LevelFinished;
        Level.OnStartCurrentLevel += NewLevel;
    }

    private void Start()
    {
    
    }

    public void LevelFinished(Level level)
    {
        
    }
    
    public void NewLevel(Level level)
    {
        currentLevel = level;
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
