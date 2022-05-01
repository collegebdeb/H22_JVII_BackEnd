using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiVoiceTheme : MonoBehaviour
{
    [SerializeField] private bool isBackend = false;

    private GameObject darkCanva;
    private GameObject lightCanva;
    private TextMeshProUGUI title;
    private TextMeshProUGUI content;

    [SerializeField] public Color light;
    [SerializeField] public Color dark;

    void Start()
    {
        title = GameObject.Find("TitleVoice").GetComponent <TextMeshProUGUI>();
        content = GameObject.Find("ContentVoice").GetComponent<TextMeshProUGUI>();
        darkCanva = GameObject.Find("DarkWindow");
        lightCanva = GameObject.Find("LightWindow");
    }

    void Update()
    {
        ChangeColor();
    }

    public void ChangeColor()
    {
        if(isBackend == false )
        {
            darkCanva.SetActive(false);
            lightCanva.SetActive(true);
            title.color = dark;
            content.color = dark;
        }
        else
        {
            darkCanva.SetActive(true);
            lightCanva.SetActive(false);
            title.color = light;
            content.color = light;
        };
    }
}
