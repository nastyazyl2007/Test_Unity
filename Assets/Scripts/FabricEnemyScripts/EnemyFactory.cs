using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;




public enum EnemyType
{
    Bat,
    Rhino,
    Chicken,
    Plant
}




public interface IEnemyFactory
{
    void CreateEnemyOfType(EnemyType type);

    void CreateAllEnemys();
}



public interface IEnemy
{
    public string Name { get; }
    public float Speed { get; }
    EnemyType Type { get; }
    void Spawn(Vector2 position);
    void Attack();
}





//Фабрика врагов
public class EnemyFactory : MonoBehaviour, IEnemyFactory
{


    [Serializable]
    public class EnemyPrefab
    {
        public EnemyType Type;
        public GameObject Prefab;
    }





    [Serializable]
    public class SpawnPointGroup
    {
        public Vector2 position;
    }




    [Serializable]
    public class EnemySpawnConfig
    {
        public EnemyType enemyType;
        public List<SpawnPointGroup> spawnPoint = new List<SpawnPointGroup>();

    }



    [Header ("Prefab")]
    [SerializeField] private List<EnemyPrefab> enemyPrefabs = new List<EnemyPrefab>();




    [Header("Spawn Points")]
    [SerializeField] private List<EnemySpawnConfig> spawnConfigs = new List<EnemySpawnConfig>();





    private Dictionary<EnemyType, GameObject> _prefabDictionary;
    private Dictionary<EnemyType, List<SpawnPointGroup> > _spawnPointDictionary;
    private Transform _enemyParent;








    public void Initialize()
    {
        _prefabDictionary = new Dictionary<EnemyType, GameObject>();
        _spawnPointDictionary = new Dictionary<EnemyType, List<SpawnPointGroup>>();

        foreach(var enemyPrefab in enemyPrefabs) 
        {
            if(enemyPrefab.Prefab)
            {
                _prefabDictionary[enemyPrefab.Type] = enemyPrefab.Prefab;
            }
        }


        foreach(EnemyType type in Enum.GetValues(typeof(EnemyType)))
        {
            _spawnPointDictionary[type] = new List<SpawnPointGroup>();
        }


        foreach(var  config in spawnConfigs)
        {
            _spawnPointDictionary[config.enemyType] = new List<SpawnPointGroup> (config.spawnPoint);
        }

        _enemyParent = new GameObject("Enemy").transform;

        Debug.Log($"EnemyFacttory inicialized");
    }









    public void CreateEnemyOfType(EnemyType type)
    {
        if (!_prefabDictionary.ContainsKey(type))
        {
            Debug.LogError($"Enemy type {type} not founded!");
            return;
        }
        var spawnPoint = _spawnPointDictionary[type];
        if(spawnPoint.Count == 0)
        {
            Debug.LogError($"No Spawnpoints");
            return;
        }
        Debug.Log($"Creating {spawnPoint.Count} enemy of type: {type}");

        foreach( var spawnGroup in spawnPoint)
        {
            CreateEnemyFromSpawnPoint(type, spawnGroup);
        }
    }








    private void CreateEnemyFromSpawnPoint(EnemyType type, SpawnPointGroup spawnGroup)
    {
        GameObject enemyObject = Instantiate(_prefabDictionary[type], spawnGroup.position, Quaternion.identity, _enemyParent);
        IEnemy enemy = enemyObject.GetComponent<IEnemy>();

        if (enemy == null) 
        {
            Debug.LogError($"Prefab dosen't implement IEnemy interface! Type: {type}");

            Destroy(enemyObject);
            return;
        }

        if(enemy is AbstractEnemy baseEnemy)
        {
            baseEnemy.Move();
        }

        enemy.Spawn(spawnGroup.position);


    }







    public void CreateAllEnemys()
    {
        Debug.Log($"Creating all enemys....");
        foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
        {
            CreateEnemyOfType(type); 
        }
    }

}                 



