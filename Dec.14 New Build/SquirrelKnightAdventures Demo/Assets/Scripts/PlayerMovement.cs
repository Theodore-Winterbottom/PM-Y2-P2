using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    private Rigidbody PlayerRigidbody;
    [Header("Movement Customization")]
    [Space]
    [SerializeField] private float playerSpeedMultiplier;

    [SerializeField] private float jumpForceMultiplier;

    [SerializeField] private bool PlayerIsGrounded;

    [SerializeField] private bool facingRight = true;

    private void Start ()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate ()
    {
        calculateMovement();

        if (Input.GetKey(KeyCode.Space) && PlayerIsGrounded)
        {
            calculateJump();
        }
    }
    private void calculateMovement ()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0f, 0f);

        transform.Translate(direction * playerSpeedMultiplier * Time.deltaTime);

        if(horizontalInput > 0 && facingRight)
        {
            Flip();
        }

        if(horizontalInput < 0 && !facingRight)
        {
            Flip();
        }
    }
    private void calculateJump ()
    {
        PlayerRigidbody.AddForce(Vector3.up * jumpForceMultiplier);
    }
    void OnCollisionStay(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            PlayerIsGrounded = true;
        }
    }
    void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            PlayerIsGrounded = false;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
