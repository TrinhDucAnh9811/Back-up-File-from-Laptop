using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building4 : EnemyBuilding
{
    void Start()
    {
        maxHealth = 5000;
        currentHealth = maxHealth;
        damage = 200f;
        spawnRate = 10;
        gameTimer = 0;
        maxEnemySpawned = 5;
        canSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;

        if (gameTimer > 0 && canSpawned ==true)
        {
            SpawnEnemies(0, spawnRate);
            canSpawned = false;
        }
        else if (gameTimer > 300 && canSpawned ==true)
        {
            maxEnemySpawned = 6;
            SpawnEnemies(5, spawnRate - 2);
            canSpawned = false;

        }
        else if (gameTimer > 1200 && canSpawned ==true)
        {
            maxEnemySpawned = 7;
            SpawnEnemies(Random.Range(1, 5), spawnRate - 5);
            canSpawned = false;

        }
        else if (gameTimer > 1800 && canSpawned ==true )
        {
            SpawnEnemies(Random.Range(3, 5), spawnRate - 5);
            canSpawned = false;
        }

    }

    void SpawnEnemies(int enemyIndex, float spawnRate)
    {
        for (int i = 0; i < maxEnemySpawned; i++)
        {
            GameObject enemy = EnemyPool.instance.GetPooledEnemies(enemyIndex);
            if (enemy != null)
            {
                enemy.transform.position = transform.position + new Vector3(Random.Range(-50, 60), 0, Random.Range(-50, 60)); ;
                enemy.SetActive(true);

                
            }
        }
        StartCoroutine(WaitToSpawn(spawnRate));
    }
}
