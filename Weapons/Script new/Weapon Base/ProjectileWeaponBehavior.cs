using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary:
// Base Script for all projectile behaviors [To be placed on a Prefab of a weapon that's a projectile
public class ProjectileWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }


    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }
}
