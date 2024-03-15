using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GroundCheck _groundCheck;

    private float _horizontalDirection;

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
            _playerMovement.Jump();
        }
        if(context.canceled && _playerMovement.PlayerRigidbody.velocity.y > 0f)
        {
            _playerMovement.ShortJump();
        }
    }
}
