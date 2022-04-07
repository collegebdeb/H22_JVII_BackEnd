using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandlePlayerMovement : MonoBehaviour
{
    [Title("Reference")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    
    [Title("Movement")]
    [SerializeField] private float movementSpeed = 5f;

    [Title("Jump Setting")]
    [SerializeField, OnValueChanged("SetupJumpVariables")] private float maxJumpHeight = 1.0f;
    [SerializeField, OnValueChanged("SetupJumpVariables")] private float maxJumpTime = 0.5f;
    
    public float MaxJumpHeight { 
        get => maxJumpHeight;
        set
        {
            maxJumpHeight = value;
            SetupJumpVariables();
        }
    }
    public float MaxJumpTime
    {
        get => maxJumpTime;

        set{
            maxJumpTime = value;
            SetupJumpVariables();
        }
    }
    
    [SerializeField] private float fallMultiplier = 2f;
    
    [Title("Rotation Speed")]
    public float rotationFactorPerFrame = 30f;
    
    [Title("Movement Relative To Camera")]
    public bool relativeCameraMovement;

    [ShowIf("relativeCameraMovement")]
    public Transform cam;
    
    [FoldoutGroup("Info")]
    [Title("Movement Info")]
    [ReadOnly] public bool isMovementPressed;
    [ReadOnly] public bool isRunPressed;
    
    [FoldoutGroup("Info")]
    [Title("Jump Info")]
    [ReadOnly] public bool isJumpPressed;
    [FoldoutGroup("Info")]
    [ReadOnly] public bool isJumping = false;
    [FoldoutGroup("Info")]
    [ReadOnly] public bool isJumpAnimating;
    [FoldoutGroup("Info")]
    [ReadOnly] public float initialJumpVelocity;
    
    [FoldoutGroup("Info")]
    [Title("Gravity")]
    [SerializeField, ReadOnly] private float gravity = -9.8f;
    [SerializeField, ReadOnly] private float groundedGravity = -0.05f;

    private Vector2 _previousInput;
    public Vector3 _currentMovement;
    private Vector3 _currentRunMovement;
    
    private Vector3 _camF;
    private Vector3 _camR;
    
    private readonly int _isWalkingHash = Animator.StringToHash("isWalking");
    private readonly int _isRunningHash = Animator.StringToHash("isRunning");
    private readonly int _isJumpingHash = Animator.StringToHash("isJumping");
    
    [SerializeField] private float groundCheckSphereRadius = 0.3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    
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
        if (relativeCameraMovement && cam==null)
        {
            print("No Camera Setup : Using main-camera");
            cam = Camera.main.transform;
        }
        SetupJumpVariables();
    }
    private void OnEnable()
    {
        InputManager.Controls.Player.Move.performed += OnMovementPerformed;
        InputManager.Controls.Player.Move.canceled += ResetMovement;
        
        InputManager.Controls.Player.Jump.started += OnJump;
        InputManager.Controls.Player.Jump.canceled += OnJump;
    }
    
    private void OnDisable()
    {
        InputManager.Controls.Player.Move.performed -= OnMovementPerformed;
        InputManager.Controls.Player.Move.canceled -= ResetMovement;
        
        InputManager.Controls.Player.Jump.started -= OnJump;
        InputManager.Controls.Player.Jump.canceled -= OnJump;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        _previousInput = context.ReadValue<Vector2>();

        _currentMovement.x = _previousInput.x * movementSpeed;
        _currentMovement.z = _previousInput.y * movementSpeed;
        //_currentRunMovement = _currentMovement * movementSpeed * runMultiplier;
        isMovementPressed = true;
    }
    
    private void ResetMovement(InputAction.CallbackContext context)
    {
        _previousInput = Vector2.zero;
        _currentMovement = Vector2.zero;
        _currentRunMovement = Vector2.zero;
        isMovementPressed = false;
    }
    
    private void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        SoundEvents.onPlayerJump?.Invoke(AudioList.Sound.OnPlayerJump, gameObject);
    }
    
    private void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void HandleJump()
    {
        if (!isJumping && IsGrounded && isJumpPressed)
        {
            animator.SetBool(_isJumpingHash, true);
            isJumpAnimating = true;
            isJumping = true;
            _currentMovement.y = initialJumpVelocity * .5f;
            _currentRunMovement.y = initialJumpVelocity * .5f;
        }
        else if (!isJumpPressed && isJumping && IsGrounded)
        {
            isJumping = false;
        }
    }
    
    private void FixedUpdate()
    {
        HandleGrounded();
        HandleGravity();
       // HandlePlayerInput();


        rb.velocity = _currentMovement;
    }
    
    private void HandleGrounded()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundCheckSphereRadius, whatIsGround);
    }
    
    private void HandleGravity()
    {
        bool isFalling = _currentMovement.y <= 0.0f || !isJumpPressed;
            
        if (IsGrounded)
        {
            if (isJumpAnimating)
            {
                animator.SetBool(_isJumpingHash, false);
                isJumpAnimating = false;
            }
            _currentMovement.y = groundedGravity;
            _currentRunMovement.y = groundedGravity;
        } else if (isFalling)
        {
            float previousYVelocity = _currentMovement.y;
            float newYVelocity = _currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            _currentMovement.y = nextYVelocity;
            _currentRunMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = _currentMovement.y;
            float newYVelocity = _currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity)* (0.5f);
            _currentMovement.y = nextYVelocity;
            _currentRunMovement.y = nextYVelocity;
        }
    }
    
    private void HandleRotation()
    {
        Vector3 positionToLookAt;
           
        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = _currentMovement.z;
            
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation;
            if (relativeCameraMovement) targetRotation = Quaternion.LookRotation(positionToLookAt.x * _camR + positionToLookAt.z * _camF);
            else targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }
    private void HandleAnimation()
    {
        bool isWalking = animator.GetBool(_isWalkingHash);
        bool isRunning = animator.GetBool(_isRunningHash);

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(_isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(_isWalkingHash, false);
        }

        #region Running - Not In Use

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(_isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(_isRunningHash, false);
        }
        #endregion
    }
    
    private void HandleCameraMovement()
    {
        if (!relativeCameraMovement) return;

        _camF = cam.forward;
        _camR = cam.right;

        _camF.y = 0;
        _camR.y = 0;
        _camF = _camF.normalized;
        _camR = _camR.normalized;

        //_currentMovement = _currentMovement.x * camR + _currentMovement.z * camF;
        //print(_currentMovement.x * camR + _currentMovement.z * camF);
            
    }
    
    private void SetupJumpVariables()
    {
        float timeToApex = MaxJumpTime / 2;
        gravity = (-2 * MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * MaxJumpHeight) / timeToApex;
    }
    
    private void Move()
    {
        if (relativeCameraMovement)
        {
           // controller.Move((_currentMovement.x * _camR + _currentMovement.y * Vector3.up + _currentMovement.z * _camF) * Time.deltaTime);
        }
        else
        {
            //controller.Move(_currentMovement *
                            //Time.deltaTime); //Movement speed is affection jump speed (needs to be fixed)
        }
    }
           
    }
    
    
    
    

