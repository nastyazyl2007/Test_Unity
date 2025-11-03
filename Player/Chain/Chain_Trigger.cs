using System;
using UnityEngine;

public class Chain_Trigger : MonoBehaviour
{


    public static Action<bool> EnemyTrigger;


    [Header("Wall")]
    [SerializeField] private LayerMask _EnemyLayer;
    [SerializeField] private float _raycastDistance = 1f;
    private bool _IsWall;

    private void Update()
    {
        CheckGrounded();
    }
    private void CheckGrounded()
    {
        bool hit;
            Debug.DrawRay(transform.position, Vector2.right * _raycastDistance, _IsWall ? Color.green : Color.red);
            hit = Physics2D.Raycast(transform.position, Vector2.right, _raycastDistance, _EnemyLayer);
        if (hit != _IsWall)
        {
            _IsWall = hit;
            EnemyTrigger?.Invoke(_IsWall);
        }
    }

}
