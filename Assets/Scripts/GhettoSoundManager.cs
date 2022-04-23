using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhettoSoundManager : MonoBehaviour
{
    public static GhettoSoundManager i;
    public AudioSource Music;
    public AudioSource groundShake;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        Music.Play();
    }
    public void PlayGroundShake()
    {
        groundShake.Play();
    }
    
    
}
