using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroy : MonoBehaviour
{
    [SerializeField] private float autoDestroyTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(AutoDestruct(gameObject, autoDestroyTime));
        }
    }

    IEnumerator AutoDestruct(GameObject x, float time)
    {
        yield return new WaitForSeconds(time);
        x.SetActive(false);
        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.transform.rotation = Quaternion.identity;
    }
}
