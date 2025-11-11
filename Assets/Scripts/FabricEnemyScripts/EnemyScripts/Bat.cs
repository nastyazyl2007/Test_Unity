using UnityEngine;

public class Bat : AbstractEnemy
{
    [Header("Move")]
    [SerializeField] private float number = 2f;
    private Vector2 _position;
    private SpriteRenderer _flip;
    private bool _IsRight;

    public float Speed => number;

    private void Start()
    {
        _flip = GetComponent<SpriteRenderer>();
        _position = transform.position;
    }

    private void Awake()
    {
    }

    private void Update()
    {
        Move();
        Flip();
    }

    public override void Move()
    {
        if (_IsRight)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * Speed));
            if (transform.position.x >= _position.x + 4)
            {
                _IsRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * (Time.deltaTime * Speed));
            if (transform.position.x <= _position.x - 4)
            {
                _IsRight = true;
            }
        }
    }

    private void Flip()
    {
        _flip.flipX = _IsRight;
    }

    public override void Attack()
    {
        Debug.Log($"Attack");
    }
}