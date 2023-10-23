using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    //Skill Bomb variables:
    public float delay = 3.0f;
    private float countdown;
    private bool hasExploded = false;
    public float radius = 50.0f;
    public float explosionForce = 1000.0f;
    /*    private bool bombCreated = false;*/
    /*    public GameObject bombPrefab;*/
    public GameObject exlosionParticle;
    void Start()
    {
        //Initial bomb timer:
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {

        /*Instantiate(bombPrefab);
        bombPrefab.transform.position = transform.position;
        bombPrefab.transform.rotation = transform.rotation;
        Rigidbody bombRb = bombPrefab.GetComponent<Rigidbody>();
        bombRb.AddForce(transform.forward * 400.0f, ForceMode.Impulse);*/
        if (gameObject.activeSelf)
        {
            countdown -= Time.deltaTime;


            if (countdown <= 0f && !hasExploded)
            {
                Explode();
                hasExploded = false;
                countdown = delay;
                /*StartCoroutine(Wait());*/
                

            }
        }
    }
/*
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        hasExploded = false;
    }*/


    void Explode()
    {

        //Show Effect:
        Instantiate(exlosionParticle, transform.position, transform.rotation);

        AudioManager.instance.PlayTargetSound(5); ////SSSSSSSSSSSSSSSSSSSSS

        //Handle impact after explode:
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);

            }


            // Check if the nearby object is an enemy and apply damage if applicable
            Enemies enemy = nearbyObject.GetComponent<Enemies>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }
        }

    }
}

