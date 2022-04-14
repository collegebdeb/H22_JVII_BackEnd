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
        OnRealWorldActivated,
        OnTransitionActivated,
        OnMatrixActivated,
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
        foreach (var sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.Value._source = source;
            sound.Value.InitSound();
        }
    }

    public void Play(AudioList.Sound keySound, GameObject origin)
    {
        sounds[keySound].Play();
    }

    public void Mute(AudioList.Sound keySound, GameObject origin)
    {
        sounds[keySound].Mute();
    }
    
    public void UnMute(AudioList.Sound keySound, GameObject origin)
    {
        sounds[keySound].UnMute();
    }

    private void OnEnable()
    {
        SoundEvents.onPlayerJump += Play;
    }

    private void OnDisable()
    {
        SoundEvents.onPlayerJump -= Play;
    }
}
