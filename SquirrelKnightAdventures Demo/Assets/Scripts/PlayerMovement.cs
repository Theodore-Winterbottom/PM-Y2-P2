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
    [SerializeField] private LayerMask layermask;
    [SerializeField] private Vector3 GroundBoxRadius;
    [SerializeField] private Vector3 ObsicalBoxRadius;
    [SerializeField] private float distanceToGroundOffset;
    private Vector3 wallOffset;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        CalculateMovement();

        if (CheckIsGrounded() && Input.GetKey(KeyCode.Space))
        {
            CalculateJump();
        }
    }

    public bool CheckIsGrounded()
    {
        Vector3 center = GetComponent<Collider>().bounds.center;

        RaycastHit hit;
        bool groundCheck =  Physics.BoxCast(GroundBoxRadius + center + wallOffset, transform.lossyScale /2 - new Vector3(.01f,0,.01f), 
            -transform.up, out hit, transform.rotation, 1, layermask, QueryTriggerInteraction.UseGlobal);
        Debug.Log(transform.lossyScale / 2 - new Vector3(.1f, 0, .1f));
        if (groundCheck)
        {
            playerRigidbody.velocity = playerRigidbody.velocity.RemoveComponent(Physics.gravity);
        }

        return groundCheck;
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new(horizontalInput, 0, 0);

        if (!CheckIfObstacles(direction))
        {
            
            transform.Translate(direction * playerSpeedMultiplier * Time.deltaTime);
        }
    }

    public bool CheckIfObstacles(Vector3 moveDirection)
    {
        wallOffset = Vector3.zero;

        if (moveDirection.x > 0) { moveDirection.x = 1f; wallOffset.x = -.2f;}
        if (moveDirection.x < 0) { moveDirection.x = -1f; wallOffset.x = .2f;}

        Vector3 center = GetComponent<Collider>().bounds.center;

        RaycastHit hit;
        bool obstacleCheck = Physics.BoxCast(center, ObsicalBoxRadius + transform.lossyScale /2, moveDirection, out hit, transform.rotation,
            1, layermask, QueryTriggerInteraction.UseGlobal);

        if (obstacleCheck)
        {
            
            
            playerRigidbody.velocity = playerRigidbody.velocity.RemoveComponent(moveDirection);
        }

        return obstacleCheck;
    }

    private void CalculateJump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForceMultiplier, ForceMode.Impulse);
    }
}

public static class VectorExtensions
{
    public static Vector3 RemoveComponent(this Vector3 vector, Vector3 direction)
    {
        direction = direction.normalized;

        return vector - direction * Vector3.Dot(vector, direction);
    }
}
