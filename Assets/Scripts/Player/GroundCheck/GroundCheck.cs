using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private float _groundCheckDistance;

    [SerializeField] private LayerMask _groundLayer;

    private readonly RaycastHit[] _hitsResultBuffert = new RaycastHit[1];

    private Ray RayToGround => new Ray(transform.position, transform.up * -1);

    public bool IsGrounded()
    {
        return Physics.RaycastNonAlloc(RayToGround, _hitsResultBuffert, _groundCheckDistance , _groundLayer) > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(RayToGround.origin, RayToGround.direction * _groundCheckDistance);
    }
}
