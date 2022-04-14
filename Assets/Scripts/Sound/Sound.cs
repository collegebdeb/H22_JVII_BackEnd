using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;

[InlineEditor]
[CreateAssetMenu(fileName = "Data", menuName = "", order = 1)]
public class Sound : ScriptableObject
{
    [SerializeField, ExternalPropertyAttributes.ReadOnly] private AudioList.Sound name = AudioList.Sound.NotConfigured;
    public AudioClip clip;
    public AudioMixerGroup mixer;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [MinMaxSlider(-1,1)]
    public Vector2 randomVolume;
    [MinMaxSlider(-1,1)]
    public Vector2 randomPitch;

    public bool loop = false;
    
    public AudioSource _source;

    public void InitSound()
    {
        _source.clip = clip;
        _source.volume = volume;
        _source.pitch = pitch;
        _source.loop = loop;
    }

    public void Play()
    {
        RandomPitch();
        RandomVolume();
        _source.Play();
    }

    private void RandomPitch()
    {
        _source.pitch = pitch;
        _source.pitch = pitch + Random.Range(randomPitch.x, randomPitch.y);
    }

    public void RandomVolume()
    {
        _source.volume = volume;
        _source.volume = volume + Random.Range(randomVolume.x, randomVolume.y);
    }
    public void Stop()
    {
        _source.Stop();
    }

    public void Mute()
    {
        _source.volume = 0;
    }

    public void UnMute()
    {
        _source.volume = volume;
    }
}
