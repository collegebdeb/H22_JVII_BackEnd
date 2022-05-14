using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiSwitchTheme : MonoBehaviour
{ 
    [SerializeField] private bool isBackend = false;
    
    public GameObject darkCanva;
    public GameObject lightCanva;
    public TextMeshProUGUI title;
    public TextMeshProUGUI content;

    [SerializeField] public Color light;
    [SerializeField] public Color dark;
    void Update()
    {
        isBackend = (MatrixManager.worldState == MatrixManager.WorldState.Real);
        ChangeColor();
    }

    private void ChangeColor()
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
