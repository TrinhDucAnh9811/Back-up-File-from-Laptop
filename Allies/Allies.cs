using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Allie : MonoBehaviour
{
    //Chỉ số đặc điểm của mỗi Allies:
    [SerializeField] private int skillIndex; //Skill
    [SerializeField] private int skillTier0 = 0; //Ban đầu skill đều =0 (Tier 0) //GUN
    [SerializeField] private int skillTier1 = 0; //Ban đầu skill đều =0 (Tier 0) //Rocket
    [SerializeField] private int skillTier2 = 0; //Ban đầu skill đều =0 (Tier 0) //Healing/Shield (Allies Tacticer)
    [SerializeField] private int skillTier3 = 0; //Ban đầu skill đều =0 (Tier 0) //(Allies Sniper) //CHƯA LÀM

    public GameObject fireThrowerPrefab;

    //Gun
    private float timeBetweenAttack1 = 3.0f; //Gun
    private float fireTime1 = 6;//Gun Tier1

    private float timeBetweenAttack2 = 4.0f; //Rocket
    private float fireTime2 = 6;//Rocket Tier 1: x6
    private float fireTime2_1 = 12;//Rocket Tier2: x12

    private float timeBetweenAttack3 = 30.0f; //Healing
    private float timeBetweenAttack3Tier1 = 30.0f; //Shield

    float originalTime1;
    float originalTime2;
    float originalTime3;
    float originalTime3Tier1;

    [SerializeField] private float projectileSpeed;

    [SerializeField] private float moveSpeed = 10.0f;

    private Rigidbody alliesRb;
    private NavMeshAgent allies;
    public float followDistance = 10f; // Khoảng cách tối thiểu để chạy theo player ????


    //Auto Shoot mechanism:
    private Shooting shooting;
    void Start()
    {
        //Set time ban đầu:
         originalTime1 = timeBetweenAttack1;
         originalTime2 = timeBetweenAttack2;
         originalTime3 = timeBetweenAttack3;
         originalTime3Tier1 = timeBetweenAttack3Tier1;

        alliesRb = GetComponent<Rigidbody>();

        allies = GetComponent<NavMeshAgent>();

        shooting = GameObject.Find("Player").GetComponent<Shooting>();
    }

    void Update()
    {
        //Gọi Allies bằng "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            allies.SetDestination(PlayerController.instance.transform.position);
        }
        else
        {
            //Goc quay theo con tro chuot
            Vector3 targetPos = PlayerController.instance.mousePos;
            targetPos.y = transform.position.y; //Match player's y-position
            Vector3 lookDirection = targetPos - alliesRb.position;
            lookDirection.y = 0; // Đảm bảo rằng không có sự xoay theo trục y
            if (lookDirection != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                alliesRb.MoveRotation(rotation);
            }
        }

        timeBetweenAttack1 -= Time.deltaTime;
        timeBetweenAttack2 -= Time.deltaTime;
        timeBetweenAttack3 -= Time.deltaTime;
        timeBetweenAttack3Tier1 -= Time.deltaTime;
        //GUN:
        if (skillIndex == 0)
        {
            if (timeBetweenAttack1 <= 0 && skillTier0 == 0) //Tier 0 Gun (x1)
            {
                AlliesShootTier0(0); 
            }
            else if(timeBetweenAttack1 <= 0 && skillTier0 ==1 && fireTime1 >0) //Tier 1 Gun (x6)
            {
                AlliesShootTier1(0); 
                fireTime1--;
                if (fireTime1 <= 0)
                {
                    timeBetweenAttack1 = originalTime1; //SAU NÀY CÓ HỆ THỐNG UPGRADE THÌ SỬA LẠI SANG SCRIPT ALLIES STATS
                    fireTime1 = 6;
                }
            }
            else if (timeBetweenAttack1 <=0 && skillTier0 == 2) //Tier 2 Gun (FireThrower) CHÚ Ý ĐANG ĐỂ TIME là -5
            {
                AlliesShootTier2(0); 
            }
        }
        //Rocket
        if (skillIndex == 1)
        {
            if (timeBetweenAttack2 <= 0 && skillTier1 == 0) //Tier 0 Rocket (x1)
            {
                AlliesShootTier0(1);
            }
            else if( timeBetweenAttack2 <= 0 &&skillTier1 == 1 && fireTime2 > 0) //Tier 1 Rocket (x6)
            {
                AlliesShootTier1(1);
                fireTime2--;
                if (fireTime2 <= 0)
                {
                    timeBetweenAttack2 = originalTime2; //SAU NÀY CÓ HỆ THỐNG UPGRADE THÌ SỬA LẠI SANG SCRIPT ALLIES STATS
                    fireTime2 = 6;
                }
            }
            else if(timeBetweenAttack2 <= 0 && skillTier1 == 2 && fireTime2_1 > 0) //Tier 2 - Rocket (x12)
            {
                AlliesShootTier2(1); 
                fireTime2_1--;
                if (fireTime2_1 <= 0)
                {
                    timeBetweenAttack2 = originalTime2; //SAU NÀY CÓ HỆ THỐNG UPGRADE THÌ SỬA LẠI SANG SCRIPT ALLIES STATS
                    fireTime2_1 = 12;
                }
            }    
        }
        //Shield
        if (skillIndex == 2)
        {
            if (timeBetweenAttack3 <= 0 && skillTier2 == 0)
            {
                AlliesShootTier0(2);
            }
            else if (timeBetweenAttack3Tier1 <= 0 && skillTier2 == 1) //CHÚ Ý: TimeBetWeenttack3Tier1
            {
                AlliesShootTier1(2); //Tier 1 - Shield 
            }
        }
    }


    public void AlliesShootTier0(int index)
    {
        GameObject projectile = AlliesProjectilePool.instance.GetPooledAlliesWeaponTier0(index);
        switch (index)
        {
            case 0://Busteds Gun
                {
                    if (projectile != null)
                    {
                       
                        /*Instantiate(fireParticle, transform.position, Quaternion.identity);*/
                        projectile.transform.position = transform.position;
                        projectile.transform.rotation = transform.rotation;

                        projectile.SetActive(true);

                        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

                        projectileRb.velocity = transform.forward * projectileSpeed;
                        /*StartCoroutine(AutoDestruct(projectile, 2f));*/

                        timeBetweenAttack1 = originalTime1; //SAU NÀY CÓ HỆ THỐNG UPGRADE THÌ SỬA LẠI SANG SCRIPT ALLIES STATS

                    }
                    break;
                }
            case 1: //Rocket
                {
                    if (projectile != null)
                    {
                        //THIẾU ÂM THANH/HIỆU ỨNG
                        projectile.transform.position = transform.position + new Vector3(0, 8, 0); //Vị trí tên lửa ở trên đầu Player
                        projectile.transform.rotation = transform.rotation;
                        projectile.SetActive(true);

                        timeBetweenAttack2 = originalTime2; //SAU NÀY CÓ HỆ THỐNG UPGRADE THÌ SỬA LẠI SANG SCRIPT ALLIES STATS
                    }
                }
                    break;
                

            case 2: //Tactic - Healing
                {
                    if (projectile != null)
                    {
                        projectile.transform.position = PlayerController.instance.transform.position; 
                        projectile.transform.rotation = PlayerController.instance.transform.rotation; //??? 
                        projectile.SetActive(true);

                        timeBetweenAttack3 = originalTime3; //SAU NÀY CÓ HỆ THỐNG UPGRADE THÌ SỬA LẠI SANG SCRIPT ALLIES STATS
                    }
                    break;
                }
        }

    }


    //TIER 1: Auto Shoot + x4 Rocket + Shield
    public void AlliesShootTier1(int index)
    {
        GameObject projectile = AlliesProjectilePool.instance.GetPooledAlliesWeaponTier1(index);
        switch (index)
        {
            case 0://Auto Gun, add Random position
                {
                    if (projectile != null)
                    {
                        Vector3 randomPosition = transform.position + new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10)) ;

                        /*Instantiate(fireParticle, transform.position, Quaternion.identity);*/
                        projectile.transform.position = randomPosition;
                        projectile.transform.rotation = transform.rotation;

                        projectile.SetActive(true);

                        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

                        projectileRb.velocity = transform.forward * projectileSpeed;
                        /*StartCoroutine(AutoDestruct(projectile, 2f));*/     

                    }
                    break;
                }
            case 1: //Rocket Tier 1 (x6)
                {
                    if (projectile != null)
                    {
                        //THIẾU ÂM THANH/HIỆU ỨNG
                        Vector3 randomPosition = transform.position + new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));

                        projectile.transform.position = randomPosition + new Vector3(0, 8, 0); //Vị trí tên lửa ở trên đầu Player
                        projectile.transform.rotation = transform.rotation;
                        projectile.SetActive(true);

                        
                    }
                }
                break;


            case 2: //Tactic - SHIELD
                {
                    if (projectile != null)
                    {
                        //THIẾU ÂM THANH/HIỆU ỨNG
                        projectile.transform.position = PlayerController.instance.transform.position;
                        projectile.transform.rotation = transform.rotation;
                        projectile.SetActive(true);

                        timeBetweenAttack3Tier1 = originalTime3Tier1; //SAU NÀY CÓ HỆ THỐNG UPGRADE THÌ SỬA LẠI SANG SCRIPT ALLIES STATS
                    }
                }
                break;
        }

    }



    public void AlliesShootTier2(int index)
    {
        GameObject projectile = AlliesProjectilePool.instance.GetPooledAlliesWeaponTier2(index);
        switch (index)
        {
            case 0://Fire Thrower
                {
                    fireThrowerPrefab.SetActive(true);
                    StartCoroutine(WaitForInactive(6));
                }
                break;
            case 1: //Rocket Tier 2 (x12)
                {
                    if (projectile != null)
                    {
                        //THIẾU ÂM THANH/HIỆU ỨNG
                        Vector3 randomPosition = transform.position + new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));

                        projectile.transform.position = randomPosition + new Vector3(0, 8, 0); //Vị trí tên lửa ở trên đầu Player
                        projectile.transform.rotation = transform.rotation;
                        projectile.SetActive(true); 
                    }
                }
                break;


            case 2: //Tactic - SHIELD
                {
                    if (projectile != null)
                    {
                        //THIẾU ÂM THANH/HIỆU ỨNG

                        projectile.transform.position = PlayerController.instance.transform.position;
                        projectile.transform.rotation = transform.rotation;
                        projectile.SetActive(true);

                        timeBetweenAttack3Tier1 = originalTime3Tier1; //SAU NÀY CÓ HỆ THỐNG UPGRADE THÌ SỬA LẠI SANG SCRIPT ALLIES STATS
                    }
                }
                break;
        }
        IEnumerator WaitForInactive(int index)
        {
            yield return new WaitForSeconds(index);
            fireThrowerPrefab.SetActive(false);
            timeBetweenAttack1 = originalTime1;
    }

    }


   
}


