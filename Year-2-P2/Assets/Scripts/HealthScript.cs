using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public HealthBar healthBar;

    // The current health of the object
    public float health = 100f;

    // The maximum health of the object
    public float maxHealth = 100f;

    // Damage to be applied when the object is hit
    public float damage = 16.6f;

    // Method to take damage
    public void TakeDamage(float damage)
    {
        // Reduce the current health by the damage amount
        health -= damage;
        healthBar.SetMaxHealth(damage);

        // Check if the object's health has reached zero
        if (health <= 1.001f)
        {
            // If the object's health is zero or less, destroy it
            Destroy(gameObject);
        }
    }

    // Method to restore health
    /*public void RestoreHealth()
    {
        // Increase the current health by the healing amount
        health = Mathf.Min(health + 16.6f, maxHealth);
    }*/
}
