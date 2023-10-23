using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Image fillImage;
    public Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        float fillValue = PlayerStats.instance.currentHealth / PlayerStats.instance.maxHealth;
        slider.value = fillValue;
        if (slider.value <= slider.maxValue / 3)
        {
            fillImage.color = Color.yellow;
        }
        if (slider.value > slider.maxValue / 3)
        {
            fillImage.color = Color.red;
        }
    }
}
