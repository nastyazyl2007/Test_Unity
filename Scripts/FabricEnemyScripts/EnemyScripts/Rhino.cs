using System;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.InputSystem.HID;

public class Rhino : AbstractEnemy
{
    [Header("Move")]
    [SerializeField] private float number = 2f;
    [SerializeField] private bool _isTriggerd;
    public SpriteRenderer _flip;
    private bool _isRight;
    private Animator _animation; 
    public float Speed => number;

    private void Awake()
    {
        _animation = GetComponent<Animator>(); 
        _flip = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (_flip.flipX == true)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * Speed));
        }
        else
        {
            transform.Translate(Vector3.left * (Time.deltaTime * Speed));
        }
        Flip();
    }
    public override void Attack()
    {
        Debug.Log($"Attack");
    }

    void Flip()
    {
        if (_isRight && _isTriggerd)
        {
            _isRight = false;
            _flip.flipX = true;
        }
        else
        {
            if (!_isRight && _isTriggerd)
            {
                _isRight = true;
                _flip.flipX = false;
            }
        }
        _animation.SetBool("Touch", _isTriggerd);
        Debug.Log(_isTriggerd);
    }


    private void OnEnable()
    {
        Trigger_on_wall.WallTrigger += HandlGroundState;
    }
    private void OnDisable()
    {
        Trigger_on_wall.WallTrigger -= HandlGroundState;
    }
    private void HandlGroundState(bool grounded)
    {
        _isTriggerd = grounded;
    }
}
