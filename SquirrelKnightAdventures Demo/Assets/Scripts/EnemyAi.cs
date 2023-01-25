using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder;
#region Main Script
public class EnemyAi : MonoBehaviour
{
    #region Variables

    [SerializeField] private int enemyType;

    [SerializeField] private NavMeshAgent nav_agent;

    [SerializeField] private Transform target_object;

    [SerializeField] private LayerMask player_layermask;

    [SerializeField] private LayerMask ground_layermask;

    [SerializeField] private GameObject projectile_prefab;

    [SerializeField] private Animator attack_animation;

    [SerializeField] private float timeBetweenAttacks;

    [SerializeField] private float walkPointRange;

    [SerializeField] private float timeBetweenShots;

    [SerializeField] private float nextshotTime;

    [SerializeField] private float sightRange;

    [SerializeField] private float attackRange;

    [SerializeField] private float rangeAttack;

    [SerializeField] private float speed;

    public bool playerInSightRange, playerInAttackRange, playerInRangeAttack, walkPointSet, settinglast, alreadyAttacked;

    private Vector3 walkPoint, moveDirection, currentPosition, _lastPosition;

    #endregion

    #region Script
    private void Awake()
    {
        //Finds enemys NavMeshAgent, Transform and lastpositon when the game starts
        _lastPosition = nav_agent.transform.position;

    }
    private void Update()
    {
        Debug.Log(player_layermask.value);
        Debug.Log(ground_layermask.value);
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
            if (moveDirection.x < 0)
            {

                
                
                flipEnemy();

            }
            else
            {
                
                
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
        RaycastHit hit;
        if (Physics.Raycast(walkPoint, -transform.up, out hit, 20f, ground_layermask))
        {
            Debug.Log(hit.collider.gameObject.name);
            walkPointSet = true;
        }
    }

    private void flipEnemy()
    {
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

    private void ChasePlayer()
    {
        //Chases the player as long as it is in range
        nav_agent.SetDestination(target_object.position);
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
    /*
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
    */
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
    public int ground_layer_value;
    public int player_layer_value;
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
    SerializedProperty projectile_prefab;
    SerializedProperty attack_animation;
    SerializedProperty timeBetweenAttacks;
    SerializedProperty walkPointRange;
    SerializedProperty timeBetweenShots;
    SerializedProperty nextshotTime;
    SerializedProperty sightRange;
    SerializedProperty attackRange;
    SerializedProperty rangeAttack;
    SerializedProperty speed;


    void OnEnable()
    {
        enemyType = serializedObject.FindProperty("enemyType");
        target_object = serializedObject.FindProperty("target_object");
        nav_agent = serializedObject.FindProperty("nav_agent");
        ground_layermask = serializedObject.FindProperty("ground_layermask");
        player_layermask = serializedObject.FindProperty("player_layermask");
        projectile_prefab = serializedObject.FindProperty("projectile_prefab");
        attack_animation = serializedObject.FindProperty("attack_animation");
        timeBetweenAttacks = serializedObject.FindProperty("timeBetweenAttacks");
        walkPointRange = serializedObject.FindProperty("walkPointRange");
        timeBetweenShots = serializedObject.FindProperty("timeBetweenShots");
        nextshotTime = serializedObject.FindProperty("nextshotTime");
        sightRange = serializedObject.FindProperty("sightRange");
        attackRange = serializedObject.FindProperty("attackRange");
        rangeAttack = serializedObject.FindProperty("rangeAttack");
        speed = serializedObject.FindProperty("speed");
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
                enemyType.intValue = 1;
                DisplayMeleeEnemyStats();
                break;

            case DisplayCategory.Ranged_Enemy:
                enemyType.intValue = 2;
                DisplayRangedEnemyStats();
                break;

            case DisplayCategory.Boss_Enemy:
                enemyType.intValue = 3;
                DisplayBossEnemyStats();
                break;

            case DisplayCategory.Flying_Enemy:
                enemyType.intValue = 4;
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
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Attack Animation");
        EditorGUILayout.ObjectField(attack_animation, GUIContent.none);
        

        
    }
    void DisplayRangedEnemyStats()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Ranged Enemey Type", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Projectile Prefab");
        EditorGUILayout.ObjectField(projectile_prefab, GUIContent.none);
        
    }
    void DisplayBossEnemyStats()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Boss Eemey Type", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Attack Animation");
        EditorGUILayout.ObjectField(attack_animation, GUIContent.none);
        

        
    }
    void DisplayFlyingEnemyStats()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Flying Eemey Type", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Projectile Prefab");
        EditorGUILayout.ObjectField(projectile_prefab, GUIContent.none);
        

        
    }

    void DisplayBasicStats()
    {
        EditorGUI.indentLevel = 0;

        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Basic Stats", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 1;

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Object Declaration", EditorStyles.boldLabel);
        

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Ai Target");
        EditorGUILayout.ObjectField(target_object, GUIContent.none);
        

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Ai NavAgent");
        EditorGUILayout.ObjectField(nav_agent, GUIContent.none);








        // before organized var

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Time Between Attacks");
        EditorGUILayout.Slider(timeBetweenAttacks, 0, 20, GUIContent.none);

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Walk Point Range");
        EditorGUILayout.Slider(walkPointRange, 0, 20, GUIContent.none);

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Time Between Shots");
        EditorGUILayout.Slider(timeBetweenShots, 0, 20, GUIContent.none);

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Next Shot Time");
        EditorGUILayout.Slider(nextshotTime, 0, 20, GUIContent.none);



        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Sight Range");
        EditorGUILayout.Slider(sightRange, 0, 20, GUIContent.none);

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Attack Range");
        EditorGUILayout.Slider(attackRange, 0, 20, GUIContent.none);

        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Range Attack");
        EditorGUILayout.Slider(rangeAttack, 0, 20, GUIContent.none);
        
        EditorGUILayout.Space(3f);
        EditorGUILayout.LabelField("Speed");
        EditorGUILayout.Slider(speed, 0, 20, GUIContent.none);
    }
}
#endregion
