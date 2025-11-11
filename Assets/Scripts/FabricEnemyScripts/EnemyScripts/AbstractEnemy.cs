using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour, IEnemy
{
    [Header("Enemy")]
    [SerializeField] private string _enemyName;
    [SerializeField] private EnemyType _enemyType;

    public string Name => _enemyName;

    public EnemyType  Type => _enemyType;

    protected virtual void Awake(){}
    public void Spawn(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
    public virtual void Move() {}
    public float Speed { get; }
    public abstract void Attack();
}
