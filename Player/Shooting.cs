using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //Shoot variables:
    public Transform firePoint;
    public GameObject projectilePrefabs;
    public float projectileSpeed = 100.0f;

    //Reload Variables:
    //Weapon1: 
    public int currentAmmo1;
    public bool isReloading1 = false;

    //Weapon2:
    public int currentAmmo2;
    private bool isReloading2 = false;

    //Weapon3:
    public int currentAmmo3;
    private bool isReloading3 = false;



    public ParticleSystem fireParticle; //At fire point
    public ParticleSystem hitParticle; //On trigger Enter

    public ParticleSystem rocketParticle;
    


    void Start()
    {
        currentAmmo1 = PlayerStats.instance.ammoSize1;
        currentAmmo2 = PlayerStats.instance.ammoSize2;
        currentAmmo3 = PlayerStats.instance.ammoSize3;
    }

    void Update()
    {
        //WP1:
        if (Input.GetMouseButtonDown(0) && isReloading1 ==false)
        { 
            MainPlayerShoot(0);
            AudioManager.instance.PlayTargetSound(1); ////SSSSSSSSSSSSSSSSSSSSS
            currentAmmo1--;
        }
        //Reload check WP1:
        if (currentAmmo1 <= 0 && !isReloading1)
        {
            isReloading1 = true;
            StartCoroutine(Reload1(PlayerStats.instance.reloadTime1));
        }
        //Reload Manual:
        if(Input.GetKeyDown(KeyCode.R)) 
        { 
            isReloading1 = true;
            StartCoroutine(Reload1(PlayerStats.instance.reloadTime1));
        }

        //WP2:
        if (Input.GetKeyDown(KeyCode.F) && isReloading2 == false)
        { 
            MainPlayerShoot(1);
            AudioManager.instance.PlayTargetSound(4); ////SSSSSSSSSSSSSSSSSSSSS
            currentAmmo2--;
        }
        //Reload check WP2:
        if (currentAmmo2 <= 0 && !isReloading2)
        {
            isReloading2 = true;
            StartCoroutine(Reload2(PlayerStats.instance.reloadTime2));
        }

        //WP3:
        if (Input.GetMouseButtonDown(1) && !isReloading3)
        { 
            MainPlayerShoot(2);
            AudioManager.instance.PlayTargetSound(6); ////SSSSSSSSSSSSSSSSSSSSS
            currentAmmo3--;
        }
        //Reload check WP3:
        if (currentAmmo3 <= 0 && !isReloading3)
        {
            isReloading3 = true;
            StartCoroutine(Reload3(PlayerStats.instance.reloadTime3));
        }


    }

    IEnumerator Reload1(float time1)
    {

        yield return new WaitForSeconds(time1);
        currentAmmo1 = PlayerStats.instance.ammoSize1;
        isReloading1 = false;
    }

    IEnumerator Reload2(float time2)
    {
        yield return new WaitForSeconds(time2);
        currentAmmo2 = PlayerStats.instance.ammoSize2;
        isReloading2 = false;
    }
    IEnumerator Reload3(float time3)
    {
        yield return new WaitForSeconds(time3);
        currentAmmo3 = PlayerStats.instance.ammoSize3;
        isReloading3 = false;
    }
    

        public void MainPlayerShoot(int index)
    {
        GameObject projectile = ProjectilePool.instance.GetPooledObject(index);
        switch (index)
        {
            case 0:
                {
                    if (projectile != null)
                    {
                        Instantiate(fireParticle, transform.position, Quaternion.identity);
                        projectile.transform.position = firePoint.position; 
                        projectile.transform.rotation = transform.rotation;

                        projectile.SetActive(true);

                        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

                        projectileRb.velocity = firePoint.forward * projectileSpeed;  //Bắn theo hướng thẳng của firepoint
                        StartCoroutine(AutoDestruct(projectile, 2f));
                    }
                    break;
                }
            case 1:
                {
                    if (projectile != null)
                    {
                        Instantiate(fireParticle, transform.position, Quaternion.identity);
                        projectile.transform.position = firePoint.position;
                        projectile.transform.rotation = transform.rotation;

                        projectile.SetActive(true);

                        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

                        projectileRb.velocity = transform.forward * 30;
                        /*projectileRb.velocity = transform.up * 10;*/
                        /*StartCoroutine(AutoDestruct(projectile, 5f));*/
                    }
                    break;
                }
            case 2:
                {
                    if (projectile != null)
                    {
                        Instantiate(rocketParticle, transform.position, Quaternion.identity);
                        Instantiate(fireParticle, transform.position, Quaternion.identity);
                        projectile.transform.position = transform.position + new Vector3(0, 8, 0); //Vị trí tên lửa ở trên đầu Player
                        projectile.transform.rotation = transform.rotation;
                        projectile.SetActive(true);
                    }
                    break;
                }



                IEnumerator AutoDestruct(GameObject x, float time)
                {
                    yield return new WaitForSeconds(time);
                    x.SetActive(false);
                    projectile.transform.position = new Vector3(0, 0, 0);
                    projectile.transform.rotation = Quaternion.identity;
                }
        }
    }
}

