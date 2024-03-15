using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float _moveSpeed;
    [SerializeField][Range(0, 100)] private float _jumpForce;
    [SerializeField][Range(0, 1)] private float _canceledJumpMultiplier;

    private Rigidbody _rigidbody;

    public Rigidbody PlayerRigidbody => _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void HorizontalMove(float direction)
    {
        Vector3 newDirection = new Vector3(direction * _moveSpeed, _rigidbody.velocity.y, 0);
        _rigidbody.velocity = newDirection;
    }

    public void Jump()
    {
        Vector3 jumpVector = new Vector3(_rigidbody.velocity.x, _jumpForce, 0);
        _rigidbody.velocity = jumpVector;
    }

    public void ShortJump()
    {
        Vector3 jumpVector = new Vector3(_rigidbody.velocity.x, _canceledJumpMultiplier, 0);
        _rigidbody.velocity = jumpVector;
    }
}
