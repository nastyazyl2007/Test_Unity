using System;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Trigger_on_wall : MonoBehaviour
{


    public static Action<bool> WallTrigger;


    [Header("Wall")]
    [SerializeField] private LayerMask _WallLayer;
    [SerializeField] private float _raycastDistance = 1f;
    [SerializeField] private SpriteRenderer _flip;
    private bool _IsWall;

    private void Update()
    {
        CheckGrounded();
    }
    private void CheckGrounded()
    {
        bool hit;
        if (_flip.flipX == true)
        {
            Debug.DrawRay(transform.position, Vector2.right * _raycastDistance, _IsWall ? Color.green : Color.red);
            hit = Physics2D.Raycast(transform.position, Vector2.right, _raycastDistance, _WallLayer);
        }
        else
        {
           Debug.DrawRay(transform.position, Vector2.left * _raycastDistance, _IsWall ? Color.green : Color.red);
           hit = Physics2D.Raycast(transform.position, Vector2.left, _raycastDistance, _WallLayer);
        }

        if (hit != _IsWall)
        {
            _IsWall = hit;
            WallTrigger?.Invoke(_IsWall);
        }
    }

}
