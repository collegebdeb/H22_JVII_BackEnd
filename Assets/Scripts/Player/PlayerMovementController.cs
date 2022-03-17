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

        [Title("Movement Info")]
        [ReadOnly] public bool isMovementPressed;
        [ReadOnly] public bool isRunPressed;
        
        [Title("Jump Info")]
        [ReadOnly] public bool isJumpPressed;
        [ReadOnly] public bool isJumping = false;
        [ReadOnly] public float initialJumpVelocity;
        public float maxJumpHeight = 1.0f;
        public float maxJumpTime = 0.5f;
        
        [Title("Rotation Speed")]
        public float rotationFactorPerFrame = 30f;

        private Vector2 _previousInput;
        private Vector3 _currentMovement;
        private Vector3 _currentRunMovement;

        private Vector3 camF;
        private Vector3 camR;
        
        private readonly int _isWalkingHash = Animator.StringToHash("isWalking");
        private readonly int _isRunningHash = Animator.StringToHash("isRunning");

        #region Gravity
        [Title("Gravity")]
        public float _gravity = -9.8f;
        float groundedGravity = -0.05f;
        
        [Title("Movement Relative To Camera")]
        public bool relativeCameraMovement;
        
        [ShowIf("relativeCameraMovement")]
        public Transform cam;

        

    

        private void SetupJumpVariables()
        {
            float timeToApex = maxJumpTime / 2;
            _gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        }
        
        #endregion

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
            InputManager.Controls.Player.Move.started += onMovementStarted;
            InputManager.Controls.Player.Move.canceled += ResetMovement;

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
            _currentMovement.x = _previousInput.x;
            _currentMovement.z = _previousInput.y;
            _currentRunMovement = _currentMovement * runMultiplier;
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
                isJumping = true;
                _currentMovement.y = initialJumpVelocity;
                _currentRunMovement.y = initialJumpVelocity;
            }
            else if (!isJumpPressed && isJumping && controller.isGrounded)
            {
                isJumping = false;
            }
        }
        private void HandleGravity()
        {
            if (controller.isGrounded)
            {
                _currentMovement.y = groundedGravity;
                _currentRunMovement.y = groundedGravity;
            }
            else
            {
                _currentMovement.y += _gravity * Time.deltaTime;
                _currentRunMovement.y += _gravity * Time.deltaTime;
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
                if (relativeCameraMovement) targetRotation = Quaternion.LookRotation(positionToLookAt.x * camR + positionToLookAt.z * camF);
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

            camF = cam.forward;
            camR = cam.right;

            camF.y = 0;
            camR.y = 0;
            camF = camF.normalized;
            camR = camR.normalized;

            //_currentMovement = _currentMovement.x * camR + _currentMovement.z * camF;
            //print(_currentMovement.x * camR + _currentMovement.z * camF);
            
            _currentRunMovement = _currentRunMovement.x * camR;
            _currentRunMovement = _currentRunMovement.x * camR;
        }


        #endregion
        
        private void Move()
        {
            if(relativeCameraMovement) controller.Move((_currentMovement.x * camR + _currentMovement.z * camF) * movementSpeed * Time.deltaTime);
            else controller.Move(_currentMovement * movementSpeed * Time.deltaTime);
           
        }
        
        

    }