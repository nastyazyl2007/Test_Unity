using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIFOPool : MonoBehaviour
{
    [SerializeField] private GameObject _chainPrefab;
    [SerializeField] private int _countOfInitialisaition = 20;
    [SerializeField] private Transform _chainSpawnPoint;

    private Queue<GameObject> _chainPool = new Queue<GameObject>();

    private void Start()
    {
        InitializePool();
    }




    private void InitializePool()
    {
        for (int i = 0; i < _countOfInitialisaition; i++)
        {
            CreateNewChain(); 
        }
    }




    private void CreateNewChain()
    {
        GameObject newChain = Instantiate(_chainPrefab, _chainSpawnPoint.position, Quaternion.identity);
        newChain.SetActive(false);
        newChain.transform.SetParent(_chainSpawnPoint);
        _chainPool.Enqueue(newChain);
    }


    public GameObject GetChain()
    {
        if(_chainPool.Count == 0)
        {
            CreateNewChain();
        }
        GameObject newChain = _chainPool.Dequeue();
        newChain.SetActive(true);
        return newChain;
    }


    public void ReturnChain(GameObject obj)
    {
        obj.SetActive(false);
        _chainPool.Enqueue(obj);
        obj.transform.position = _chainSpawnPoint.position; 
    }

    public void ReturnChain(GameObject obj, float time)
    {
        StartCoroutine(DelayReturnChain(obj, time));
    }

    private IEnumerator DelayReturnChain(GameObject obj, float time)    
    {
        yield return new WaitForSeconds(time);
        obj.transform.parent = _chainSpawnPoint;
        ReturnChain(obj);
    }
}
