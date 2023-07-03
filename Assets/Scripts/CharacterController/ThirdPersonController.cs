using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] InventoryManager _inventoryManager;
    [SerializeField] BookManager _bookManager;

    //Input fields
    private IA_ThirdPersonController _playerActionAsset;
    private CharacterController _characterController;
    private Animator _animator;
    public CameraManager cameraManager;

    int isWalkingHash;
    int isRunningHash;

    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _currentRunMovement;

    bool _isMovementPressed;
    bool _isRunPressed;
    float _rotationFactor = 1f;
    float _runMultiplier = 3f;

    private Interactor _interactor;

    //[SerializeField] private Camera playerCamera;
    [SerializeField] private TextAsset startDialogue;

    void Awake()
    {
        _playerActionAsset = new IA_ThirdPersonController();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        _playerActionAsset.Player.Move.started += OnMovementInput;
        _playerActionAsset.Player.Move.canceled += OnMovementInput;
        _playerActionAsset.Player.Move.performed += OnMovementInput;
        _playerActionAsset.Player.Interact.performed += Interact;

        _playerActionAsset.Player.Inventory.performed += OnInventoryInput;
        _playerActionAsset.Player.Book.performed += OnBookInput;

        _playerActionAsset.Player.Run.started += OnRun;
        _playerActionAsset.Player.Run.canceled += OnRun;

        // Inventory actions
        _playerActionAsset.Player.AddToMix.performed += OnAddToMix;
        _playerActionAsset.Player.ResetMix.performed += OnResetMix;

        _interactor = GetComponent<Interactor>();
    }
    void OnMovementInput (InputAction.CallbackContext context)
    {
        if(DialogueManager.instance.dialogueIsPlaying)
        {
            Debug.Log("DialogueIsPlaying");
            return;
        }
        _currentMovementInput = context.ReadValue<Vector2>();

        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;

        _currentRunMovement.x = _currentMovementInput.x * _runMultiplier;
        _currentRunMovement.z = _currentMovementInput.y * _runMultiplier;

        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }
    void OnRun (InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }
    private void Start()
    {
        DialogueManager.instance.EnterDialogueMode(startDialogue);
    }
    private void OnInventoryInput(InputAction.CallbackContext context)
    {
        _inventoryManager.ToggleInventory();
    }

    private void OnAddToMix(InputAction.CallbackContext context)
    {
        _inventoryManager.AddToMix();
    }

    private void OnResetMix(InputAction.CallbackContext context)
    {
        _inventoryManager.ResetMix();
    }

    private void OnBookInput(InputAction.CallbackContext context)
    {
        _bookManager.ToggleBook();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (_interactor.canInteract)
        {
            _interactor.interactable.Interact(_interactor);
        }
    }

    private void FixedUpdate()
    {
        HandleGravity();
        HandleRotation();
        HandleAnimation();
        
        if (_isRunPressed)
        {
            _characterController.Move(_currentRunMovement * Time.fixedDeltaTime);
        }
        else
        {
            _characterController.Move(_currentMovement * Time.fixedDeltaTime);
        }
        

        //if (direction.magnitude >= 0.01)
        //{
        //    
        //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _trunSmoothTime);
        //    transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //    _characterController.Move(moveDir * _speed * Time.fixedDeltaTime);
        //}

            
    }
    private void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = _currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        if(_isMovementPressed)
        {
            Quaternion targetrRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetrRotation, _rotationFactor * Time.fixedDeltaTime);
        }
    }
    private void HandleAnimation()
    {
        bool isWalking = _animator.GetBool(isWalkingHash);
        bool isRunning = _animator.GetBool(isRunningHash);

        if(_isMovementPressed && !isWalking)
        {
            _animator.SetBool(isWalkingHash, true);
        }
        else if(!_isMovementPressed && isWalking)
        {
            _animator.SetBool(isWalkingHash, false);
        }

        if((_isMovementPressed && _isRunPressed) && !isRunning)
        {
            _animator.SetBool(isRunningHash, true);
        }
        else if((!_isMovementPressed || !_isRunPressed) && isRunning)
        {
            _animator.SetBool(isRunningHash, false);
        }
    }
    private void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            float groundedGravity = -0.5f;
            _currentMovement.y = groundedGravity;
            _currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -4.8f;
            _currentMovement.y = gravity;
            _currentRunMovement.y = gravity;
        }
    }
    private void OnEnable()
    {
        _playerActionAsset.Player.Enable();

    }
    private void OnDisable()
    {
        _playerActionAsset.Player.Disable();
    }
}
