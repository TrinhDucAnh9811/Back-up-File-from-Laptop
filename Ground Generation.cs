using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneration : MonoBehaviour
{
    private Vector3 lastSpawnPosition;
    public GameObject groundSpawn;
    public float spawnDistance = 90f; // The distance at which to spawn the ground.

    public int maxGrounds = 3; // max Grounds to exist

    private Queue<GameObject> spawnedGrounds = new Queue<GameObject>(); //DSA: Hàng đợi queue

    void Start()
    {
        lastSpawnPosition = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;

        // Calculate the absolute difference between the player's position and the last spawn position.
        float distanceMoved = Vector3.Distance(playerPosition, lastSpawnPosition);

        // If the player has moved far enough, spawn the ground and update the last spawn position.
        if (distanceMoved >= spawnDistance)
        {
            SpawnGround(playerPosition);
        }
        if (spawnedGrounds.Count > maxGrounds)
        {
            DestroyGround();
        }
    }

    private void SpawnGround(Vector3 playerPosition)
    {
        GameObject newGround = Instantiate(groundSpawn, new Vector3(playerPosition.x, 0, playerPosition.z), Quaternion.identity);
        spawnedGrounds.Enqueue(newGround);
        lastSpawnPosition = playerPosition;
    }

    private void DestroyGround()
    {
        if(spawnedGrounds.Count >=0)
        {
            GameObject oldGround = spawnedGrounds.Dequeue();
            Destroy(oldGround);
        }    
    }    
}
