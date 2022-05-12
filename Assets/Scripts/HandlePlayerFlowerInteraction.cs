using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class HandlePlayerFlowerInteraction : MonoBehaviour
{ 
    public enum PlayerInteractState {None, FlowerOnTopHead, FlowerToFloor}
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
            if (interactState == PlayerInteractState.FlowerToFloor) return;
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
        if (interactState == PlayerInteractState.None)
        {
            if (playerCloseToFlower)
            {
                interactState = PlayerInteractState.FlowerOnTopHead;
                StartCoroutine(SnapToHead());
            }
        } else if (interactState == PlayerInteractState.FlowerOnTopHead)
        {
            if (GameManager.i.playerReal.movement.connectedToPlatform) return;
            interactState = PlayerInteractState.FlowerToFloor;

            StartCoroutine(SnapToFloor());
            print("snap");
           
           

        }
    }
    
    IEnumerator SnapToFloor()
    {
        InputManager.Controls.Player.Disable();
        currentFlower.transform.DOJump(GameManager.i.playerReal.transform.position + transform.forward * 2f,0.8f,0,1f);
        print(GameManager.i.playerReal.flowerDropPos.position);
        yield return new WaitForSeconds(0.5f);
        InputManager.Controls.Player.Enable();
        yield return new WaitForSeconds(0.2f);
        interactState = PlayerInteractState.None;
    }

    IEnumerator SnapToHead()
    {
        InputManager.Controls.Player.Disable();
        currentFlower.transform.DOJump(GameManager.i.playerReal.flowerLockPos.position,0.5f,1,0.5f);
        yield return new WaitForSeconds(0.5f);
        InputManager.Controls.Player.Enable();
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