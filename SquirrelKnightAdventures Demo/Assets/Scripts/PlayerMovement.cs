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
    [SerializeField] private Vector3 SphereRadius;
    [SerializeField] private float distanceToGroundOffset;
    private float _distanceToTheGround;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        
    }
    
    private void FixedUpdate()
    {
        Debug.Log(CheckIsGrounded());
        CalculateMovement();

        if (Input.GetKey(KeyCode.Space) && CheckIsGrounded())
        {
            CalculateJump();
        }
    }
    public bool CheckIsGrounded()
    {
        _distanceToTheGround = GetComponent<Collider>().bounds.extents.y + distanceToGroundOffset;
        //Debug.DrawRay(transform.position, -transform.up, Color.white, _distanceToTheGround);
        return Physics.BoxCast(transform.position, SphereRadius, -transform.up, transform.rotation,
            _distanceToTheGround, layermask, QueryTriggerInteraction.UseGlobal);
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + -transform.up * _distanceToTheGround);
        Gizmos.DrawWireCube(transform.position + -transform.up * _distanceToTheGround, SphereRadius);
    }
}
