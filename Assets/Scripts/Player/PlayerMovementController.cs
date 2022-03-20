using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using devziie.Inputs;
using UnityEngine.InputSystem;
using UnityEngine;
using Sirenix.OdinInspector;

    public class PlayerMovementController : MonoBehaviour
    {
        [Title("Reference")]
        [SerializeField] private CharacterController controller = null;
        [SerializeField] private Animator animator;

        [Title("Movement")]
        public float runMultiplier = 3f;
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
        
        [Title("Movement Info")]
        [ReadOnly] public bool isMovementPressed;
        [ReadOnly] public bool isRunPressed;
        
        [Title("Jump Info")]
        [ReadOnly] public bool isJumpPressed;
        [ReadOnly] public bool isJumping = false;
        [ReadOnly] public bool isJumpAnimating;
        [ReadOnly] public float initialJumpVelocity;

        private Vector2 _previousInput;
        private Vector3 _currentMovement;
        private Vector3 _currentRunMovement;

        private Vector3 _camF;
        private Vector3 _camR;
        
        private readonly int _isWalkingHash = Animator.StringToHash("isWalking");
        private readonly int _isRunningHash = Animator.StringToHash("isRunning");
        private readonly int _isJumpingHash = Animator.StringToHash("isJumping");

        [Title("Gravity")]
        [SerializeField, ReadOnly] private float gravity = -9.8f;
        [SerializeField, ReadOnly] private float groundedGravity = -0.05f;
        
        public bool allowBasicCollideHit = false;
        
        [ShowIf("allowBasicCollideHit")]
        [SerializeField] private float pushPower = 2.0f;
        public void OnControllerColliderHit(ControllerColliderHit hit)
        {
        if (!allowBasicCollideHit) return;
     
        Rigidbody body = hit.collider.attachedRigidbody;
     

        // no rigidbody
        if (body == null || body.isKinematic) { return; }
 
        if (hit.moveDirection.y < -0.3f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);
     
        // Apply the push
        body.velocity = pushDir * pushPower;
        }

        private void SetupJumpVariables()
        {
            float timeToApex = MaxJumpTime / 2;
            gravity = (-2 * MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            initialJumpVelocity = (2 * MaxJumpHeight) / timeToApex;
        }

        #region UnityBehavior

        private void Update()
        {
            HandleCameraMovement();
            HandleRotation();
            HandleAnimation();
            Move();
            HandleGravity();
            HandleJump();
        }
        
        public void Start()
        {
            InputManager.Controls.Player.Move.performed += OnMovementPerformed;
            //InputManager.Controls.Player.JoystickMove.performed += OnJoystickMovementPerformed;
            InputManager.Controls.Player.Move.started += onMovementStarted;
            //InputManager.Controls.Player.JoystickMove.started += onMovementStarted;
            InputManager.Controls.Player.Move.canceled += ResetMovement;
            //InputManager.Controls.Player.JoystickMove.canceled += ResetMovement;

            InputManager.Controls.Player.Jump.started += OnJump;
            InputManager.Controls.Player.Jump.canceled += OnJump;
        }

        private void Awake()
        {
            if (relativeCameraMovement && cam==null)
            {
                print("No Camera Setup : Using main-camera");
                cam = Camera.main.transform;
            }
            SetupJumpVariables();
        }

        #endregion

        #region Events
        
        private void OnJump(InputAction.CallbackContext context)
        {
            isJumpPressed = context.ReadValueAsButton();
            SoundEvents.onPlayerJump?.Invoke();
        }
        
        private void OnJumpCanceled(InputAction.CallbackContext context)
        {
            isJumpPressed = context.ReadValueAsButton();
            SoundEvents.onPlayerJumpCanceled?.Invoke();
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            isRunPressed = context.ReadValueAsButton();
        }

        private void onMovementStarted(InputAction.CallbackContext context)
        {

        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            _previousInput = context.ReadValue<Vector2>();
            _currentMovement.x = _previousInput.x * movementSpeed;
            _currentMovement.z = _previousInput.y * movementSpeed;
            _currentRunMovement = _currentMovement * movementSpeed * runMultiplier;
            isMovementPressed = true;
        }
        
        private void OnJoystickMovementPerformed(InputAction.CallbackContext context)
        {
            _previousInput = context.ReadValue<Vector2>();
            _currentMovement.x = _previousInput.x * movementSpeed;
            _currentMovement.z = _previousInput.y * movementSpeed;
            _currentRunMovement = _currentMovement * movementSpeed * runMultiplier;
            isMovementPressed = true;
        }

        private void ResetMovement(InputAction.CallbackContext context)
        {
            _previousInput = Vector2.zero;
            _currentMovement = Vector2.zero;
            _currentRunMovement = Vector2.zero;
            isMovementPressed = false;
        }

        #endregion

        #region Handlers

        private void HandleJump()
        {
            if (!isJumping && controller.isGrounded && isJumpPressed)
            {
                animator.SetBool(_isJumpingHash, true);
                isJumpAnimating = true;
                isJumping = true;
                _currentMovement.y = initialJumpVelocity * .5f;
                _currentRunMovement.y = initialJumpVelocity * .5f;
            }
            else if (!isJumpPressed && isJumping && controller.isGrounded)
            {
                isJumping = false;
            }
        }
        private void HandleGravity()
        {
            bool isFalling = _currentMovement.y <= 0.0f || !isJumpPressed;
            
            if (controller.isGrounded)
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


        #endregion
        
        private void Move()
        {
            if (relativeCameraMovement)
            {
                
                controller.Move((_currentMovement.x * _camR + _currentMovement.y * Vector3.up + _currentMovement.z * _camF) * Time.deltaTime);
            }
            else controller.Move(_currentMovement * Time.deltaTime); //Movement speed is affection jump speed (needs to be fixed)
           
        }
        
        

    }