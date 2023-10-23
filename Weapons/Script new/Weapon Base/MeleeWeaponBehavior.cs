using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary:
// Base Script of all melee behaviors [To be placed on a prefab of a weapon that's melle]
public class MeleeWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    public float destroyAfterSeconds;
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    
}
