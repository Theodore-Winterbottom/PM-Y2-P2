using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchingEnemy : MonoBehaviour
{
    public float rotationSpeed;
    public Rigidbody rb;

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
