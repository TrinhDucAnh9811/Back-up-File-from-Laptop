using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUXmanager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI timerText;
    public GameObject reloadingText;
    public GameObject escMenu;
    private Shooting shooting;

    public TextMeshProUGUI ammo1;
    public TextMeshProUGUI ammo2;
    public TextMeshProUGUI ammo3;
        

    private void Start()
    {
        shooting = FindAnyObjectByType<Shooting>();
    }
    void Update()
    {
        levelText.text = "Level: " + PlayerStats.instance.level;
        expText.text = "Exp: " + PlayerStats.instance.experience;
        timerText.text = "Timer: " + Time.time;

        //Display Ammo each WP:
        ammo1.text = shooting.currentAmmo1.ToString();
        ammo2.text = shooting.currentAmmo2.ToString();
        ammo3.text = shooting.currentAmmo3.ToString();

        //Reloading text:
        if (shooting.isReloading1 == true) 
        {
            reloadingText.SetActive(true);
        }
        else if(shooting.isReloading1 == false)
        {
            reloadingText.SetActive(false);
        }
        

        //ESC Menu:
        if(Input.GetKeyDown(KeyCode.Escape))
            {
            escMenu.SetActive(true);
            Time.timeScale = 0f;
            }
    }
}
