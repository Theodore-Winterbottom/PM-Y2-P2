using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Rigidbody PlayerRigidbody;
    [SerializeField] private float playerSpeedMultiplier;
    [SerializeField] private float jumpForceMultiplier;
    [SerializeField] private bool PlayerIsGrounded;

    private void Start ()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
    }
    private void Update ()
    {
        if (PlayerIsGrounded)
        {
            calculateMovement();

            if (Input.GetKey(KeyCode.Space))
            {
                calculateJump();
            }
        }
    }
    private void calculateMovement ()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0f, 0f);

        transform.Translate(direction * playerSpeedMultiplier * Time.deltaTime);
    }
    private void calculateJump ()
    {
        PlayerRigidbody.AddForce(Vector3.up * jumpForceMultiplier);
    }
}
