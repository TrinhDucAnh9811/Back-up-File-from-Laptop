using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponPool : Singleton<EnemyWeaponPool>
{
    private List<GameObject> pooledWeapon1 = new List<GameObject>(); //E_Weapon Pool 1
    private List<GameObject> pooledWeapon2 = new List<GameObject>(); //E_Weapon Pool 2
    private List<GameObject> pooledWeapon3 = new List<GameObject>(); //E_Weapon Pool 3
    private int amountToPool1 = 100;
    private int amountToPool2 = 100;
    private int amountToPool3 = 100;
    [SerializeField] private GameObject[] e_WeaponPrefab;

    //Particle Effect:
    private List<GameObject> poolParticle = new List<GameObject>(); //E_Particle Effect Pool 1 (Enemy 3 Only)
    private int amountToEffectPool = 30;
    [SerializeField] private GameObject e_particlePrefab;

    void Start()
    {
        //Initialize amount of Enemie 1:
        for (int i = 0; i < amountToPool1; i++)
        {
            GameObject obj = Instantiate(e_WeaponPrefab[0]);
            obj.SetActive(false);
            pooledWeapon1.Add(obj);
        }

        //Initialize amount of Enemie 2:
        for (int i = 0; i < amountToPool2; i++)
        {
            GameObject obj = Instantiate(e_WeaponPrefab[1]);
            obj.SetActive(false);
            pooledWeapon2.Add(obj);
        }

        //Initialize amount of Enemie 3:
        for (int i = 0; i < amountToPool3; i++)
        {
            GameObject obj = Instantiate(e_WeaponPrefab[2]);
            obj.SetActive(false);
            pooledWeapon3.Add(obj);
        }
        //Effect 3:
        //Initialize amount of Enemie 1:
        for (int i = 0; i < amountToEffectPool; i++)
        {
            GameObject obj = Instantiate(e_particlePrefab);
            obj.SetActive(false);
            poolParticle.Add(obj);
        }
    }

    public GameObject GetPooledEnemyWeapon(int number)
    {
        switch (number)
        {
            case 0:
                {
                    for (int i = 0; i < pooledWeapon1.Count; i++)
                    {
                        if (!pooledWeapon1[i].activeInHierarchy)
                        {
                            return pooledWeapon1[i];
                        }
                    }
                    return null;
                    break;
                }

            case 1:
                {
                    for (int i = 0; i < pooledWeapon2.Count; i++)
                    {
                        if (!pooledWeapon2[i].activeInHierarchy)
                        {
                            return pooledWeapon2[i];
                        }
                    }
                    return null;
                    break;
                }

            case 2:
                {
                    for (int i = 0; i < pooledWeapon3.Count; i++)
                    {
                        if (!pooledWeapon3[i].activeInHierarchy)
                        {
                            return pooledWeapon3[i];
                        }
                    }
                    return null;
                    break;
                }
            case 3: //Particle Effect 3
                {
                    for (int i = 0; i < poolParticle.Count; i++)
                    {
                        if (!poolParticle[i].activeInHierarchy)
                        {
                            return poolParticle[i];
                        }
                    }
                    return null;
                    break;
                }

            default: return null;


        }
    }
}
