using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] InventoryManager _inventoryManager;
    [SerializeField] BookManager _bookManager;

    //Input fields
    private IA_ThirdPersonController _playerActionAsset;
    private CharacterController _characterController;
    private Animator _animator;
    public CameraManager cameraManager;
    //private StarterAssetsInputs _input;

#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif


    int isWalkingHash;
    int isRunningHash;

    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _currentRunMovement;
    private Vector2 look;

    bool _isMovementPressed;
    bool _isRunPressed;
    float _rotationFactor = 2f;
    float _runMultiplier = 3f;

    private Interactor _interactor;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    private const float _threshold = 10f;

    //[SerializeField] private Camera playerCamera;
    [SerializeField] private TextAsset startDialogue;

    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }

    private void Awake()
    {
        _playerActionAsset = new IA_ThirdPersonController();
        _playerActionAsset.Player.Move.started += OnMovementInput;
        _playerActionAsset.Player.Move.canceled += OnMovementInput;
        _playerActionAsset.Player.Move.performed += OnMovementInput;
        _playerActionAsset.Player.Look.performed += OnLook;

        _playerActionAsset.Player.Interact.performed += Interact;

        _playerActionAsset.Player.Inventory.performed += OnInventoryInput;
        _playerActionAsset.Player.Book.performed += OnBookInput;

        _playerActionAsset.Player.Run.started += OnRun;
        _playerActionAsset.Player.Run.canceled += OnRun;

        // Inventory actions
        _playerActionAsset.Player.AddToMix.performed += OnAddToMix;
        _playerActionAsset.Player.ResetMix.performed += OnResetMix;
    }
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        _interactor = GetComponent<Interactor>();

        DialogueManager.instance.EnterDialogueMode(startDialogue);
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

    private void LateUpdate()
    {
        CameraRotation();
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

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
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
