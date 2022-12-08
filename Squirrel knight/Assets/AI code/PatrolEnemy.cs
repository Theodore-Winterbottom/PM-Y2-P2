using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{

    public float speed;
    public List<Transform> patrolPoints;
    public FollowEnemy followEnemy;
    public float waitTime;
    int currentPointIndex;
    private NavMeshAgent navMeshAgent;
    public Transform target;

    bool once;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if(followEnemy.playerInRange ==false)
        {
            Vector3 movePoint = new Vector3(patrolPoints[currentPointIndex].position.x, transform.position.y, patrolPoints[currentPointIndex].position.z);
            if (transform.position != movePoint && followEnemy.playerInRange == false)
            {
                //transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
                navMeshAgent.destination = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

            }
            else
            {
                if (once == false)
                {
                    once = true;
                    StartCoroutine(Wait());
                }

            }
        }
        

        if (followEnemy.playerInRange == true)
        {
            //Vector3 movePosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //Vector3 movePos = new Vector3(movePosition.x, transform.position.y, movePosition.z);
            //transform.position = movePos;
            navMeshAgent.destination = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if(currentPointIndex + 1 < patrolPoints.Count)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        
        once = false;
    }
}
