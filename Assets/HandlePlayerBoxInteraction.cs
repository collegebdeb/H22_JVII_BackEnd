using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class HandlePlayerBoxInteraction : MonoBehaviour
{
    private Player _player;
    private HandlePlayerMovement _playerMovement;
    private Rigidbody rb;
    
    private InteractableBox _interactable;
    private Rigidbody _interactableRb;
    
    public List<Transform> raycastPos;
    public float rayMaxGrabDistance;
    public float rayMaxBreakDistance;
    [SerializeField] private LayerMask layerMask;

    public static event Action OnPushableInteractionAllowed;
    public static event Action OnPushableInteractionNotAllowed;
    public static event Action OnPushableInteractionStarted;
    public static event Action OnPushableInteractionBreak;
    
    public enum PlayerInteractState {None, InteractionWithBox}
    public PlayerInteractState interactState;

    public InteractableBox currentInteraction;
    [ShowInInspector] private bool _interactionEngaged = false;
    private bool _engageItem;
    private bool _disEngageItem;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<HandlePlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.Controls.Player.Interact.started += OnPlayerTryInteract;
    }
    
    private void OnDisable()
    {
        InputManager.Controls.Player.Interact.started -= OnPlayerTryInteract;
    }
    
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
            _interactable = currentInteraction as InteractableBox;
            _interactableRb = _interactable.GetComponent<Rigidbody>();
            //interactCollider = _interactable.GetComponent<BoxCollider>();
            _engageItem = true;
        }
    }

    #region RayCast Debug

    
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

    #endregion
    
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
            if (!_interactable.IsGrounded && _interactable.state == InteractableBox.BoxState.Normal)
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

        if (currentInteraction is InteractableBox) //Est-ce que l'objet interactif est une boite?
        {
            interactState = PlayerInteractState.InteractionWithBox; //Nouvelle État du Player
        }

        OnPushableInteractionAllowed?.Invoke();

        if (_engageItem)
        {
            _engageItem = false;
            if (!_player.movement.IsGrounded) return;
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
       
        InteractableBox lastInteraction = null;
        
        foreach (Transform ray in raycastPos)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray.position, transform.forward, out hit, distance, layerMask))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    currentInteraction = hit.collider.GetComponent<InteractableBox>();
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
//                print("RayCast not touching anything");
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
        //interactCollider.size = new Vector3(0.8f, interactCollider.size.y, 0.8f);

        //Physics.IgnoreLayerCollision(9,11,false);
        //Physics.IgnoreLayerCollision(2,11,false);
        _interactionEngaged = false;
        _interactable.RemoveDrag(rb);
        OnPushableInteractionBreak?.Invoke();
        interactState = PlayerInteractState.None;
    }
    
    public void EngageItem()
    {
        InputManager.Controls.Player.Jump.Disable();
        //interactCollider.size = new Vector3(1.1f, interactCollider.size.y, 1.1f);
        //Physics.IgnoreLayerCollision(9,11,true);
        //Physics.IgnoreLayerCollision(2,11,true);
        OnPushableInteractionStarted?.Invoke();
        _interactionEngaged = true;
        //_fixedJoint.connectedBody = _interactableRb;
        
        _interactable.SetDrag(rb);
        _interactableRb.velocity = Vector3.zero;
        _interactableRb.angularVelocity = Vector3.zero;
    }
    
    
}
