using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;



// fonction visible dans l'inspecteur pour gérer mes clips audios
[System.Serializable]
public class Sound
{

    //déclaration de variables
    public string name;
    public AudioClip clip;
    
    
    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool loop = false;


    //Me permet de set le clip audio à l'audio source et de modifier avec des sliders le volume et le pitch (random) + autres fonctionnalités
    private AudioSource source;
    public void SetSource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }

    public void Play()
    {
        source.volume = volume * (1+ Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
}

public class AudioManager : MonoBehaviour
{

    //Tableau affiché dans le moteur de jeu qui me permet de gérer mes clips audio
    [SerializeField]   
    Sound[] sounds;

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
    }


    #endregion
    
    //lorsque les events sont invoke dans les autres scripts, on appel une fonction du Audio Manager
    private void OnEnable()
    {
        //
    }

    //Par mesure de sécurité je disable la fonction de jouer un son. C'est au cas où on devrait désactiver le sound manager (préparation pour le tp final de jeu vidéo)
    private void OnDisable()
    {

    }
}
