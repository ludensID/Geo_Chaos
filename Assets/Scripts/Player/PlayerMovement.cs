using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float _moveSpeed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void HorizontalMove(float direction)
    {
        Vector3 newDirection = new Vector3(direction * _moveSpeed, _rigidbody.velocity.y, 0);
        _rigidbody.velocity = newDirection;
    }
}
