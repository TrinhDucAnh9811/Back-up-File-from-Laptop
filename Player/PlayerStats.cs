using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 
    PlayerStats : Singleton<PlayerStats>
{
    public GameObject upgradeCanvas;
    //Allies Stats (Để tạm)
    public float healAmount = 10.0f;


    //Health:
    public float maxHealth = 100;
    public float currentHealth;

    //Stats
    public float moveSpeed = 10.0f;
    public float dashCoolDown = 1;
    public int baseDamage = 5; //CHƯA GÁN BASE DAMAGE
    public float projectileSpeed = 10;
    //Reload variables:
    public float reloadTime1 = 2;
    public int ammoSize1 = 6;
    public float reloadTime2 = 5;
    public int ammoSize2 = 2;
    public float reloadTime3 = 5;
    public int ammoSize3 = 4;


    //Experience and level of player
    [HideInInspector] internal int experience = 0;
    [HideInInspector] internal int level = 0;
    [HideInInspector] internal int experienceCap = 10;
    [HideInInspector] internal int experienceCapIncrease = 20;
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if(experience >= experienceCap)
        {
            //UPGRADE PANNEL APPEAR
            AudioManager.instance.PlayTargetSound(8);
            upgradeCanvas.SetActive(true);
            Time.timeScale = 0f;
            level++;

            experience -= experienceCap;
            experienceCap += experienceCapIncrease;
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy_Weapon"))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if ((currentHealth <= 0))
        {
            AudioManager.instance.PlayTargetSound(11);
            StartCoroutine(WaitForRestartAfterDie());

        }
    }

    IEnumerator WaitForRestartAfterDie()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }

    public void Healing()
    {
        int amount = Random.Range(30, 70);
        currentHealth += amount;
        if ((currentHealth >= 0)) 
        {
            currentHealth = maxHealth;
        }
    }

    public void HealthIncrease()
    {
        int amount = Random.Range(10, 20);
        maxHealth += amount;
    } 
    
    public void DamageIncrease()
    {
        int amount = Random.Range(5, 10);
        baseDamage += amount;
    }

    public void MoveSpeedIncrease()
    {
        int amount = Random.Range(5, 8);
        moveSpeed += amount;
    }

    public void IncreaseGunAmmo()
    {
        int amount = 3;
        ammoSize1 += amount;
    }

    public void IncreaseRocketAmmo()
    {
        int amount = 1;
        ammoSize1 += amount;
    }

    public void IncreaseBombAmmo()
    {
        int amount = 1;
        ammoSize1 += amount;
    }

    public void ProjectileSpeedIncrease()
    {
        int amount = 5;
        projectileSpeed += amount;
    }

    public void ReduceReloadTime1()
    {
        float amount = 0.5f;
        reloadTime1 -= amount;
        if(reloadTime1 <=0) 
        {
            reloadTime1 = 0.5f;
        }
    }
    public void ReduceReloadTime2()
    {
        float amount = 0.5f;
        reloadTime2 -= amount;
        if (reloadTime2 <= 0)
        {
            reloadTime2 = 0.5f;
        }
    }
    public void ReduceReloadTime3()
    {
        float amount = 0.5f;
        reloadTime2 -= amount;
        if (reloadTime1 <= 0)
        {
            reloadTime3 = 0.5f;
        }
    }
}
