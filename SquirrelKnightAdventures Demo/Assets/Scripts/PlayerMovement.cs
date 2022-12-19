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
        CheckIsGrounded();
        CalculateMovement();

        if (Input.GetKey(KeyCode.Space) && CheckIsGrounded())
        {
            CalculateJump();
        }
    }
    public bool CheckIsGrounded()
    {
        
        float _distanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, _distanceToTheGround + 0.1f);
        DrawThickLine(transform.position, transform.position + Vector3.down * (_distanceToTheGround + 0.1f), isGrounded ? Color.green : Color.red, .1f);
        bool ground = Physics.SphereCast(transform.position, 500, Vector3.down, out RaycastHit hitInfo, _distanceToTheGround + 0.1f);
        Debug.Log(ground);
        return ground;
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
        //bool hasObstacle = Physics.Raycast(transform.position, moveDirection, _distanceToBarrier + 0.1f);

        // visualize the raycast using Debug.DrawRay
        //DrawThickLine(transform.position, transform.position + moveDirection * (_distanceToBarrier + 0.1f), hasObstacle ? Color.red : Color.green, .1f);
        return Physics.BoxCast(transform.position, Vector3.one * _distanceToBarrier, moveDirection, out RaycastHit hitInfo, Quaternion.identity, _distanceToBarrier + 0.1f);
    }

    private void CalculateJump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForceMultiplier, ForceMode.Impulse);
    }
    private void DrawThickLine(Vector3 start, Vector3 end, Color color, float thickness)
    {
        // calculate the offset vectors
        Vector3 offset = (end - start).normalized * thickness * 0.5f;
        Vector3 offset1 = Quaternion.Euler(0, 90, 0) * offset;
        Vector3 offset2 = Quaternion.Euler(0, -90, 0) * offset;

        // draw the lines with the offset vectors
        Debug.DrawLine(start - offset1, end - offset1, color);
        Debug.DrawLine(start - offset2, end - offset2, color);
        Debug.DrawLine(start + offset1, end + offset1, color);
        Debug.DrawLine(start + offset2, end + offset2, color);
    }
}
