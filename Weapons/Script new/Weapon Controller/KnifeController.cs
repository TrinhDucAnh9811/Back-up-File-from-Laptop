using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController //Inheritance
{
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.prefab,transform.position, weaponData.prefab.transform.rotation);   //Nếu bị lỗi rotation của vũ khí, xem ở đây
        /*spawnedKnife.transform.position = transform.position;*/
        spawnedKnife.GetComponent<KnifeBehavior>().DirectionChecker(PlayerController.instance.transform.forward); //Reference and set direction

    }
}
