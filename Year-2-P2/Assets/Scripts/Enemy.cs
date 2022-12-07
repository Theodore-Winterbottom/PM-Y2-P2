using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerManagment playerManagment;
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    /*void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //enemyRb.AddForce((player.transform.position - transform.position).normalized * speed);
    }
    public void DamageEvent()
    {
        //playerManagment.SetDamage(10);
    }*/

    public int damage = 25;
    private void OnTriggerEnter(Collider other)
    {
        PlayerManagment playerManagment = other.GetComponent<PlayerManagment>();

        if (playerManagment != null)
        {
            playerManagment.TakeDamage(damage);
        }
    }
}
