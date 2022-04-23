using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class InteractableBox : MonoBehaviour
{
    private Rigidbody _rb;
    public Rigidbody connectedToPlatformRb;
    private Rigidbody _draggerRb;
    [ShowInInspector, ReadOnly] private Vector3 _velocity;
    public enum BoxState{Normal, OnBox, Drag}
    public BoxState state;
    public float gravity;
    public Vector3 _input;
    public bool disAllowBoxSnap;
    public bool interactionWithPlayerEngaged;
    public StickToBoxPlatform stickBox;
    
    [SerializeField, ExternalPropertyAttributes.ReadOnly] private bool isGrounded;
    
    public bool IsGrounded
    {
        get => isGrounded;
        set
        {
            isGrounded = value;
        }
    }

    private void OnEnable()
    {
        MatrixEntityBehavior.OnMatrixEntityReload += AllowBoxSnap;
    }
    
    private void OnDisable()
    {
        MatrixEntityBehavior.OnMatrixEntityReload -= AllowBoxSnap;
    }


    private void AllowBoxSnap()
    {
        StartCoroutine(coco());
    }

    IEnumerator coco()
    {
        yield return new WaitForSeconds(0.5f);
        disAllowBoxSnap = false;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        stickBox = GetComponentInChildren<StickToBoxPlatform>();
    }
    
    #region Raycast
    public List<Transform> raycastPos;
    [SerializeField] private float rayDistance;
    private void OnDrawGizmos()
    {
        foreach (Transform ray in raycastPos)
        {
           // Gizmos.DrawLine(ray.position, ray.position + ray.transform.up * rayDistance);
        }
    }
    private void CheckRayGrounded()
    {
        RaycastHit hit;
        
        IsGrounded = false;
        
        foreach (Transform ray in raycastPos)
        {
            if (Physics.Raycast(ray.position, ray.up, out hit, rayDistance))
            {
                IsGrounded = true;
            }
        }
    }
    
    #endregion

    public void ConnectSelfToPlatformBox(Rigidbody platformBoxRb)
    {
        if (state == BoxState.Drag) return;
        state = BoxState.OnBox;
        connectedToPlatformRb = platformBoxRb;
    }
    
    public void DisconnectSelfToPlatformBox(Rigidbody platformBoxRb = null)
    {
        if (state == BoxState.Drag) return;
        state = BoxState.Normal;
        connectedToPlatformRb = null;
    }
    
    
    
    private void Update()
    {
        if (disAllowBoxSnap)
        {
            return;
        }
        
        if (state == BoxState.OnBox)
        {
            transform.position = new Vector3(connectedToPlatformRb.transform.position.x, connectedToPlatformRb.transform.position.y + 1.01f,
                connectedToPlatformRb.transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        HandleGrounded();
        switch (state)
        {
            case BoxState.Normal :
                
                CalculateGravity();
                _velocity.x = 0;
                _velocity.z = 0;
                break;
            
            case BoxState.OnBox:
                _rb.isKinematic = true; //was false before? thinking emoji
                
                //transform.position = new Vector3(connectedToPlatformRb.transform.position.x, connectedToPlatformRb.transform.position.y + 1f,
                   // connectedToPlatformRb.transform.position.z);
                
                //var velocity = connectedToPlatformRb.velocity;
                //_velocity = new Vector3(velocity.x,-0.05f,velocity.z);
                break;
            
            case BoxState.Drag:
                print("drag");
                _rb.isKinematic = false; 
                _velocity = _draggerRb.velocity;
                if (!isGrounded && connectedToPlatformRb == null)
                {
                    print("retuend to normal");
                    state = BoxState.Normal;
                }
               
                break;

        }
        
        if (state == BoxState.OnBox) return;

        _rb.velocity = _velocity;
    }

    public void SetDrag(Rigidbody draggerRb)
    {
        _draggerRb = draggerRb;
        state = BoxState.Drag;
    }
    
    public void RemoveDrag(Rigidbody draggerRb)
    {
        _draggerRb = null;
        state = BoxState.Normal;
    }

    private void CalculateGravity()
    {
        if (!IsGrounded)
        {
            _rb.isKinematic = false;
            float previousYVelocity = _velocity.y;
            float newYVelocity = _velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * (0.5f);
            _velocity.y = nextYVelocity;
            _velocity.x = 0;
            _velocity.z = 0;
        }
        else
        {
            _velocity = new Vector3(0,-2f,0);
            _rb.isKinematic = true;
        }
  
    }
    
    [SerializeField] private float groundCheckSphereRadius = 0.3f;
    [SerializeField] private Transform groundCheck;
    public LayerMask whatIsGround;
   
    private void HandleGrounded()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundCheckSphereRadius, whatIsGround);
    }
}
