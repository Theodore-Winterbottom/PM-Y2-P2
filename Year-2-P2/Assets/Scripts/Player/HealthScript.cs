using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [Tooltip("Place the Stats game object Here")][SerializeField] public Stats stats;

    [Tooltip("Place the HealthScript game object Here")][SerializeField] public Enemy enemy;

    [Header("Player Damage Script")]

    [Space()]

    [Tooltip("Place the Health Slider Here")] [SerializeField] private Slider slider;

    [Range(0f, 30f)]
    [Tooltip("Amount of damage to apply when the object is hit")] [SerializeField] private float damageAmount = 16.6f;

    [Range(0f, 100f)]
    [Tooltip("The current health of the object")] [SerializeField] private float health = 100f;

    [Tooltip("Damage to be applied when the object is hit")] [SerializeField] private float damage = 16.6f;

    // Method to take damage
    private void TakeDamage(float damage, GameObject other)
    {
        // Reduce the current health by the damage amount
        health -= damage;


        // Check if the object's health has onehundred health
        if (health <= 100)
        {
            // If the object gets attacked by a enemy health goes down
            SetMaxHealth(damage);
        }
        // Check if the object's health has reached zero health
        if (health <= 1.001f)
        {
            // If the object's health is zero or less, destroy it
            Destroy(gameObject);
            stats.Playerkilled(other);
            stats.EnemyKilled(other);
        }

    }

    // Player's health goes down
    private void SetMaxHealth(float health)
    {
        // Player's health goes down
        slider.value -= health;
    }

    // When the player is hit by an enemy player takes damage
    private void OnCollisionEnter(Collision collision)
    {
        // Enemy deals damage to the player if enemy hits player
        if (collision.gameObject.tag == "Player")
        {
            // Apply damage to the player using the HealthScript component
            TakeDamage(damageAmount, collision.gameObject); 

            
        }
    }

   

}
