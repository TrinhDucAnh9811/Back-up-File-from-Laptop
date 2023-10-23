using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonShopFunction : MonoBehaviour
{
    public GameObject upgradeCanvas;

    private Button button;
    [SerializeField] private int index;
    void Start()
    {
        upgradeCanvas = GameObject.Find("Upgrade Option");

        button = GetComponent<Button>();
        button.onClick.AddListener(() => SetFunction(index));
    }
    public void SetFunction(int index)
    {
        Debug.Log(button.gameObject.name + " was clicked");
        switch (index)
            {
            case 0:
                PlayerStats.instance.HealthIncrease();
                break;
            case 1:
                PlayerStats.instance.Healing();
                break;
            case 2:
                PlayerStats.instance.DamageIncrease();
                break;
            case 3:
                PlayerStats.instance.MoveSpeedIncrease();
                break;
            default:
                Debug.LogWarning("Unknown index: " + index);
                break;
        }
        upgradeCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        
    }
}
