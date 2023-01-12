using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
#region Main Script
public class EnemyAi : MonoBehaviour
{
    #region Variables

    [SerializeField] private bool enemyType;

    [SerializeField] private NavMeshAgent nav_agent;

    [SerializeField] private Vector3 walkPoint;

    [SerializeField] private Transform target_object;

    [SerializeField] private LayerMask player_layermask;

    [SerializeField] private LayerMask ground_layermask;

    [SerializeField] private GameObject projectile_prefab;

    [SerializeField] private Animator attack_animation;

    [SerializeField] private float walkPointRange;

    [SerializeField] private Vector3 _lastPosition;

    [SerializeField] private float velocity;

    [SerializeField] private float timeBetweenAttacks;

    [SerializeField] private float timeBetweenShots;

    [SerializeField] private float nextshotTime;

    [SerializeField] private bool alreadyAttacked;

    [SerializeField] private float sightRange, attackRange, rangeAttack;
    
    [SerializeField] public bool playerInSightRange, playerInAttackRange, playerInRangeAttack;

    [SerializeField] private float speed;
    
    [SerializeField] private Vector3 lastpoint;
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private bool settinglast;
    [SerializeField] private float moveDirectionF;
    [SerializeField] private float LastPositionF;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private bool walkPointSet;
    #endregion
    #region Script
    [SerializeField]private void Awake()
    {
        nav_agent = GetComponent<NavMeshAgent>();
        //Finds enemys NavMeshAgent, Transform and lastpositon when the game starts
        _lastPosition = nav_agent.transform.position;
        
    }
    private void Update()
    {
        
        if (!settinglast)
        {
            settinglast = true;
            StartCoroutine(setLastPosition());
        }
        
        currentPosition = nav_agent.transform.position;
    }
    private void FixedUpdate()
    {
        //Check for sight and attack range

        

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player_layermask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player_layermask);
        playerInRangeAttack = Physics.CheckSphere(transform.position, rangeAttack, player_layermask);

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
            attack_animation.SetBool("enemyAttacking", true);
        }
        else if (!playerInAttackRange)
        {
            attack_animation.SetBool("enemyAttacking", false);
        }

        //Attacks with range attack if in range
        if (playerInSightRange && playerInRangeAttack)
        {
            RangeAttack();
        }


        float playerdistance = (transform.position - target_object.position).magnitude;

        moveDirection = currentPosition - _lastPosition;
        
        if (Mathf.Sign(moveDirection.x) == nav_agent.transform.localScale.x && playerdistance != 1f)
        {
            flipEnemy();
        }
    }
    private void ChasePlayer()
    {
        Debug.Log("chasing");
        //Chases the player as long as it is in range
        nav_agent.SetDestination(target_object.position);
    }
    private void Patroling()
    {
        //If no walkpoint finds walk point
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        //moves to the walkpoint
        if (walkPointSet)
        {
            nav_agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
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
        if (Physics.Raycast(walkPoint, -transform.up, 2f, ground_layermask))
        {
            walkPointSet = true;
        }
    }

    private void flipEnemy()
    {
        Debug.Log("flipped");
        Vector3 currentScale = nav_agent.transform.localScale;

        currentScale.x *= -1;

        nav_agent.transform.localScale = currentScale;
    }
    private IEnumerator setLastPosition()
    {
        
        yield return new WaitForSeconds(1);
        _lastPosition = nav_agent.transform.position;
        settinglast = false;
    }

    

    public void AttackPlayer()
    {
        //Moves towards the player to perform a attack
        nav_agent.SetDestination(transform.position);

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
        nav_agent.SetDestination(transform.position);

        transform.LookAt(target_object);

        //Instanitates the projectile
        if (Time.time > nextshotTime)
        {
            Instantiate(projectile_prefab, transform.position, Quaternion.identity);
            nextshotTime = Time.time + timeBetweenShots;
        }

        //Moves in range attack range and moves backworsd when the player apporchs
        if (playerInRangeAttack == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target_object.position, -speed * Time.deltaTime);
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
#endregion
#endregion

#region Editor Region
[CustomEditor(typeof(EnemyAi))]
public class EnemyEditor : Editor
{
    public enum DisplayCategory
    {
        Melee_Enemy, Ranged_Enemy, Boss_Enemy, Flying_Enemy
    }

    public DisplayCategory categoryToDisplay;

    SerializedProperty enemyType;
    SerializedProperty target_object;
    SerializedProperty nav_agent;

    SerializedProperty ground_layermask;
    SerializedProperty player_layermask;

    SerializedProperty projectile;

    SerializedProperty anim;
    SerializedProperty rb;
    SerializedProperty walkPoint;
    void OnEnable()
    {
        enemyType = serializedObject.FindProperty("enemyType");
        target_object = serializedObject.FindProperty("target_object");
        nav_agent = serializedObject.FindProperty("nav_agent");
        ground_layermask = serializedObject.FindProperty("ground_layermask");
        player_layermask = serializedObject.FindProperty("player_layermask");
        enemyType = serializedObject.FindProperty("enemyType");

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Enemy AI Script", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 0;

        EditorGUILayout.Space(5f);
        categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Enemy Picker", categoryToDisplay);
        EditorGUI.indentLevel = 0;

        switch (categoryToDisplay)
        {
            case DisplayCategory.Melee_Enemy:
                DisplayMeleeEnemyStats();
                break;

            case DisplayCategory.Ranged_Enemy:
                DisplayRangedEnemyStats();
                break;

            case DisplayCategory.Boss_Enemy:
                DisplayBossEnemyStats();
                break;

            case DisplayCategory.Flying_Enemy:
                DisplayFlyingEnemyStats();
                break;
        }

        DisplayBasicStats();

        serializedObject.ApplyModifiedProperties();
    }
    
    void DisplayMeleeEnemyStats()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Melee Enemey Type", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 0;

        
    }
    void DisplayRangedEnemyStats()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Ranged Enemey Type", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 0;
    }
    void DisplayBossEnemyStats()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Boss Eemey Type", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 0;
    }
    void DisplayFlyingEnemyStats()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Flying Eemey Type", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 0;
    }

    void DisplayBasicStats()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Basic Stats", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(2f);
        EditorGUILayout.LabelField("Object Declaration");
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(5f);
        EditorGUILayout.ObjectField(target_object, GUIContent.none);
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(1f);
        EditorGUILayout.ObjectField(nav_agent, GUIContent.none);
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(1f);
        ground_layermask.intValue = EditorGUILayout.LayerField("Layer for Objects:", 100);
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(1f);
        //EditorGUILayout.LayerField("Layer for Objects:", 100);
        EditorGUI.indentLevel = 1;
    }
}
#endregion
