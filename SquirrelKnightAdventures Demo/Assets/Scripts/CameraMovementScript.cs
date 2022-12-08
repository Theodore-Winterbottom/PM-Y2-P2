using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField] private new Rigidbody camera;
    [SerializeField] private float cameraSpeedMultiplier;

    private bool cameraIsPanning;
    private bool cameraIsUnlocked;
    private Collider collidedGameobject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "left_barrier" || other.name == "right_barrier" || other.name == "middle_barrier")
        {
            cameraIsUnlocked = true;
            collidedGameobject = other;
        }
    }
    private void FixedUpdate()
    {
        if(cameraIsUnlocked)
        {
            calculateMovePos(collidedGameobject);
        }
    }
    private void calculateMovePos(Collider other)
    {
        while (other.name != "middle_barrier" && !cameraIsPanning)
        {

            float barrierPoxX = other.transform.position.x;

            float cameraPosX = camera.transform.position.x;

            float posDifference = barrierPoxX - cameraPosX;
            Debug.Log(posDifference);

            Vector3 moveDirection = new Vector3(posDifference, camera.transform.position.y, camera.transform.position.z);

            camera.AddForce(moveDirection * Mathf.Abs(posDifference) * cameraSpeedMultiplier);
            while (posDifference < .1)
            {
                cameraIsPanning = true;
                calculateMovePos(collidedGameobject);
            }
        } 

        if(other.name == "middle_barrier")
        {
            camera.velocity = Vector3.zero;
            cameraIsPanning = false;
            cameraIsUnlocked = false;
        }
    }
}
