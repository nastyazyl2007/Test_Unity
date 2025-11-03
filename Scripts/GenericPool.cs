using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class GenericPool<T>: MonoBehaviour where T: Component
{
    [Header("Pool Settings")]
    [SerializeField] T _prefab;
    [SerializeField] private int _objectSize = 10;
    [SerializeField] private int _maxSize = 20;
    [SerializeField] private Transform _poolParent;

    private Queue<T> _avalableObject = new Queue<T>();
    private List<T> _allObjects = new List<T>();
    private int _activeCount = 0;






    private void Start()
    {
        if (_poolParent == null)
        {
            _poolParent = transform;
        }
        InitializePool();
    }







    private void InitializePool()
    {
        for (int i =0; i< _objectSize; i++)
        {
           CreateObject();
        }
    }






    private T CreateObject()
    {
        T newObject = Instantiate(_prefab,_poolParent);
        newObject.gameObject.SetActive(false);

        var pooledComponent = newObject.GetComponent<GenericPoolObject>();
        if (pooledComponent == null) 
        {
            pooledComponent = newObject.gameObject.AddComponent<GenericPoolObject>();
        }
        pooledComponent.SetPool(this);
        return newObject;
    }







    public T GetObject()
    {
        T obj = default(T);
        if (_avalableObject.Count > 0) 
        {
            obj = _avalableObject.Dequeue();
        }
        else if(_allObjects.Count < _maxSize)
        {
            obj = CreateObject();
        }
        else
        {
            Debug.LogWarning($"Pool <{typeof(T).Name}> reached maximum size!");
            return null;
        }
        if (obj != null)
        {
            obj.gameObject.SetActive(true);
            _activeCount++;
        }
        return obj;
    }







    public T GetObject (Vector3 position, Quaternion rotation)
    {
        T obj = GetObject();
        if (obj)
        {
            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }
        return obj;
    }






    public void ReturnObject(T obj)
    {
        if(obj == null || !_allObjects.Contains(obj))return;
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_poolParent);
        _avalableObject.Enqueue(obj);
        _activeCount--;

    }






    public void PrintStatus()
    {
        Debug.Log($"Pool <{typeof(T).Name}> Total: {_allObjects.Count};"+
            $"Availabel{_activeCount};"+
            $"Active {_activeCount}");
    }




    public int TotalCount => _allObjects.Count;
    public int AvailableCount => _avalableObject.Count;
    public int ActiveCount => _activeCount;
}











public class GenericPoolObject : MonoBehaviour
{
    private object _pool;
    public void SetPool<T>(GenericPool<T> pool) where T : Component
    {
        _pool = pool;
    }

    void OnDisable()
    {
        if (_pool != null) 
        {
            var method = _pool.GetType().GetMethod("ReturnObject");
            var component = GetComponent(_pool.GetType().GetGenericArguments()[0]);
            method?.Invoke(_pool, new object[] { component });
        }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }


}








public class Chain : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private Rigidbody _rigidBody;


    private void Awake() => _rigidBody = GetComponent<Rigidbody>();



    private void OnEnable()
    {
        _rigidBody.linearVelocity = transform.forward * _speed;
        Invoke(nameof(Disable), 3f);
    }

    private void OnDisable()
    {

        _rigidBody.linearVelocity = Vector3.zero;
    }

    private void Disable() => gameObject.SetActive(false);


}




public class ExplosionEffects: MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _particleSystem.Play();
        Invoke(nameof(Disable), _particleSystem.main.duration);
    }
    private void OnDisable() => CancelInvoke();

    private void Disable() => gameObject.SetActive(false);
}







//public class EffectComponent: MonoBehaviour
//{
//    [SerializeField] private ExplosionEffects _explosionPrefab;
//    private GenericPool<ExplosionEffects> _explosionPool;

//    private void Start()
//    {
//        _explosionPool = new GenericPool<ExplosionEffects>(_explosionPrefab, transform,5,10);

//    }

//    public void SpawnExplosion(Vector3 position)
//    {
//        ExplosionEffects effect = _explosionPool.GetObject(position, Quaternion.identity);
//    }

//}







//public class PoolManagerTest: MonoBehaviour
//{
//    public static PoolManagerTest _instatce;
//    public static PoolManagerTest Instance => _instatce;

//    private Dictionary<string,object> _pools = new Dictionary<string,object>();

//    private void Awake()
//    {
//        if( _instatce == null)
//        {
//            _instatce = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }
//}




//public GenericPool<T> CreatePool <T> (T prefab,Transform parent,int initializeSize = 10,int maxSize = 50 ) where T : Component
//{
//    string poolKey = $"{typeof(T).Name}_{prefab.GetInstanceID()}";
//    if (!_pools.ContainsKey(poolKey))
//    {

//    }
//    return (GenericPool<T>)_pools[poolKey]
//}