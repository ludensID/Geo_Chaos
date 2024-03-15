using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private float _horizontalDirection;

    private void FixedUpdate()
    {
        _playerMovement.HorizontalMove(_horizontalDirection);
    }

    public void OnHorizontalInput(InputAction.CallbackContext context)
    {
        _horizontalDirection = context.ReadValue<float>();
    }
}
