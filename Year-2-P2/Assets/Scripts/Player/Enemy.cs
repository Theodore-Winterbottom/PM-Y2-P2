using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0f, 30f)]
    [Tooltip("Amount of damage to apply when the object is hit")][SerializeField] private float damageAmount = 16.6f;

    [Range(0f, 100f)]
    [Tooltip("The current health of the object")][SerializeField] private float health = 100f;

    [Tooltip("Damage to be applied when the object is hit")][SerializeField] private float damage = 16.6f;

    private void OnCollisionEnter(Collision collision)
    {
        // Enemy deals damage to the player if enemy hits player
        if (collision.gameObject.tag == "Player")
        {
            // Apply damage to the player using the HealthScript component
            TakeDamage(damageAmount, collision.gameObject);


        }
    }

    public void EnemyTakeDamage(GameObject other)
    {
        if (other.gameObject.tag == "Enemy")
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
                Destroy(gameObject);
            }
        }

    }

}
