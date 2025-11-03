using UnityEngine;

public class Chicken : AbstractEnemy
{
    [Header("Move")]
    [SerializeField] private float number = 0.1f;
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
            if (transform.position.x >= _position.x + 1)
            {
                _IsRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * (Time.deltaTime * Speed));
            if (transform.position.x <= _position.x - 1)
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