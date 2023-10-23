using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


//Sumary: Project: Bullets + Bomb + Hommissing
public class ProjectilePool : Singleton<ProjectilePool>
{
    private List<GameObject> pooledObjects1 = new List<GameObject>();
    private List<GameObject> pooledObjects2 = new List<GameObject>();
    private List<GameObject> pooledObjects3 = new List<GameObject>();
    private int amountToPool1 = 40;
    private int amountToPool2 = 15;
    private int amountToPool3 = 15;
    public GameObject[] projectilePrefab;
    public Transform firePoint;

    void Start()
    {
        for (int i = 0; i < amountToPool1; i++)
        {
            GameObject obj = Instantiate(projectilePrefab[0]); //Chạy vòng lặp for để tạo ra đủ 20 bullets
            obj.SetActive(false);                           //Ngay khi tạo ra thì cho nó không hiện lên màn hình
            pooledObjects1.Add(obj);                         //Sau khi tạo và bỏ active xong, thêm nó vào list
        }

        for (int i = 0; i < amountToPool2; i++)
        {
            GameObject obj = Instantiate(projectilePrefab[1]); //Chạy vòng lặp for để tạo ra đủ 20 bullets
            obj.SetActive(false);                           //Ngay khi tạo ra thì cho nó không hiện lên màn hình
            pooledObjects2.Add(obj);                         //Sau khi tạo và bỏ active xong, thêm nó vào list
        }

        for (int i = 0; i < amountToPool3; i++)
        {
            GameObject obj = Instantiate(projectilePrefab[2]); //Chạy vòng lặp for để tạo ra đủ 20 bullets
            obj.SetActive(false);                           //Ngay khi tạo ra thì cho nó không hiện lên màn hình
            pooledObjects3.Add(obj);                         //Sau khi tạo và bỏ active xong, thêm nó vào list
        }
    }

    public GameObject GetPooledObject(int number)
    {
        switch (number)
        {
            case 0:
                {
                    for (int i = 0; i < pooledObjects1.Count; i++)
                    {
                        if (!pooledObjects1[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledObjects1[i];
                        }

                    }
                    return null;
                    break;
                }


            case 1:
                {
                    for (int i = 0; i < pooledObjects2.Count; i++)
                    {
                        if (!pooledObjects2[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledObjects2[i];
                        }

                    }
                    return null;
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < pooledObjects3.Count; i++)
                    {
                        if (!pooledObjects3[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledObjects3[i];
                        }

                    }
                    return null;
                    break;
                }
            default: return null;
        }
        
    }
}
