using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    
    
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
