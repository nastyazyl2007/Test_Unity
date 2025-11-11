using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Player : MonoBehaviour
{

    private Player_Movement _movement;
    [SerializeField] private bool _trigger;
    [SerializeField] private FIFOPool _chainPool;

    public GameObject chains;


    private Player_Movement checkfFlip;
    public bool _checkIsRight;

    private void Awake()
    {
        _movement = GetComponent<Player_Movement>();

    }
    private void OnEnable()
    {
        _movement.SpawnChain += ChainSpawn;
    }


    private void OnDisable()
    {
        _movement.SpawnChain -= ChainSpawn;
    }
    private void ChainSpawn()
    {
        chains = _chainPool.GetChain(); 
        chains.transform.parent = null;
        InitializePlayerFlip();
        if(_trigger == true ) _chainPool.ReturnChain(chains, 0f);
        else _chainPool.ReturnChain(chains, 2f);
    }

    private void InitializePlayerFlip()
    {
        checkfFlip = GetComponent<Player_Movement>();
        if (checkfFlip != null)
        {
            _checkIsRight = checkfFlip._isRight;
        }
        else
        {
            _checkIsRight = true;
        }

        Debug.Log($"{_checkIsRight}");

    }
}