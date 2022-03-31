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


    //Me permet de set le clip audio à l'audio source et de modifier avec des sliders le volume et le pitch (random) + autres fonctionnalités
    private AudioSource _source;
    public void SetSource (AudioSource _source)
    {
        this._source = _source;
        this._source.clip = clip;
        this._source.loop = loop;
    }

    [Button]
    private void TestPlay()
    {
        _source.Play();
    }
 
    public void Play()
    {
        //_source.volume = volume * (1+ Random.Range(-randomVolume / 2f, randomVolume / 2f));
        //_source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        _source.Play();
    }
    public void Stop()
    {
        _source.Stop();
    }
}
