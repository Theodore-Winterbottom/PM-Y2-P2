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

    private void Update()
    {
        if(Time.time > nextshotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextshotTime = Time.time + timeBetweenShots;
        }

        if (Vector3.Distance(transform.position, target.position) < minimumDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }

    }
}
