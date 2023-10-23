using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weaponData.prefab);
        spawnedGarlic.transform.position = PlayerController.instance.transform.position + new Vector3(0,-1.0f, 0);
        spawnedGarlic.transform.parent = transform; //Không có thì không đi theo player
        spawnedGarlic.transform.rotation = weaponData.prefab.transform.rotation;
    }
}
