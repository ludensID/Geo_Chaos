using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    [SerializeField] [Range(-100, 100)] private float _gravity;

    private void Start()
    {
        Physics.gravity = new Vector3(0, _gravity, 0);
    }
}
