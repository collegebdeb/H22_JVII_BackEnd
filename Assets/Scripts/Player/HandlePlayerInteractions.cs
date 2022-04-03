using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using devziie.Inputs;
using Newtonsoft.Json.Serialization;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Player)), RequireComponent(typeof(PlayerMovementController))]
public class HandlePlayerInteractions : MonoBehaviour
{
    private Player _player;
    private PlayerMovementController _playerMovement;
    [ShowInInspector] private bool _interactionEngaged = false;
    private Interactable _interactable;
    private Rigidbody _interactableRb;

    public static event Action onPushableStartInteraction;
    public static event Action onPushableExitInteraction;
    
    public enum PlayerInteractState {None, InteractionWithBox}

    //Etat d'interaction du joueur
    public PlayerInteractState interactState;

    //Avec quoi le joueur est t'il en train d'interagir? Peut être un pickable comme une fleur ou quelque chose comme une boite
    public Interactable currentInteraction;
    
    //Get le player
    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovementController>();
    }

    public void OnEnable()
    {
        //Lit la fonction OnPlayerTryInteract si le joueur cliquer sur le bouton interact
        InputManager.Controls.Player.Interact.started += OnPlayerTryInteract;
        InputManager.Controls.Player.Jump.started += OnJump;
    }
    public void OnDisable()
    {
        InputManager.Controls.Player.Interact.started -= OnPlayerTryInteract;
    }
    
    private void OnJump(InputAction.CallbackContext context)
    {
        DisengageItem();
    }
    
    private void OnPlayerTryInteract(InputAction.CallbackContext context)
    {
        if (currentInteraction == null)
        {
            DisengageItem();
        }
        //Le joueur interagit bien avec une boite; faire code bouge avec boite et tout - Justin
        if (interactState == PlayerInteractState.InteractionWithBox)
        {
            _interactable = currentInteraction as Box;
            _interactableRb = _interactable.GetComponent<Rigidbody>();
            _interactionEngaged = true;
        }
        
    }

    public Vector3 interactionPushDir;
    
    private void Update()
    {
        if (!_interactionEngaged) return;

        switch (interactState)
        {
            case PlayerInteractState.InteractionWithBox :
            {
                interactionPushDir = _playerMovement.GetComponent<CharacterController>().velocity;
                //_interactable.transform.Translate(_interactionPushDir * Time.deltaTime); 
                break;
            }
        }
    }

    public Transform raycastPos;
    public float rayMaxGrabDistance;
    [SerializeField] private LayerMask layerMask;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastPos.transform.position,raycastPos.transform.position + transform.forward * rayMaxGrabDistance);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(raycastPos.position, transform.forward, out hit, rayMaxGrabDistance, layerMask))
        {
            Debug.DrawRay(raycastPos.transform.position, transform.forward * hit.distance, Color.yellow, layerMask);

            if (hit.collider.CompareTag("Interactable"))
            {
                print("boxcollide");
                currentInteraction = hit.collider.GetComponent<Interactable>();

                if (currentInteraction is Box) //Est-ce que l'objet interactif est une boite?
                {
                    interactState = PlayerInteractState.InteractionWithBox; //Nouvelle État du Player
                }
            }
        }
        else
        {
            interactState = PlayerInteractState.None;
            currentInteraction = null;
        }

        if (!_interactionEngaged) return;
        print("move");
        //_interactableRb.MovePosition(_interactableRb.transform.position + interactionPushDir * Time.fixedDeltaTime);
        //_interactableRb.MovePosition(_interactableRb.transform.position * Time.fixedDeltaTime + raycastPos.position + transform.forward * rayMaxGrabDistance);
        //_interactableRb.position = transform.position + transform.forward * rayMaxGrabDistance;
    }

    public void DisengageItem()
    {
        _interactionEngaged = false;
    }

    IEnumerator PushBox(Box box, Rigidbody boxRb)
    {
        boxRb.MovePosition(_playerMovement._currentMovement * Time.deltaTime);
        yield return new WaitForFixedUpdate();
    }
    

}
