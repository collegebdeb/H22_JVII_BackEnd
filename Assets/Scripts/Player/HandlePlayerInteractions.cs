using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using devziie.Inputs;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Player))]
public class HandlePlayerInteractions : MonoBehaviour
{
    private Player _player;
    private PlayerMovementController _playerMovement;
    [ShowInInspector] private bool _interactionEngaged = false;
    private Interactable _interactable;
    private Rigidbody _interactableRb;
    private BoxCollider interactCollider;
    private FixedJoint _fixedJoint;

    public List<Transform> raycastPos;
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
        SafeZoneCollider.OnPlayerEnteredSafeZone += DisengageItem;
    }
    public void OnDisable()
    {
        InputManager.Controls.Player.Interact.started -= OnPlayerTryInteract;
    }
    
    private void OnJump(InputAction.CallbackContext context)
    {
        DisengageItem();
    }

    private bool _engageItem;
    private bool _disEngageItem;
    private void OnPlayerTryInteract(InputAction.CallbackContext context)
    {
        if (_interactionEngaged)
        {
            _disEngageItem = true;
            return;
        }
        //Le joueur interagit bien avec une boite; faire code bouge avec boite et tout - Justin
        if (interactState == PlayerInteractState.InteractionWithBox)
        {
            _interactable = currentInteraction as Box;
            _interactableRb = _interactable.GetComponent<Rigidbody>();
            interactCollider = _interactable.GetComponent<BoxCollider>();
            _engageItem = true;
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

        if (!_interactionEngaged)
        {
            if (!IsConnectedToInteraction(rayMaxGrabDistance, _interactionEngaged, raycastPos))
            {
                interactState = PlayerInteractState.None;
                return;
            }
        }
        else
        {
            if (!_interactable.IsGrounded)
            {
                DisengageItem();
                return;
            }
            
            if (!IsConnectedToInteraction(rayMaxBreakDistance, _interactionEngaged, raycastPos))
            {
                DisengageItem();
                return;
            }
        }

        if (currentInteraction is Box) //Est-ce que l'objet interactif est une boite?
        {
            interactState = PlayerInteractState.InteractionWithBox; //Nouvelle État du Player
        }

        OnPushableInteractionAllowed?.Invoke();

        if (_engageItem)
        {
            _engageItem = false;
            EngageItem();
        }

        if (_disEngageItem)
        {
            _disEngageItem = false;
            DisengageItem();
        }
        
        
        if (!_interactionEngaged) return;
        
        
    }

    public bool IsConnectedToInteraction(float distance, bool interactionEngaged, List<Transform> raycasts)
    {
       
        Interactable lastInteraction = null;
        
        foreach (Transform ray in raycastPos)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray.position, transform.forward, out hit, distance, layerMask))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    currentInteraction = hit.collider.GetComponent<Interactable>();
                    if (lastInteraction != null)
                    {
                        if (lastInteraction != currentInteraction)
                        {
                            print("Not the same box");
                            //this should only be called when there is a engaged interaction
                            if (interactionEngaged) OnPushableInteractionBreak?.Invoke(); //This is trash its called 3 times please change that emile
                            else OnPushableInteractionNotAllowed?.Invoke();
                            
                            return false;
                        }
                    }
                    lastInteraction = currentInteraction;
                }
                else
                {
                    print("Not a interactable collider. Collider is " + hit.collider.gameObject.name);
                    if (interactionEngaged) OnPushableInteractionBreak?.Invoke(); //This is trash its called 3 times please change that emile
                    else OnPushableInteractionNotAllowed?.Invoke();
                    return false;
                }
            }
            else
            {
               //print("RayCast not touching anything");
                if (interactionEngaged) OnPushableInteractionBreak?.Invoke(); //This is trash its called 3 times please change that emile
                else OnPushableInteractionNotAllowed?.Invoke();
                return false;
            }
        }

        return true;
    }

    public void DisengageItem()
    {
        InputManager.Controls.Player.Jump.Enable();
        interactCollider.size = new Vector3(0.8f, interactCollider.size.y, 0.8f);
        _interactableRb.velocity = Vector3.zero;
        _interactableRb.angularVelocity = Vector3.zero;

        Physics.IgnoreLayerCollision(9,11,false);
        Physics.IgnoreLayerCollision(2,11,false);
        _interactionEngaged = false;
        OnPushableInteractionBreak?.Invoke();
  
        _fixedJoint.connectedBody = null;
        _interactableRb.velocity = Vector3.zero;
        _interactableRb.angularVelocity = Vector3.zero;
        
        interactState = PlayerInteractState.None;
    }
    
    public void EngageItem()
    {
        InputManager.Controls.Player.Jump.Disable();
        interactCollider.size = new Vector3(1.1f, interactCollider.size.y, 1.1f);
        Physics.IgnoreLayerCollision(9,11,true);
        Physics.IgnoreLayerCollision(2,11,true);
        OnPushableInteractionStarted?.Invoke();
        _interactionEngaged = true;
        _fixedJoint.connectedBody = _interactableRb;
        _interactableRb.velocity = Vector3.zero;
        _interactableRb.angularVelocity = Vector3.zero;
    }

}
