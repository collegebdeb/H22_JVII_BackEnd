using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableBox : MonoBehaviour
{
    private Rigidbody _rb;
    public Rigidbody connectedToPlatformRb;
    [ShowInInspector, ReadOnly] private Vector3 _velocity;
    public enum BoxState{Normal, OnBox, Drag}
    public BoxState state;
    public float gravity;
    public bool testMovement;
    

    public Vector3 _input;
    
    [SerializeField, ExternalPropertyAttributes.ReadOnly] private bool isGrounded;
    public bool IsGrounded
    {
        get => isGrounded;
        set
        {
            isGrounded = value;
        }
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.Controls.Player.Move.performed += OnMovementPerformed;
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
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
        state = BoxState.OnBox;
        connectedToPlatformRb = platformBoxRb;
    }
    
    public void DisconnectSelfToPlatformBox(Rigidbody platformBoxRb)
    {
        state = BoxState.Normal;
        connectedToPlatformRb = null;
    }

    private void FixedUpdate()
    {
        
        switch (state)
        {
            case BoxState.Normal :
                HandleGrounded();
                CalculateGravity();
                break;
            
            case BoxState.OnBox:
                _velocity = new Vector3(connectedToPlatformRb.velocity.x,-0.05f,connectedToPlatformRb.velocity.z);
                break;
            
        }
        
        if (testMovement)
        {
            _velocity.x = _input.x;
            _velocity.z = _input.y;
        }
        
        _rb.velocity = _velocity;
    }

    private void CalculateGravity()
    {
        if (!IsGrounded)
        {
            float previousYVelocity = _velocity.y;
            float newYVelocity = _velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * (0.5f);
            _velocity.y = nextYVelocity;
        }
        else
        {
            _velocity = new Vector3(0,-0.05f,0);
        }
  
    }
    
    [SerializeField] private float groundCheckSphereRadius = 0.3f;
    [SerializeField] private Transform groundCheck;
    public LayerMask layerMask;
   
    private void HandleGrounded()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundCheckSphereRadius, layerMask);
    }
}
