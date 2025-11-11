using UnityEngine;
using UnityEngine.Rendering;

public class WarmUpComponent : MonoBehaviour
{




    [Header("Factory References")]
    [SerializeField] private EnemyFactory _enemyFactory;






    [Header("Warm Up Settings")]
    [SerializeField] private bool _warmOnStart = true;
    [SerializeField] private bool _useFactoryColor = true;






    private void Start()
    {
        if (!CheckFactory()) return;

        _enemyFactory.Initialize();

        if (_warmOnStart)
        {
            WarmAllEnemys();
        }
    }



    //public void  WarmUpBat() => WarmUpEnemyType(EnemyType.Bat);
    //public void WarmUpRhino() => WarmUpEnemyType(EnemyType.Rhino);
    //public void WarmUpPlant() => WarmUpEnemyType(EnemyType.Plant);
    //public void WarmUpChameleon() => WarmUpEnemyType(EnemyType.Chameleon);


    public bool CheckFactory()
    {
        if (!_enemyFactory) return false;
        else return true;
    }





    private void WarmAllEnemys()
    {
        if(!CheckFactory()) return;

        Debug.Log("=== WARM UP Creating all enemys ====");


        if (_useFactoryColor)
        {
            _enemyFactory.CreateAllEnemys();
        }
        else
        {
            _enemyFactory.CreateAllEnemys();
        }
    }


    public void WarmUpEnemyType(EnemyType type)
    {
        if (!CheckFactory()) return;
        Debug.Log($"=== WARM UP Creating {type} enemys ====");


        if (_useFactoryColor)
        {
            _enemyFactory.CreateEnemyOfType(type);
        }
        else
        {
            _enemyFactory.CreateEnemyOfType(type); 
        }
    }

}
