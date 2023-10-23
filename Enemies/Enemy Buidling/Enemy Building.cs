using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilding : MonoBehaviour
{
    protected float maxHealth;
    protected float currentHealth;
    protected float damage;
    protected float spawnRate;
    protected float enemyIndex;
    protected float maxEnemySpawned;
    protected bool canSpawned;
    [SerializeField] protected float gameTimer; //Depend on the time enemy will be spawned more or type of enemy

    protected int enemyCount; //Tổng số quái đang được spawn
    protected Vector3 spawnPosition;
    protected int currentWave; // Biến để theo dõi số quái của đợt hiện tại
    protected GameObject[] enemy_Alive;

    protected IEnumerator WaitToSpawn(float spawnRate)
    {
        yield return new WaitForSeconds(spawnRate);
        canSpawned = true;
    }
}
