using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;
using devziie.Inputs;
using Newtonsoft.Json.Serialization;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(FixedJoint))]
public class HandlePlayerInteractions : MonoBehaviour
{
    private Player _player;
    private PlayerMovementController _playerMovement;
    [ShowInInspector] private bool _interactionEngaged = false;
    private Interactable _interactable;
    private Rigidbody _interactableRb;
    private FixedJoint _fixedJoint;
    
    public List<Transform> raycastPos;
    public List<Transform> raycastBreak;
    public float rayMaxGrabDistance;
    public float rayMaxBreakDistance;

    public static event Action OnPushableInteractionAllowed;
    public static event Action OnPushableInteractionNotAllowed;
    public static event Action OnPushableInteractionStarted;
    public static event Action OnPushableInteractionBreak;
    

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
        _fixedJoint = GetComponent<FixedJoint>();
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
        if (currentInteraction == null || _interactionEngaged)
        {
            print("ds");
            DisengageItem();
        }
        //Le joueur interagit bien avec une boite; faire code bouge avec boite et tout - Justin
        if (interactState == PlayerInteractState.InteractionWithBox)
        {
            _interactable = currentInteraction as Box;
            _interactableRb = _interactable.GetComponent<Rigidbody>();
            EngageItem();
        }
    }

    [SerializeField] private LayerMask layerMask;
    private void OnDrawGizmos()
    {
        if (!_interactionEngaged)
        {
            Gizmos.color = Color.yellow;
            foreach (Transform ray in raycastPos)
            {
                Gizmos.DrawLine(ray.position,ray.position + transform.forward * rayMaxGrabDistance);
            }
        }
        else
        {
            Gizmos.color = Color.green;
            foreach (Transform ray in raycastPos)
            {
                Gizmos.DrawLine(ray.position,ray.position + transform.forward * rayMaxBreakDistance);
            }
        }
    }
    private void FixedUpdate()
    {
        interactState = PlayerInteractState.None;
        
        if (!_interactionEngaged)
        {
            if(!IsConnectedToInteraction(rayMaxGrabDistance, _interactionEngaged, raycastPos)) return;
        }
        else
        {
            if(!IsConnectedToInteraction(rayMaxBreakDistance, _interactionEngaged,raycastPos)) return;
        }

        if (currentInteraction is Box) //Est-ce que l'objet interactif est une boite?
        {
            interactState = PlayerInteractState.InteractionWithBox; //Nouvelle État du Player
        }

        OnPushableInteractionAllowed?.Invoke();
        
        if (!_interactionEngaged) return;
        
    }

    public bool IsConnectedToInteraction(float distance, bool interactionEngaged, List<Transform> raycasts)
    {
        RaycastHit hit;
        Interactable lastInteraction = null;
        currentInteraction = null;
        
        foreach (Transform ray in raycastPos)
        {
            if (Physics.Raycast(ray.position, transform.forward, out hit, distance, layerMask))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    currentInteraction = hit.collider.GetComponent<Interactable>();
                    if (lastInteraction != null)
                    {
                        if (lastInteraction != currentInteraction)
                        {
                            DisengageItem(); //this should only be called when there is a engaged interaction
                            if (interactionEngaged) OnPushableInteractionBreak?.Invoke(); //This is trash its called 3 times please change that emile
                            else OnPushableInteractionNotAllowed?.Invoke();
                            return false;
                        }
                    }
                    lastInteraction = currentInteraction;
                }
                else
                {
                    DisengageItem();
                    if (interactionEngaged) OnPushableInteractionBreak?.Invoke(); //This is trash its called 3 times please change that emile
                    else OnPushableInteractionNotAllowed?.Invoke();
                    return false;
                }
            }
            else
            {
                DisengageItem();
                if (interactionEngaged) OnPushableInteractionBreak?.Invoke(); //This is trash its called 3 times please change that emile
                else OnPushableInteractionNotAllowed?.Invoke();
                return false;
            }
        }

        return true;
    }

    public void DisengageItem()
    {
        OnPushableInteractionBreak?.Invoke();
        _interactionEngaged = false;
        _fixedJoint.connectedBody = null;
    }

    public void EngageItem()
    {
        OnPushableInteractionStarted?.Invoke();
        _interactionEngaged = true;
        _fixedJoint.connectedBody = _interactableRb;
    }

}
