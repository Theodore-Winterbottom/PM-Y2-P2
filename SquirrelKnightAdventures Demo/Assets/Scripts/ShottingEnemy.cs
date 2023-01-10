using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShottingEnemy : MonoBehaviour
{
    [Header("Shooting Enemey Script")]

    [Range(0,1)]
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private float minimumDistance;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float nextshotTime;
    [SerializeField] private EnemyAi enemyAi;

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
