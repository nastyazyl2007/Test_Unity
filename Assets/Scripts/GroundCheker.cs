using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheker : MonoBehaviour
{

    public static Action<bool> OnGround;

    [Header("Ground")]
    [SerializeField] private LayerMask _GroundLayer;
    [SerializeField] private float _raycastDistance = 0.1f;
    private bool IsGround;

    private void Update() 
    {
       CheckGrounded();
    }
    private void CheckGrounded()
    {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, _raycastDistance, _GroundLayer); 
        if (hit != IsGround)
        {
            IsGround = hit;
            OnGround?.Invoke(IsGround); 
        }
        Debug.DrawRay(transform.position, Vector2.down * _raycastDistance, IsGround ? Color.green : Color.red);
    }
}
