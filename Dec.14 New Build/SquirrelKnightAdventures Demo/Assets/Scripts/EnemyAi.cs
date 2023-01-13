using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyAi : MonoBehaviour
{

    [Header("Objects attach to player")]

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform player;

    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private GameObject projectile;

    [SerializeField] private Animator anim;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private HealthScript healthScript;

    [SerializeField]
    private int health;

    [Header("Patroling")]

    [SerializeField] private Vector3 walkPoint;

    [SerializeField] public bool walkPointSet;

    [SerializeField] private float walkPointRange;

    [SerializeField] private Vector3 _lastPosition;

    [SerializeField] private float velocity;

    [Header("Attacking")]

    [SerializeField] private float timeBetweenAttacks;

    [SerializeField] private float timeBetweenShots;

    [SerializeField] private float nextshotTime;

    [SerializeField] private bool alreadyAttacked;



    [Header("States")]

    [SerializeField] private float sightRange, attackRange, rangeAttack;
    
    [SerializeField] public bool playerInSightRange, playerInAttackRange, playerInRangeAttack;

    [SerializeField] private float speed;
    public Vector3 lastpoint;
    public Vector3 currentPosition;
    public bool settinglast;
    public float moveDirectionF;
    public float LastPositionF;
    public Vector3 moveDirection;
    private void Awake()
    {
        //Finds enemys NavMeshAgent, Transform and lastpositon when the game starts
        _lastPosition = agent.transform.position;
        
        
    }
    private void Update()
    {
        if (!settinglast)
        {
            settinglast = true;
            StartCoroutine(setLastPosition());
        }
        
        currentPosition = agent.transform.position;
    }
    private void FixedUpdate()
    {
        //Check for sight and attack range
        

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInRangeAttack = Physics.CheckSphere(transform.position, rangeAttack, whatIsPlayer);

        //Patroling the area if not attacking or chaseing the player
        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }

        //chases player if it is in the dection field
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        //Attacks player if it is in range
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            anim.SetBool("enemyAttacking", true);
        }
        else if (!playerInAttackRange)
        {
            anim.SetBool("enemyAttacking", false);
        }

        //Attacks with range attack if in range
        if (playerInSightRange && playerInRangeAttack)
        {
            RangeAttack();
        }


        float playerdistance = (transform.position - player.position).magnitude;

        moveDirection = currentPosition - _lastPosition;
        
        if (Mathf.Sign(moveDirection.x) == agent.transform.localScale.x && playerdistance != 1f)
        {
            if(moveDirection.x < 0)
            {
                
                moveDirectionF = Mathf.Sign(moveDirection.x);
                LastPositionF = Mathf.Sign(_lastPosition.x);
                lastpoint = _lastPosition;
                flipEnemy();

            }
            else
            {
                moveDirectionF = Mathf.Sign(moveDirection.x);
                LastPositionF = Mathf.Sign(_lastPosition.x);
                lastpoint = _lastPosition;
                flipEnemy();
                
            }
        }
    }

    private void Patroling()
    {
        //If no walkpoint finds walk point
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        //moves to the walkpoint
        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y);

        //sets walk point with the random range it obtained
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void flipEnemy()
    {
        Vector3 currentScale = agent.transform.localScale;

        currentScale.x *= -1;

        agent.transform.localScale = currentScale;
    }
    private IEnumerator setLastPosition()
    {
        
        yield return new WaitForSeconds(1);
        _lastPosition = agent.transform.position;
        settinglast = false;
    }

    private void ChasePlayer()
    {
        //Chases the player as long as it is in range
        agent.SetDestination(player.position);
    }

    public void AttackPlayer()
    {
        //Moves towards the player to perform a attack
        agent.SetDestination(transform.position);

        //transform.LookAt(player);

        //resets the enemys attack
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public void RangeAttack()
    {
        //Moves towards the player to perform a range attack
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        //Instanitates the projectile
        if (Time.time > nextshotTime)
        {
           
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextshotTime = Time.time + timeBetweenShots;
        }

        //Moves in range attack range and moves backworsd when the player apporchs
        if (playerInRangeAttack == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        //resets the enemys attack
        if (!alreadyAttacked)
        {
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        //Resets atack after the enemy attacks it it dsont attack contintly
        alreadyAttacked = false;
    }

    public void Death()
    {
        //Kills the emeny when health reachs zero
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {

        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && anim.GetBool("enemyAttacking"))
        {
            healthScript.TakeDamage(20);
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);

    }

}
