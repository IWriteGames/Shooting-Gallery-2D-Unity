using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private SpriteRenderer duckSprite;


    private int totalHealth;

    public void StartHealthBar(int currentHealth)
    {
        totalHealth = currentHealth;
        slider.minValue = 0;
        slider.maxValue = currentHealth;
    }

    public void UpdateHealthBar(int currentHealth)
    {
        if(currentHealth < totalHealth)
        {
            fillImage.color = new Color32(255, 182, 0, 255);
        } 
        
        if (currentHealth == 0) 
        {
            fillImage.color = new Color32(0, 0, 0, 255);
            duckSprite.color = new Color32(255, 255, 255, 0);
        }
        
        slider.value = currentHealth;

    }

}
