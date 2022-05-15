using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using devziie.Inputs;
using Mystery.Graphing;
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

    [SerializeField] private float interactionMovementSpeed = 2f;

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
    private float _cachedRotationFactorPerFrame;
    
    [Title("Movement Relative To Camera")]
    public bool relativeCameraMovement;

    [ShowIf("relativeCameraMovement")]
    public Transform cam;
    
    [FoldoutGroup("Info")]
    [Title("Movement Info")]
    [ReadOnly] public bool isMovementPressed;
    [FoldoutGroup("Info")]
    [FoldoutGroup("Info")]
    [ReadOnly] public bool isRunPressed;
    [FoldoutGroup("Info")]
    [ReadOnly] public float _cachedMovementSpeed;
    
    [FoldoutGroup("Info")]
    [Title("Jump Info")]
    [ReadOnly] public bool isJumpPressed;
    [FoldoutGroup("Info")]
    [ReadOnly] public bool isJumping = false;
    [FoldoutGroup("Info")]
    [ReadOnly] public bool isJumpAnimating;
    [FoldoutGroup("Info")]
    [ReadOnly] public float initialJumpVelocity;
    [FoldoutGroup("Info")] public bool isFalling;
    
    [FoldoutGroup("Info")]
    [Title("Gravity")]
    [SerializeField, ReadOnly] private float gravity = -9.8f;
    [FoldoutGroup("Info")]
    [SerializeField, ReadOnly] private float groundedGravity = -0.05f;

    [FoldoutGroup("Info")]
    public Vector2 previousInput;
    [FoldoutGroup("Info")]
    public Vector3 _currentMovement;
    [FoldoutGroup("Info")] 
    private Vector3 _currentRunMovement;
    
    [ShowInInspector] private Vector3 _camF;
    [ShowInInspector] private Vector3 _camR;
    
    private readonly int _isWalkingHash = Animator.StringToHash("isWalking");
    private readonly int _isRunningHash = Animator.StringToHash("isRunning");
    private readonly int _isJumpingHash = Animator.StringToHash("isJumping");
    private readonly int _velocityXHash = Animator.StringToHash("VelocityX");
    private readonly int _velocityYHash = Animator.StringToHash("VelocityY");

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

    public bool connectedToPlatform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (relativeCameraMovement && cam==null)
        {
            print("No Camera Setup : Using main-camera");
            cam = Camera.main.transform;
        }
        SetupJumpVariables();
        _cachedRotationFactorPerFrame = rotationFactorPerFrame;
        _cachedMovementSpeed = movementSpeed;
    }
    private void OnEnable()
    {
        InputManager.Controls.Player.Move.performed += OnMovementPerformed;
        InputManager.Controls.Player.Move.canceled += ResetMovement;
        
        InputManager.Controls.Player.Jump.started += OnJump;
        InputManager.Controls.Player.Jump.canceled += OnJump;

        InputManager.Controls.Player.Lock.started += LockPlayer;
        InputManager.Controls.Player.Lock.canceled += LockPlayer;

        HandlePlayerBoxInteraction.OnPushableInteractionAllowed += LockRotation;
        HandlePlayerBoxInteraction.OnPushableInteractionNotAllowed += FreeRotation;

        HandlePlayerBoxInteraction.OnPushableInteractionStarted += SetInteractionMovementSpeed;
        HandlePlayerBoxInteraction.OnPushableInteractionBreak += SetNormalMovementSpeed;
    }
    
    private void OnDisable()
    {
        InputManager.Controls.Player.Move.performed -= OnMovementPerformed;
        InputManager.Controls.Player.Move.canceled -= ResetMovement;
        
        InputManager.Controls.Player.Jump.started -= OnJump;
        InputManager.Controls.Player.Jump.canceled -= OnJump;
        
        HandlePlayerBoxInteraction.OnPushableInteractionAllowed -= LockRotation;
        HandlePlayerBoxInteraction.OnPushableInteractionNotAllowed -= FreeRotation;

        HandlePlayerBoxInteraction.OnPushableInteractionStarted -= SetInteractionMovementSpeed;
        HandlePlayerBoxInteraction.OnPushableInteractionBreak -= SetNormalMovementSpeed;
    }

    public Rigidbody connectedRbPlatform;

    public void ConnectToPlatform(Rigidbody rb, bool connected)
    {
        connectedToPlatform = connected;
        connectedRbPlatform = rb;
    }

    private void LockRotation()
    {
        rotationFactorPerFrame = 0;
        var vec = transform.eulerAngles;
        vec.x = transform.rotation.x;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = transform.rotation.z;
        transform.eulerAngles = vec;
    }
    
    private void FreeRotation()
    {
        rotationFactorPerFrame = _cachedRotationFactorPerFrame;
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        previousInput = context.ReadValue<Vector2>();

        float animX = (previousInput.x * transform.forward.z) * Mathf.Cos(Vector2.Angle(-_camF,transform.right) +45) - 
                      (previousInput.y) * Mathf.Sin(Vector2.Angle(-_camF,transform.right) +45);
        float animY = (previousInput.x * transform.forward.z) * Mathf.Sin(Vector2.Angle(-_camF,transform.right) +45) + 
                      (previousInput.y * transform.right.z)* Mathf.Cos(Vector2.Angle(-_camF,transform.right) +45);

        DebugGraph.Log(transform.right.x, Color.red);
        if ( transform.right.x > -0.1f  && transform.right.x < 0.1f && transform.forward.x >0.9f && transform.forward.x < 1.1f)
        {
            animX = -animX;
            DebugGraph.Log(true);

        }
        
        animator.SetFloat(_velocityXHash, -animX);
        animator.SetFloat(_velocityYHash, animY);
        //animator.SetFloat(_velocityYHash, previousInput.y * _camF.x);
        DebugGraph.Log(transform.forward.z);
        _currentMovement.x = previousInput.x * movementSpeed;
        _currentMovement.z = previousInput.y * movementSpeed;
        //_currentRunMovement = _currentMovement * movementSpeed * runMultiplier;
        isMovementPressed = true;
    }
    
    private void ResetMovement(InputAction.CallbackContext context)
    {
        previousInput = Vector2.zero;
        animator.SetFloat(_velocityXHash, previousInput.x);
        //animator.SetFloat(_velocityYHash, previousInput.y);
        _currentMovement = Vector2.zero;
        _currentRunMovement = Vector2.zero;
        isMovementPressed = false;
    }
    
    private void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        //SoundEvents.onPlayerJump?.Invoke(AudioList.Sound.OnPlayerJump, gameObject);
    }
    
    private void LockPlayer(InputAction.CallbackContext context)
    {
        LockPlayerToBox = context.ReadValueAsButton();
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
    
    private void SetInteractionMovementSpeed()
    {
        movementSpeed = interactionMovementSpeed;
    }
        
    private void SetNormalMovementSpeed()
    {
        movementSpeed = _cachedMovementSpeed;
        _currentMovement.x = previousInput.x * movementSpeed;
        _currentMovement.z = previousInput.y * movementSpeed;
        //_currentRunMovement = _currentMovement * movementSpeed * runMultiplier;
    }
    
    private void FixedUpdate()
    {
        HandleGrounded();
        HandleCameraMovement();
        HandleRotation();
        HandleAnimation();
        HandleGravity();
        HandleJump();
        Move();
       
    }
    
    #region Handler
    
    private void HandleGrounded()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundCheckSphereRadius, whatIsGround);
    }
    private void HandleGravity()
    {
        
        isFalling = _currentMovement.y <= 0.0f || !isJumpPressed;
            
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
            float newYVelocity = _currentMovement.y + (gravity * fallMultiplier * Time.fixedDeltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            _currentMovement.y = nextYVelocity;
            _currentRunMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = _currentMovement.y;
            float newYVelocity = _currentMovement.y + (gravity * Time.fixedDeltaTime);
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
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.fixedDeltaTime);
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

        return;
        
        /**
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
        **/
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
    
    #endregion
    private void SetupJumpVariables()
    {
        float timeToApex = MaxJumpTime / 2;
        gravity = (-2 * MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * MaxJumpHeight) / timeToApex;
    }

    private bool LockPlayerToBox;
    
    private void Move()
    {
        if (connectedToPlatform)
        {
            if (LockPlayerToBox)
            {
                    
                rb.MovePosition(new Vector3(connectedRbPlatform.transform.position.x, transform.position.y, connectedRbPlatform.transform.position.z));
                return;
            }
        }
        
        if (relativeCameraMovement)
        {
            rb.velocity = (_currentMovement.x * _camR + _currentMovement.y * Vector3.up +
                                      _currentMovement.z * _camF) * Time.fixedDeltaTime;
            // controller.Move((_currentMovement.x * _camR + _currentMovement.y * Vector3.up + _currentMovement.z * _camF) * Time.deltaTime);
        }
        else
        {
            rb.velocity = _currentMovement * Time.fixedDeltaTime;
            // rb.MovePosition(transform.position + _currentMovement * Time.fixedDeltaTime);
            //controller.Move(_currentMovement *
            //Time.deltaTime); //Movement speed is affection jump speed (needs to be fixed)
        }
    }
           
    }
    
    
    
    

