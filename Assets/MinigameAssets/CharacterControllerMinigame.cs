using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterControllerMinigame : MonoBehaviour
{

    //public CharacterController2D controller;

    public float runSpeed = 10f;
    private Vector2 _moveInput;
    float horizontalMove = 0f;
    bool jump = false;
    [FormerlySerializedAs("GroundCheck")] public Transform groundCheck;
    [FormerlySerializedAs("TouchingGround")] public bool touchingGround;
    bool crouch = false;
    public Rigidbody2D _rbody;
    public LayerMask groundLayer2D;
    public float jumpForce;
    private SpriteRenderer spriteRenderer;
    public Sprite jumpSprite;
    public Sprite idleSprite;
    public Sprite[] moveSprite;
    public float delayAnimation;
    public float timer;
    public int currentFrame;

    // Update is called once per frame
    void Update()
    {
        

       
    }

    private void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        InputManager.Controls.Player.Disable();
        InputManager.Controls.Player2D.Enable();
    }

    private void OnEnable()
    {
        InputManager.Controls.Player2D.Jump.performed += OnJump;
        InputManager.Controls.Player2D.Move.performed += OnMove;
        InputManager.Controls.Player2D.Move.canceled += OnStop;
    }
	
    private void OnDisable()
    {
        InputManager.Controls.Player2D.Jump.performed -= OnJump;
        InputManager.Controls.Player2D.Move.performed -= OnMove;
        InputManager.Controls.Player2D.Move.canceled -= OnStop;
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        
        _moveInput = context.ReadValue<Vector2>();
        if (_moveInput.x < 0)
        {
         
            
            spriteRenderer.flipX = true; 
        }
        if (_moveInput.x > 0)
        {
            
            spriteRenderer.flipX = false; 
        }

        

    }
    private void OnJump(InputAction.CallbackContext context)
    {
        jump = context.ReadValueAsButton();
        

    }

    private void OnStop(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
        spriteRenderer.sprite = idleSprite;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= delayAnimation)
        {
            
            if (currentFrame == 0)
            {
                currentFrame = 1;
            }
            else
            {
                currentFrame = 0;
            }
            timer = 0f;
                
        }

        if (touchingGround == true)
        {
            spriteRenderer.sprite = moveSprite[currentFrame];
        }

        touchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundLayer2D);
        // Move our character
        //controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        //jump = false;

        _rbody.velocity = new Vector2(_moveInput.x * runSpeed, _rbody.velocity.y);
        if (jump == true && touchingGround == true)
        {
            _rbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (touchingGround == false)
        {
            spriteRenderer.sprite = jumpSprite;
        }

        if (_moveInput == Vector2.zero)
        {
            spriteRenderer.sprite = idleSprite;
            if (touchingGround == false)
            {
                spriteRenderer.sprite = jumpSprite;
            }
        }
        

        jump = false;

    }
}