using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandlePlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private Vector3 _input;
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
      rb.velocity = new Vector3(_input.x, 0, _input.y);
    }
}
