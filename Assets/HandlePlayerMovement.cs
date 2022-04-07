using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandlePlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float groundCheckSphereRadius = 0.3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    public float gravity;
    
    private Vector3 _input;
    private Vector3 _velocity;
    
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
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        InputManager.Controls.Player.Move.performed += OnMovementPerformed;
    }
    
    private void OnDisable()
    {
        InputManager.Controls.Player.Move.performed -= OnMovementPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleGrounded();
        HandleGravity();
        HandlePlayerInput();
        
        
        rb.velocity = new Vector3(_input.x, 0, _input.y);
    }
    
    private void HandleGrounded()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundCheckSphereRadius, whatIsGround);
    }
    
    private void HandleGravity()
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

    private void HandlePlayerInput()
    {
        _velocity += _input;
    }
}
