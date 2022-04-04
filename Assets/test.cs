using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
public class test : MonoBehaviour
{
    public PlayerMovementController mov;
    public Vector3 input;

    private void OnEnable()
    {
        InputManager.Controls.Player.Move.performed += OnMovementPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        transform.position = input;
    }

    private void Update()
    {
        
        //transform.position = input;
    }
}
