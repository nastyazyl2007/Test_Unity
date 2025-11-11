using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainPoool : MonoBehaviour
{
    [SerializeField] private GameObject _chainPrefab;
    [SerializeField] private int _countOfInitialisaition = 15;
    [SerializeField] private Transform _chainSpawnPoint;

    private List<GameObject> _chain = new List<GameObject>();

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

    private GameObject CreateNewChain()
    {
        GameObject newChain = Instantiate(_chainPrefab, _chainSpawnPoint.position, Quaternion.identity);
        newChain.SetActive(false);
        newChain.transform.SetParent(_chainSpawnPoint);
        _chain.Add(newChain);
        return newChain;
    }




    public GameObject GetChain()
    {
        foreach (GameObject chain in _chain) 
        {
            if (!chain.activeInHierarchy)
            {
                chain.SetActive(true);
                return chain; 
            }
        }
        GameObject newChain = CreateNewChain();
        newChain.SetActive(true);
        return newChain;
    }


    public void ReturnChain(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = _chainSpawnPoint.position;
    }



    public void ReturnChain(GameObject obj, float time)
    {
        StartCoroutine(DelayReturnChain(obj, time));
    }




    private IEnumerator DelayReturnChain(GameObject obj,float time)   
    {
        yield return  new WaitForSeconds(time);
        ReturnChain(obj);
    }

}
