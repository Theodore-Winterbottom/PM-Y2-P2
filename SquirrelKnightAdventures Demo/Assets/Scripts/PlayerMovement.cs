using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    [Header("Movement Customization")]
    [Space]
    [SerializeField] private float playerSpeedMultiplier;
    [SerializeField] private float jumpForceMultiplier;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        CalculateMovement();

        if (Input.GetKey(KeyCode.Space) && CheckIsGrounded())
        {
            CalculateJump();
        }
    }
    public bool CheckIsGrounded()
    {
        float _distanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, _distanceToTheGround + 0.1f);
    }
    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, 0);

        if (!CheckIfObstacles(direction))
        {
            transform.Translate(direction * playerSpeedMultiplier * Time.deltaTime);
        }
    }
    public bool CheckIfObstacles(Vector3 moveDirection)
    {
        float _distanceToBarrier = GetComponent<Collider>().bounds.extents.x;
        return Physics.Raycast(transform.position, moveDirection, _distanceToBarrier + 0.1f);
    }
    private void CalculateJump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForceMultiplier * 200 * Time.deltaTime);
    }
}
