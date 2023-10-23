using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesProjectilePool : Singleton<AlliesProjectilePool>
{
    private List<GameObject> pooledAlliesWeapon1 = new List<GameObject>();
    private List<GameObject> pooledAlliesWeapon2 = new List<GameObject>();
    private List<GameObject> pooledAlliesWeapon3 = new List<GameObject>();
    private List<GameObject> pooledAlliesWeapon3Tier1 = new List<GameObject>();
    private int amountToPool1 = 20;
    private int amountToPool2 = 20;
    private int amountToPool3 = 20; //Healing... Tính sau (Có thể chỉ thêm VFX)
    private int amountToPool3Tier1 = 2; //Shield... //CHÚ Ý
    public GameObject[] projectilePrefab;


    void Start()
    {
        for (int i = 0; i < amountToPool1; i++)
        {
            GameObject obj = Instantiate(projectilePrefab[0]); //Chạy vòng lặp for để tạo ra đủ 20 bullets
            obj.SetActive(false);                           //Ngay khi tạo ra thì cho nó không hiện lên màn hình
            pooledAlliesWeapon1.Add(obj);                         //Sau khi tạo và bỏ active xong, thêm nó vào list
        }

        for (int i = 0; i < amountToPool2; i++)
        {
            GameObject obj = Instantiate(projectilePrefab[1]); 
            obj.SetActive(false);                           
            pooledAlliesWeapon2.Add(obj);                        
        }

        for (int i = 0; i < amountToPool3; i++)
        {
            GameObject obj = Instantiate(projectilePrefab[2]); 
            obj.SetActive(false);                         
            pooledAlliesWeapon3.Add(obj);                        
        }

        for (int i = 0; i < amountToPool3Tier1; i++)
        {
            GameObject obj = Instantiate(projectilePrefab[3]); //SHIELD
            obj.SetActive(false);                           
            pooledAlliesWeapon3Tier1.Add(obj);                         
        }
    }

    public GameObject GetPooledAlliesWeaponTier0(int number)
    {
        switch (number)
        {
            case 0:
                {
                    for (int i = 0; i < pooledAlliesWeapon1.Count; i++)
                    {
                        if (!pooledAlliesWeapon1[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledAlliesWeapon1[i];
                        }

                    }
                    return null;
                    break;
                }


            case 1:
                {
                    for (int i = 0; i < pooledAlliesWeapon2.Count; i++)
                    {
                        if (!pooledAlliesWeapon2[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledAlliesWeapon2[i];
                        }

                    }
                    return null;
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < pooledAlliesWeapon3.Count; i++)
                    {
                        if (!pooledAlliesWeapon3[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledAlliesWeapon3[i];
                        }

                    }
                    return null;
                    break;
                }
            default: return null;
        }

    }



    public GameObject GetPooledAlliesWeaponTier1(int number)
    {
        switch (number)
        {
            //Auto Shoot, the same Tier 0 but get more from pool
            case 0:
                {
                    for (int i = 0; i < pooledAlliesWeapon1.Count; i++)
                    {
                        if (!pooledAlliesWeapon1[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledAlliesWeapon1[i];
                        }

                    }
                    return null;
                    break;
                }

            //x4 Rocket fire, the same Tier 0 but get more from pool
            case 1:
                {
                    for (int i = 0; i < pooledAlliesWeapon2.Count; i++)
                    {
                        if (!pooledAlliesWeapon2[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledAlliesWeapon2[i];
                        }

                    }
                    return null;
                    break;
                }
            case 2: //Shield
                {
                    for (int i = 0; i < pooledAlliesWeapon3Tier1.Count; i++)
                    {
                        if (!pooledAlliesWeapon3Tier1[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledAlliesWeapon3Tier1[i];
                        }

                    }
                    return null;
                    break;
                }
            default: return null;
        }

    }

    public GameObject GetPooledAlliesWeaponTier2(int number)
    {
        switch (number)
        {
            //Fire Thrower
            case 0:
                { 
                    return null;
                    break;
                }

            //x12 Rocket fire CHƯA SỬA GÌ*
            case 1:
                {
                    for (int i = 0; i < pooledAlliesWeapon2.Count; i++)
                    {
                        if (!pooledAlliesWeapon2[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledAlliesWeapon2[i];
                        }

                    }
                    return null;
                    break;
                }
            case 2: //CHƯA SỬA Gì
                {
                    for (int i = 0; i < pooledAlliesWeapon3Tier1.Count; i++)
                    {
                        if (!pooledAlliesWeapon3Tier1[i].activeInHierarchy)        //Nếu GameObject i đang không hiển thị ở ngoài Hierachy thì...
                        {
                            return pooledAlliesWeapon3Tier1[i];
                        }

                    }
                    return null;
                    break;
                }
            default: return null;
        }

    }
}
