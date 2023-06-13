using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    //Input fields
    private IA_ThirdPersonController _playerActionAsset;
    private InputAction _move;
    private Interactor _interactor;

    //movement fields
    private Rigidbody _rb;
    [SerializeField] private float movementForce = 1f;
    [SerializeField] private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField] private Camera playerCamera;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerActionAsset = new IA_ThirdPersonController();
        _interactor = GetComponent<Interactor>();
    }
    private void OnEnable()
    {
        _move = _playerActionAsset.Player.Move;
        _playerActionAsset.Player.Enable();
        _playerActionAsset.Player.Interact.performed += Interact;
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
        forceDirection += _move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += _move.ReadValue<Vector2>().y * GetCameraFoward(playerCamera) * movementForce;

        _rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if(_rb.velocity.y< 0f)
            _rb.velocity += Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = _rb.velocity;
        horizontalVelocity.y = 0f;

        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            _rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * _rb.velocity.y;

        LookAt();
    }
    private void LookAt()
    {
        Vector3 direction = _rb.velocity;
        direction.y = 0;
        if (_move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this._rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            _rb.angularVelocity = Vector3.zero;
    }
    private Vector3 GetCameraFoward(Camera playerCamera)
    {
        Vector3 foward = playerCamera.transform.forward;
        foward.y = 0;
        return foward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void OnDisable()
    {
        _playerActionAsset.Player.Disable();
    }
}
