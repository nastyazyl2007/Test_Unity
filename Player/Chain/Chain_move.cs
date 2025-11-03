using UnityEngine;

public class Chain_move : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float _speed = 4f;
    private Chain_flip _flip;

    private void Start()
    {
        _flip = GetComponent<Chain_flip>();  
    }
    //private Player checkfFlip;



    //private void Start()
    //{
    //    GameObject player = GameObject.FindWithTag("Player");
    //    if (player != null)
    //    {
    //        checkfFlip = player.GetComponent<Player>();
    //    }
    //}

    void Update()
    {
    //    if (checkfFlip != null)
    //    {
    //        if (checkfFlip._checkIsRight == true)
           transform.Translate(_flip._flipChain * (Time.deltaTime * _speed));
        //    else if (checkfFlip._checkIsRight == false) checkfFlip.chains.transform.Translate(Vector3.left * (Time.deltaTime * _speed));
        //}
    }
}
