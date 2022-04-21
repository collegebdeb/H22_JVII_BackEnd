using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerMinigame : MonoBehaviour
{

    //public CharacterController2D controller;

    public float runSpeed = 10f;
    private Vector2 _moveInput;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public Rigidbody2D _rbody;

    // Update is called once per frame
    void Update()
    {
        

       
    }

    private void Start()
    {
        InputManager.Controls.Player.Disable();
        InputManager.Controls.Player2D.Enable();
    }

    private void OnEnable()
    {
        InputManager.Controls.Player2D.Jump.performed += OnJump;
        InputManager.Controls.Player2D.Move.performed += OnMove;
    }
	
    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

    }
    private void OnJump(InputAction.CallbackContext context)
    {
        jump = context.ReadValueAsButton();
        
    }

    void FixedUpdate()
    {
        // Move our character
        //controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        //jump = false;

        _rbody.velocity = new Vector2(_moveInput.x * runSpeed, _rbody.velocity.y);
        if (jump == true)
        {
            _rbody.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
        }

        jump = false;

    }
}