using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{

    public float speed;
    public List<Transform> patrolPoints;
    public FollowEnemy followEnemy;
    public float waitTime;
    int currentPointIndex;

    bool once;

    private void Update()
    {
        if (transform.position != patrolPoints[currentPointIndex].position && followEnemy.playerInRange == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        }else
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
        }else
        {
            currentPointIndex = 0;
        }
        once = false;
    }
}
