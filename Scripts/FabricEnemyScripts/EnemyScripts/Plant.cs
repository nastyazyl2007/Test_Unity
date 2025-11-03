using UnityEngine;

public class Plant : AbstractEnemy
{
    private float number = 0f;

    public float Speed => number;
    public override void Move()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * Speed));
    }
    public override void Attack()
    {
        Debug.Log($"Attack");
    }
}