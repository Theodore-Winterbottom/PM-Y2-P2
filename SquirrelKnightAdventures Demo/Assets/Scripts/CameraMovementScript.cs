using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField] private new Rigidbody camera;
    [SerializeField] private float cameraMoveSpeed;

    private bool cameraIsPanning;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "left_barrier" || other.name == "right_barrier" || other.name == "middle_barrier")
        {
            calculateMovePos(other);
        }
    }
    private void calculateMovePos(Collider other)
    {
        if (other.name != "middle_barrier" && !cameraIsPanning)
        {
            cameraIsPanning = true;

            float barrierPoxX = other.transform.position.x;

            float cameraPosX = camera.transform.position.x;

            float posDifference = barrierPoxX - cameraPosX;

            Vector3 moveDirection = new Vector3(posDifference, camera.transform.position.y, camera.transform.position.z);

            camera.AddForce(moveDirection * cameraMoveSpeed);
        } 

        if(other.name == "middle_barrier")
        {
            camera.velocity = Vector3.zero;
            cameraIsPanning = false;
        }
    }
}
