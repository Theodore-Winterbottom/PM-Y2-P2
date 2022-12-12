using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerScript : MonoBehaviour
{
    // Reference to the HealthScript component on the player
    public HealthScript playerHealth;

    // Amount of damage to apply when the player is hit
    public float damageAmount = 16.6f;

    // Method to be called when the player is hit by an enemy
    public void DamagePlayer()
    {
        // Check if the player's health script is defined
        if (playerHealth != null)
        {
            // Apply damage to the player using the HealthScript component
            playerHealth.TakeDamage(damageAmount);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            DamagePlayer();
        }
    }
}

