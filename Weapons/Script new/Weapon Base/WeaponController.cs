using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    float currentCooldown;


    protected virtual void Start()
    {
        currentCooldown = weaponData.cooldownDuration; 
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        
        if(currentCooldown <=0)
        {
            Attack();
        }    
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.cooldownDuration;
    }
}
