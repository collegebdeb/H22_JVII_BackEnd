using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class PushButton : MonoBehaviour
{
    private Image _image;
    private void Start()
    {
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    private void OnEnable()
    {
        HandlePlayerInteractions.OnPushableInteractionAllowed += ShowButton;
        HandlePlayerInteractions.OnPushableInteractionNotAllowed += HideButton;
    }
    
    private void OnDisable()
    {
        HandlePlayerInteractions.OnPushableInteractionAllowed -= ShowButton;
        HandlePlayerInteractions.OnPushableInteractionNotAllowed -= HideButton;
    }

    private void ShowButton()
    {
        _image.enabled = true;
    }
    
    private void HideButton()
    {
        _image.enabled = false;
    }
    
}
