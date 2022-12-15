using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShottingEnemy : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minimumDistance;

    public GameObject projectile;
    public float timeBetweenShots;
    private float nextshotTime;
    public EnemyAi enemyAi;

    private void Update()
    {
        if(Time.time > nextshotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextshotTime = Time.time + timeBetweenShots;
        }

        if (enemyAi.playerInAttackRange == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }

    }
}
