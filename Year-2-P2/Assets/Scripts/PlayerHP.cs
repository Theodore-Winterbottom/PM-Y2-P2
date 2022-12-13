using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public HealthScript healthScript;
    public Slider slider;
    public Image fill;

    
    public int currentHealth;
    public GameObject maxHealth;
    //public float maxHealth = 100f;

    private void Start()
    {
       
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        //slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        slider.value = healthScript.health;
    }
}
