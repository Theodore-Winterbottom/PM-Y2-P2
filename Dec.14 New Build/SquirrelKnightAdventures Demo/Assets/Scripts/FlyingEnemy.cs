using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float lineOfSite;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float shootingRnage;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject bulletParent;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private float nextFireTime;

    [SerializeField]
    private int health;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRnage)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRnage && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        
    }

    public void TakeDamage(int damage)
    {

        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        //Kills the emeny when health reachs zero
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRnage);

    }
}
