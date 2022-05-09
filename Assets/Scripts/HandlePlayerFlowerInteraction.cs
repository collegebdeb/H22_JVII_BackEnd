using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class HandlePlayerFlowerInteraction : MonoBehaviour
{ 
    public enum PlayerInteractState {None, FlowerOnTopHead}
    public PlayerInteractState interactState;
    
    public static event Action OnInteractionAllowed;
    public static event Action OnInteractionNotAllowed;

    public bool playerCloseToFlower = false;
    private Flower currentFlower;
    private void OnEnable()
    {
        InputManager.Controls.Player.Interact.started += OnPlayerTryInteract;
        Flower.OnProximity += HandleProximity;
    }
    
    private void OnDisable()
    {
        InputManager.Controls.Player.Interact.started -= OnPlayerTryInteract;
        Flower.OnProximity -= HandleProximity;
    }

    private void HandleProximity(bool close, Flower flower)
    {
        if (close)
        {
            currentFlower = flower;
            OnInteractionAllowed?.Invoke();
            playerCloseToFlower = true;
        }
        else
        {
            OnInteractionNotAllowed?.Invoke();
            playerCloseToFlower = false;
        }
    }
    
    private void OnPlayerTryInteract(InputAction.CallbackContext context)
    {
        interactState = PlayerInteractState.FlowerOnTopHead;
        if (interactState == PlayerInteractState.None)
        {
            if (playerCloseToFlower)
            {
                
                StartCoroutine(SnapToHead());
            }
        }
    }

    IEnumerator SnapToHead()
    {
        yield return new WaitForSeconds(1f);
        print("snap");
        currentFlower.transform.position = GameManager.i.playerReal.flowerLockPos.position;
        
    }

    private void Update()
    {
        switch (interactState)
        {
            case PlayerInteractState.FlowerOnTopHead :
                currentFlower.transform.position = GameManager.i.playerReal.flowerLockPos.position;
                break;
        }
    }
}
