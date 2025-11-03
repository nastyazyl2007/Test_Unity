using System;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.InputSystem.HID; 

public class Player_Movement : MonoBehaviour
{
    public Action SpawnChain;
    

    [Header("Movement")]
    [SerializeField] private float _speed = 7f;
    private Vector2 _movement;
    private Animator _animation;  
    private Animation _anim;    


    private bool _IsGround;

    [Header("Jump")]
    [SerializeField] private float _ForceJump = 3f;

    [Header("Flip")]
    [SerializeField] public bool _isRight;
    public SpriteRenderer _flip;



    private Rigidbody2D _rb;    


    void Awake()
    {
         _isRight = true;
        _animation = GetComponent<Animator>();
        _flip = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        Walk();
        Jump();
        Flip();

        if (Input.GetMouseButtonDown(0))   
        {
            SpawnChain?.Invoke();
        }
    }

    private void OnEnable()
    {
        GroundCheker.OnGround += HandlGroundState;
    }



    private void OnDisable()
    {
        GroundCheker.OnGround -= HandlGroundState;
    }
    private void HandlGroundState( bool grounded)
    {
        _IsGround = grounded;
    }

    void Walk()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _animation.SetFloat("MoveX", Mathf.Abs(_movement.x));
        _rb.linearVelocity = new Vector2 (_movement.x*_speed, _rb.linearVelocity.y);
    }

    void Flip()
    {
        if (_isRight && _movement.x<0) 
        { 
            _isRight = false;
            _flip.flipX = true;
        }
        else
        {
            if (!_isRight && _movement.x > 0)
            {
                _isRight = true;
                _flip.flipX = false;
            }
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _IsGround)   
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _ForceJump);
        }
        _animation.SetBool("OnGround", _IsGround); 
        _animation.SetFloat("Jump", _rb.linearVelocity.y);
        if (Time.time == 15 && _animation.GetFloat("Jump")>0)
        {
            _anim["Jump"].speed = 0;
        }
    }

}
