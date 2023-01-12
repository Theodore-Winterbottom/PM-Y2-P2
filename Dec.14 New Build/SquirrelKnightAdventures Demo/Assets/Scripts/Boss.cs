using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Boss : MonoBehaviour
{

    [Header("Objects attach to Boss")]
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private HealthScript healthScript;

    [SerializeField]
    private Rigidbody rb;

    [Header("Attacking")]

    [SerializeField]
    private float timeBetweenAttacks;

    [Header("Stats")]

    [SerializeField]
    private int health = 500;

    [Header("Position")]

    [SerializeField]
    private Vector3 lastpoint;

    [SerializeField]
    private Vector3 currentPosition;

    [SerializeField]
    private bool settinglast;

    [SerializeField]
    private float moveDirectionF;

    [SerializeField]
    private float LastPositionF;

    [SerializeField]
    private Vector3 moveDirection;

    [SerializeField]
    private Vector3 _lastPosition;

    [Header("States")]

    [SerializeField]
    private bool alreadyAttacked;

    [SerializeField]
    private bool isInvulnerable = false;

    [SerializeField]
    private float sightRange, attackRange, rangeAttack;

    [SerializeField]
    public bool playerInSightRange, playerInAttackRange, playerInRangeAttack;

   

    private void Awake()
    {
        //Finds enemys NavMeshAgent, Transform and lastpositon when the game starts
        _lastPosition = agent.transform.position;
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

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

        //chases player if it is in the dection field
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        //Attacks player if it is in range
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            anim.SetBool("bossAttack", true);
        }
        else if (!playerInAttackRange)
        {
            anim.SetBool("bossAttack", false);
        }


        float playerdistance = (transform.position - player.position).magnitude;

        moveDirection = currentPosition - _lastPosition;

        if (Mathf.Sign(moveDirection.x) == agent.transform.localScale.x && playerdistance != 1f)
        {
            if (moveDirection.x < 0)
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

    private void ResetAttack()
    {
        //Resets atack after the enemy attacks it it dsont attack contintly
        alreadyAttacked = false;
    }

    private void ChasePlayer()
    {
        //Chases the player as long as it is in range
        agent.SetDestination(player.position);
    }

    private IEnumerator setLastPosition()
    {

        yield return new WaitForSeconds(1);
        _lastPosition = agent.transform.position;
        settinglast = false;
    }
    
    private void flipEnemy()
    {
        Vector3 currentScale = agent.transform.localScale;

        currentScale.x *= -1;

        agent.transform.localScale = currentScale;
    }


    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (health <= 200)
        {

        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && anim.GetBool("enemyAttacking"))
        {
            healthScript.TakeDamage(20);
        }
    }

}