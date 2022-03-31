using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

public static class AudioList
{
    public enum Sound
    {
        Unknown,
        NotConfigured,
        OnPlayerJump,
        OnCannotSwitchToMatrix,
    }
}


public class AudioManager : SerializedMonoBehaviour
{

    public Dictionary<AudioList.Sound, Sound> sounds = new Dictionary<AudioList.Sound, Sound>();
    
    public static Sound sound;
    
    [Sirenix.OdinInspector.FilePath(ParentFolder = "Assets/Resources/")]
    public string relativeToParentPath;
    
    #region Singleton

    public static AudioManager instance;

    //me permet de conserver l'audio manager entre les différentes scènes
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }


        DontDestroyOnLoad(gameObject);

        InitializeSounds();
    }


    #endregion

    public void InitializeSounds()
    {
        
    }

    public void Play(AudioList.Sound sound, GameObject origin)
    {
        
    }
    
    public void Play3D(AudioList.Sound sound, GameObject origin)
    {
        
    }
    
    public void PlayPlayerJump(AudioList.Sound sound, GameObject origin)
    {
        
    }

    private void OnEnable()
    {
        SoundEvents.onPlayerJump += PlayPlayerJump;
    }

    private void OnDisable()
    {
        SoundEvents.onPlayerJump -= Play3D;
    }
}
