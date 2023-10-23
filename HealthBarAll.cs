using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAll : MonoBehaviour
{
    public Image healthBarSprite;
    private Camera cam;

    public float currentHealth;
    public float maxHealth;

   
    void Start()
    {

        cam = Camera.main;
        currentHealth = maxHealth;
    }
    void Update()
    {
        transform.position = GetComponentInParent<Transform>().position;
        transform.rotation = transform.rotation; /*Quaternion.LookRotation(transform.position - Camera.main.transform.position);*/
        UpdateHealthBar(maxHealth, currentHealth);
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarSprite.fillAmount = currentHealth / maxHealth;
    }

    private void OnCollisionEnter(Collision other) //Khi Enemy Building bị trúng đạn  
    {
        if (other.gameObject.CompareTag("Player_Weapon"))
        {
            /*Destroy(other.gameObject);*/
            other.gameObject.SetActive(false);
            TakeDamage(PlayerStats.instance.baseDamage);
            Instantiate(PlayerController.instance.GetComponent<Shooting>().hitParticle, transform.position, Quaternion.identity);  //Partical when hit

            AudioManager.instance.PlayTargetSound(2); ////SSSSSSSSSSSSSSSSSSSSS
        }

        if (other.gameObject.CompareTag("Enemy_Weapon"))
        {
            /*Destroy(other.gameObject);*/
            other.gameObject.SetActive(false);
            TakeDamage(PlayerStats.instance.baseDamage);
            Instantiate(PlayerController.instance.GetComponent<Shooting>().hitParticle, transform.position, Quaternion.identity);  //Partical when hit

            AudioManager.instance.PlayTargetSound(2); ////SSSSSSSSSSSSSSSSSSSSS
        }
    }

    private void OnTriggerStay(Collider other) //FIRE THROWER
    {
        if (other.gameObject.CompareTag("Player_Weapon"))
        {
            /*Destroy(other.gameObject);*/
            TakeDamage(10);
            Instantiate(PlayerController.instance.GetComponent<Shooting>().hitParticle, transform.position, Quaternion.identity);  //Partical when hit

            AudioManager.instance.PlayTargetSound(2); ////SSSSSSSSSSSSSSSSSSSSS
        }
    }

    public void TakeDamage(int damage) //Sau này sẽ là hàm override từ class Living Entity
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
