using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagment : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthbar;

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        healthbar.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("You're dead");
        }
    }




    /*public static int helth1 = 100;
    public Slider healthBar;
    public static bool gameOvrer;
    // Start is called before the first frame update
    void Start2()
    {
        gameOvrer = false;
    }

    // Update is called once per frame
    void Update()
    {

    }*/
}
