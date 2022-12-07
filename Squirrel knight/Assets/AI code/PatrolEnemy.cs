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

    bool once;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        Vector3 movePoint = new Vector3(patrolPoints[currentPointIndex].position.x, transform.position.y, patrolPoints[currentPointIndex].position.z);
        if (transform.position != movePoint && followEnemy.playerInRange == false)
        {
            //transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            navMeshAgent.destination = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        }
        else
        {
            if(once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
            
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
