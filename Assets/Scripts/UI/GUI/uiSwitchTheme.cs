using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiSwitchTheme : MonoBehaviour
{ 
    [SerializeField] private bool isBackend = false;
    
    public GameObject _darkCanva;
    public GameObject _lightCanva;
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _content;

    [SerializeField] public Color light;
    [SerializeField] public Color dark;
    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if(isBackend == false )
        {
            _darkCanva.SetActive(false);
            _lightCanva.SetActive(true);
            _title.color = dark;
            _content.color = dark;
        }
        else
        {
            _darkCanva.SetActive(true);
            _lightCanva.SetActive(false);
            _title.color = light;
            _content.color = light;
        };
    }
}
