using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody bulletRB;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag("Player");

        Vector3 moveDir = (target.transform.position - transform.position).normalized * speed;

        bulletRB.velocity = new Vector3(moveDir.x, moveDir.y);

        Destroy(this.gameObject, 2);
    }
}
