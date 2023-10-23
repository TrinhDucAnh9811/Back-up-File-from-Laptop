using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Summary: Script for spawning Enemy Waves after destroy 1 wave
public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameTimer;
    [SerializeField] private int enemyCount; //Tổng số quái đang được spawn
    [SerializeField] private Vector3 spawnPosition;
    private int currentWave = 0; // Biến để theo dõi số quái của đợt hiện tại
    private GameObject[] enemy_Alive;

    private UIUXmanager uiManager;
    private void Start()
    {
        uiManager = FindObjectOfType<UIUXmanager>();

        gameTimer = 0;
        SpawnEnemies(0);
    }
    void Update()
    {
        gameTimer += Time.deltaTime;

        enemy_Alive = GameObject.FindGameObjectsWithTag("Enemy");  //Tìm kiếm ltuc để xem còn enemy nào sống không?

        if (gameTimer > 0 && gameTimer < 30) //Trong x phút đầu
        {
            if (enemy_Alive.Length <= 5)
            {
                currentWave++;
                SpawnEnemies(0);
            }
        }
        else if(gameTimer < 60)
        {
            currentWave = 0;
            if (enemy_Alive.Length <= 10)
            {
                
                currentWave++;
                SpawnEnemies(Random.Range(0,3));
            }    
        }
        else if(gameTimer <= 90)
        {
            currentWave = 0;
            if (enemy_Alive.Length <= 10)
            {
                
                currentWave++;
                SpawnEnemies(Random.Range(0, 3));
            }
        }
        else if(gameTimer < 120)
        {
            currentWave = 0;
            if (enemy_Alive.Length <= 20)
            {

                currentWave++;
                SpawnEnemies(Random.Range(0, 1));
            }
        }   
        else if (gameTimer <150)
        {
            currentWave = 0;
            if (enemy_Alive.Length <= 15)
            {

                currentWave++;
                SpawnEnemies(Random.Range(0, 3));
            }
        }

        else if(gameTimer <160)
        {
            currentWave = 0;
            if (enemy_Alive.Length <= 15)
            {

                currentWave++;
                SpawnEnemies(4);
            }
        }
        else
        {
            currentWave = 0;
            if (enemy_Alive.Length <= 15)
            {

                currentWave++;
                SpawnEnemies(Random.Range(0, 3));
            }
        }
    }

    void SpawnEnemies(int number)
    {
        enemyCount = (currentWave + 3);
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = EnemyPool.instance.GetPooledEnemies(number);
            if (enemy != null)
            {
                enemy.transform.position = PlayerController.instance.transform.position + new Vector3(Random.Range(50, 60), 0, Random.Range(50, 60)); ;
                enemy.SetActive(true);
                StartCoroutine(WaitToSpawn(3));
            }
        }
    }

    IEnumerator WaitToSpawn(int time)
    {
        yield return new WaitForSeconds(time);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale =1.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        uiManager.escMenu.SetActive(false);
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

}