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
    public RaycastHit hit;

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
        Vector3 center = GetComponent<Collider>().bounds.center;
        RaycastHit hit1;
        return Physics.BoxCast(GroundBoxRadius + center, transform.lossyScale /2, -transform.up, out hit, transform.rotation,
            1, layermask, QueryTriggerInteraction.UseGlobal);

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
        Vector3 center = GetComponent<Collider>().bounds.center;
        if (moveDirection.x > 0) { moveDirection.x = 1f; }
        if (moveDirection.x < 0) { moveDirection.x = -1f; }
        RaycastHit hit;
        bool checkobsical = Physics.BoxCast(center, ObsicalBoxRadius + transform.lossyScale /2, moveDirection, out hit, transform.rotation,
            1, layermask, QueryTriggerInteraction.UseGlobal);
        return checkobsical;
    }

    private void CalculateJump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForceMultiplier, ForceMode.Impulse);
    }
    private void OnDrawGizmosSelected()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 center = GetComponent<Collider>().bounds.center;
        Vector3 direction = new(horizontalInput, 0, 0);
        if (direction.x > 0) { direction.x = 1f; }
        if (direction.x < 0) { direction.x = -1f; }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center + direction * hit.distance, ObsicalBoxRadius + transform.lossyScale);
    }
}