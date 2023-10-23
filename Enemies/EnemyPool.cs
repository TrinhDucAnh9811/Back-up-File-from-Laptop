using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyPool : Singleton<EnemyPool>
{
    private List<GameObject> pooledEnemies0 = new List<GameObject>(); //Pool 1
    private List<GameObject> pooledEnemies1 = new List<GameObject>(); //Pool 2
    private List<GameObject> pooledEnemies2 = new List<GameObject>(); //Pool 3
    private List<GameObject> pooledEnemies3 = new List<GameObject>(); //Pool 4 (melee)
    private List<GameObject> pooledEnemies4 = new List<GameObject>(); //Pool 5 (BOSS)
    private int amountToPool1 = 300;
    private int amountToPool2 = 100;
    private int amountToPool3 = 500;
    private int amountToPool4 = 20; //MELEE
    private int amountToPool5 = 3; //BOSS
    [SerializeField] private GameObject[] enemyPrefab;
    void Start()
    {
        //Initialize amount of Enemie 1:
        for(int i = 0; i< amountToPool1; i++)  
        {
            GameObject obj = Instantiate(enemyPrefab[0]);
            obj.SetActive(false);
            pooledEnemies0.Add(obj);
        }

        //Initialize amount of Enemie 2:
        for (int i = 0; i < amountToPool2; i++)
        {
            GameObject obj = Instantiate(enemyPrefab[1]);
            obj.SetActive(false);
            pooledEnemies1.Add(obj);
        }

        //Initialize amount of Enemie 3:
        for (int i = 0; i < amountToPool3; i++)
        {
            GameObject obj = Instantiate(enemyPrefab[2]);
            obj.SetActive(false);
            pooledEnemies2.Add(obj);
        }

        //Initialize amount of Enemie 4 (melee):
        for (int i = 0; i < amountToPool4; i++)
        {
            GameObject obj = Instantiate(enemyPrefab[3]);
            obj.SetActive(false);
            pooledEnemies3.Add(obj);
        }

        //Initialize amount of Enemie 5 (Boss):
        for (int i = 0; i < amountToPool5; i++)
        {
            GameObject obj = Instantiate(enemyPrefab[4]);
            obj.SetActive(false);
            pooledEnemies4.Add(obj);
        }
    }

    public GameObject GetPooledEnemies(int number)
    {
        switch(number)
        {
            case 0:
                for (int i = 0; i < pooledEnemies0.Count; i++)
                    {
                        if (!pooledEnemies0[i].activeInHierarchy)
                        {
                            return pooledEnemies0[i];
                        }
                    }
                    return null;
                break;

            case 1:
                for (int i = 0; i < pooledEnemies1.Count; i++)
                {
                    if (!pooledEnemies1[i].activeInHierarchy)
                    {
                        return pooledEnemies1[i];
                    }
                }
                return null;
                break;

            case 2:
                for (int i = 0; i < pooledEnemies2.Count; i++)
                {
                    if (!pooledEnemies2[i].activeInHierarchy)
                    {
                        return pooledEnemies2[i];
                    }
                }
                return null;
                break;

            case 3:
                for (int i = 0; i < pooledEnemies3.Count; i++)
                {
                    if (!pooledEnemies3[i].activeInHierarchy)
                    {
                        return pooledEnemies3[i];
                    }
                }
                return null;
                break;

            case 4:
                for (int i = 0; i < pooledEnemies4.Count; i++)
                {
                    if (!pooledEnemies4[i].activeInHierarchy)
                    {
                        return pooledEnemies4[i];
                    }
                }
                return null;
                break;

            default: return null;


        }
    }
}
