using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minimumDistance;
    public bool playerInRange;

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            
            Vector3 movePosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            Vector3 movePos = new Vector3(movePosition.x, transform.position.y, movePosition.z);
            transform.position = movePos;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
