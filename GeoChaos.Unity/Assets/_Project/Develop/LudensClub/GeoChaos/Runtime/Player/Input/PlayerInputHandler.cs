using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float _canceledJumpMultiplier;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GroundCheck _groundCheck;

    private float _horizontalDirection;
    private InputAction _horizontalAction;
    private InputAction _jumpAction;
    private int _value;

    private void Start()
    {
        var input = GetComponent<PlayerInput>();
        
        _horizontalAction = input.actions["HorizontalMovement"];
        _jumpAction = input.actions["Jump"];
    }

    private void FixedUpdate()
    {
        _playerMovement.HorizontalMove(_horizontalDirection);
    }

    public void OnHorizontalInput(InputAction.CallbackContext context)
    {
        _horizontalDirection = context.ReadValue<float>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.performed && _groundCheck.IsGrounded())
        {
            _playerMovement.Jump(1);
        }
        if(context.canceled && _playerMovement.PlayerRigidbody.velocity.y > 0f)
        {
            _playerMovement.Jump(_canceledJumpMultiplier);
        }
    }
}
