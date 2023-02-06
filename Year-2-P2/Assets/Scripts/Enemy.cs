using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Tooltip("Place the Stats game object Here")][SerializeField] public Stats stats;

    //[Tooltip("Place the HealthScript game object Here")][SerializeField] public Enemy enemy;

    [Header("Player Damage Script")]

    [Space()]

    [Tooltip("Place the Health Slider Here")][SerializeField] private Slider slider;

    [Range(0f, 30f)]
    [Tooltip("Amount of damage to apply when the object is hit")][SerializeField] private float damageAmount = 16.6f;

    [Range(0f, 100f)]
    [Tooltip("The current health of the object")][SerializeField] private float health = 100f;

    [Tooltip("Damage to be applied when the object is hit")][SerializeField] private float damage = 40f;

    private TextMeshProUGUI killCountText;

    private TextMeshProUGUI scoreText;

    //private TextMeshProUGUI bossKillCountText;

    // Kill count variable
    private int enemyKillCount;

    // Score count variable
    private int scoreCount;

    // Boss Kill count variable
    public int bossKillCount;

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
        if (collision.gameObject.tag == "Boss")
        {
            // Apply damage to the player using the HealthScript component
            TakeDamage(damageAmount, collision.gameObject);

        }
        // Enemy deals damage to the player if enemy hits player
        else if (collision.gameObject.tag == "Enemy")
        {
            // Apply damage to the player using the HealthScript component
            TakeDamage(damageAmount, collision.gameObject);

        }
    }

    // Method to add kills
    public void EnemyKilled(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Adds kills to player kill count
            enemyKillCount = enemyKillCount + 1;
            killCountText.text = "Kills: " + enemyKillCount;
            ScoreText(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            // Adds 10 kills to player kill count
            bossKillCount = bossKillCount + 1;
            killCountText.text = "Kills: " + bossKillCount;
            ScoreText(gameObject);
        }
    }

    public void ScoreText(GameObject other)
    {
        if (other.gameObject.tag == "Boss")
        {
            scoreCount = scoreCount + 10;
            scoreText.text = "Score: " + scoreCount;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            scoreCount = scoreCount + 1;
            scoreText.text = "Score: " + scoreCount;
        } 
    }
























    /*[SerializeField] public Stats stats;

    

    [Space()]

    [SerializeField] private Slider slider;

    [SerializeField] private float damageAmount = 16.6f;

    [SerializeField] private float health = 100f;

    [SerializeField] private float damage = 16.6f;

    // Kill count text
    [SerializeField]
    private TextMeshProUGUI killCountText;

    public int bossKillCount;

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
        if (collision.gameObject.tag == "Boss")
        {
            // Apply damage to the player using the HealthScript component
            EnemyTakeDamage(damageAmount, collision.gameObject);


        }
    }

    // Method to take damage
    public void EnemyTakeDamage(float damage, GameObject other)
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
                BossKilled(gameObject);
            }
        }

    }

    public void BossKilled(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Adds 10 kills to player kill count
            bossKillCount = bossKillCount + 10;
            killCountText.text = "Kills: " + bossKillCount;
        }
    }*/


}














/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Place the Stats game object Here")]
    
    [Header("Player Damage Script")]

    [Tooltip("Place the Health Slider Here")]

    [Range(0f, 30f)]
    [Tooltip("Amount of damage to apply when the object is hit")]

    [Range(0f, 100f)]
    [Tooltip("The current health of the object")]

    [Tooltip("Damage to be applied when the object is hit")]


    private float damageAmount = 16.6f;

    private float health = 100f;

    private float damage = 16.6f;

    private void OnCollisionEnter(Collision collision)
    {
        // Enemy deals damage to the player if enemy hits player
        if (collision.gameObject.tag == "Player")
        {
            // Apply damage to the player using the HealthScript component
            TakeDamage(damageAmount, collision.gameObject);


        }
    }

    

}*/
