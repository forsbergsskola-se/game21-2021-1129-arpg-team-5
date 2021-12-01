using Team5.Combat;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySearchFindDestroy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform playerPosition; 
    public float entityHealth;
    public float entityPatrolSpeed;
    public float entityPatrolDefaultSpeed= 3f;
    public float entityChaseSpeed = 5f;

    public LayerMask whatIsGround, whatIsPlayer;
    
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    // public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        playerPosition = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //entityHealth = this.GetOrAddComponent<Entity>().Health;
        entityPatrolSpeed = GetComponent<NavMeshAgent>().speed;



        // needs to grab  health from health.cs -> corpse doesn't move if 0

        // grab move speed value from NavMeshAgent augment it based on (1) patrol and (2) chase and (3) death
    }

    private void Update()
    {
        Debug.Log(entityHealth);
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            entityPatrolSpeed = entityPatrolDefaultSpeed;
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            entityPatrolSpeed = entityChaseSpeed;
        }

        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        
        
        if (entityHealth <= 0)
        {
            CorpseStay();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        
        Debug.Log("Am Patroling");

    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerPosition.position);
        Debug.Log("Am Chasing");
    }

    private void AttackPlayer()
    {
        //This makes sure enemy doesn't move, but looks at player while attacking 
        agent.SetDestination(transform.position);

        transform.LookAt(playerPosition);

        if (!alreadyAttacked)
        {
            // Need to add attack here from Entity
            
            // Add projectile or other attack method
            
            
            /*Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);*/
            
            
            // End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
        
        Debug.Log("Am Attacking");

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        
        Debug.Log("Can attack again");

    }

    private void CorpseStay()
    {
        entityPatrolSpeed = 0;
    }

    
    private void DestroyEnemy()
    {
        Destroy(gameObject);
        
        Debug.Log("Goodbye world");

    }

    private void ReturnToStartPos()
    {
        
    }

    private void CancelChase()
    {
        
    }

}
