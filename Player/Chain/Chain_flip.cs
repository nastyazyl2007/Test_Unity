using UnityEngine;

public class Chain_flip : MonoBehaviour
{
    [SerializeField] private Player_Movement _flip;
    public Vector2 _flipChain;

    private void Start()
    {
        _flip = GetComponent<Player_Movement>();
    }

    private void Update()
    {
        CheckFlip();
    }

    private void CheckFlip()  //Доделать
    {
        if (_flip == null || _flip._flip == null)
        {
            _flipChain = Vector2.right;
            return;
        }

        _flipChain = _flip._flip.flipX ? Vector2.left : Vector2.right;
    }
}