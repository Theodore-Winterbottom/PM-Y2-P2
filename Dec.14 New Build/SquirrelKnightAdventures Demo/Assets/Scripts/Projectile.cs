using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 targetPosition;
    public float speed;
    public HealthScript healthScript;

    private void Start()
    {
        //targetPosition = FindObjectOfType<Rigidbody>().transform.position;
        targetPosition = GameObject.Find("Player").transform.position;
        healthScript = GameObject.Find("Player").GetComponent<HealthScript>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if(transform.position == targetPosition)
        {
            healthScript.TakeDamage(healthScript.damageAmount);
            Destroy(gameObject);
        }
    }
    
}
