using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    //Skill Bomb variables:
    public float delay = 5.0f;
    private float countdown;
    private bool hasExploded = false;
    public float radius = 50.0f;
    public float explosionForce = 1000.0f;

    public GameObject exlosionParticle;
    void Start()
    {
        //Initial bomb timer:
        countdown = delay;
    }


    void Update()
    {

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

        /*AudioManager.instance.PlayTargetSound(5); ////SSSSSSSSSSSSSSSSSSSSS*/

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
            PlayerStats player = nearbyObject.GetComponent<PlayerStats>();
            if (player != null)
            {
                PlayerStats.instance.TakeDamage(25);
            }
        }

    }
}
