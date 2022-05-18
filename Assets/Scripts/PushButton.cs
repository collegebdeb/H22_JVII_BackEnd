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
        HandlePlayerBoxInteraction.OnPushableInteractionAllowed += ShowButton;
        HandlePlayerBoxInteraction.OnPushableInteractionNotAllowed += HideButton;
        HandlePlayerFlowerInteraction.OnInteractionAllowed += ShowButton;
        HandlePlayerFlowerInteraction.OnInteractionNotAllowed += HideButton;
    }
    
    private void OnDisable()
    {
        HandlePlayerBoxInteraction.OnPushableInteractionAllowed -= ShowButton;
        HandlePlayerBoxInteraction.OnPushableInteractionNotAllowed -= HideButton;
        HandlePlayerFlowerInteraction.OnInteractionAllowed -= ShowButton;
        HandlePlayerFlowerInteraction.OnInteractionNotAllowed -= HideButton;
    }

    private void ShowButton()
    {
        if (GameManager.i.playerReal.movement.isJumping) return;
        _image.enabled = true;
    }
    
    private void HideButton()
    {
        _image.enabled = false;
    }
    
}
