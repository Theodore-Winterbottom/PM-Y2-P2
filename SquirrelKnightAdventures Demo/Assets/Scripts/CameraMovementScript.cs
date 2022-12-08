using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField] private new Rigidbody camera;
    [SerializeField] private float cameraSpeedMultiplier;

    private bool cameraIsPanning;
    private float PlayerPosX;
    private float positionDifference;
    private void FixedUpdate()
    {
        float cameraPosX = camera.transform.position.x;
        PlayerPosX = transform.position.x;
        positionDifference = PlayerPosX - cameraPosX;
        Debug.Log(positionDifference);
        if (Mathf.Abs(positionDifference) >= 18 && !cameraIsPanning)
        {
            cameraIsPanning = true;
            calculateMovePos();
        }
    }
    private void calculateMovePos()
    {
        for (int i = 0; Mathf.Abs(positionDifference > 0); i++)
        {

            Vector3 moveDirection = new Vector3(positionDifference, camera.transform.position.y, camera.transform.position.z);

            camera.velocity = (moveDirection * Mathf.Abs(positionDifference) * cameraSpeedMultiplier * Time.deltaTime);
            //camera.AddForce(moveDirection * Mathf.Abs(posDifference) * cameraSpeedMultiplier);
            if (Mathf.Abs(positionDifference) < 5)
            {
                StopCamera();
            }
        }
    }
        
    private void StopCamera()
    {
        camera.velocity = Vector3.zero;
        cameraIsPanning = false;
    }
}
