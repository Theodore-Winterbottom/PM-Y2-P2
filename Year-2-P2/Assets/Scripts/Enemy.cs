using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public PlayerHP playerHP;
    public EnemyDamage enemyDamage;
    public int damage = 25;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerHP playerHP = other.GetComponent<PlayerHP>();

        if (playerHP != null)
        {
            TakeDamage(damage);
        }
    }
    
     void TakeDamage(int damage)
    {
        enemyDamage.currentHealth -= damage;
        
    }


}
