using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedInEnemyAI : MonoBehaviour
{
    public Transform player;
    public Vector3 playerLastKnownPosition;
    public Vector3 playerCurrentPosition;
    public bool canSeePlayer;
    public FieldOfView fieldOfView;
    public Rigidbody rigidBody;
    public float moveSpeed;
    public float turnSpeed;
    public float mininumDistanceFromPlayer = 4f;
    public float mininumDistanceToInvestigate = 5f;
    public float cullingRangeFromPlayer = 20f;
    public int rotationSpeed;
    public UnityEngine.AI.NavMeshAgent agent;
    public Animator animator;
    private float currentPatrolDistance;
    private bool movingStage1;
    private bool movingStage2;
    private bool chasing;
    public Quaternion startRotation;
    public bool hasBeenAlerted;
    public bool isInvestigating = false;

    //Handles enemy health
    public EnemyHealth enemyHealth;
    //Lines down here are for path traversal
    public Transform path;
    public  List<Transform> myNodes;
    Vector3 nodePosition;
    public Transform myCurrentNode;
    public int index;
    //keep track of current nodes

    void Awake()
    {
        fieldOfView = GetComponent<FieldOfView>();
        startRotation = transform.rotation;
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator.SetBool("IsMoving", true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myCurrentNode = GameObject.FindGameObjectWithTag("Enemy Spawner").GetComponent<Transform>();
    }

    void Update()
    {
        playerCurrentPosition = player.position;
        Vector3 distanceFromPlayer = player.position - transform.position;
        canSeePlayer = fieldOfView.canSeeTarget;
        hasBeenAlerted = EventBus.Instance.inAlertPhase;
        float distanceToPlayer = Vector3.Distance(player.position,transform.position);
        if(EventBus.Instance.enemyCanMove == false || enemyHealth.isDead == true || distanceToPlayer >= cullingRangeFromPlayer)
        {
            return;
        }

        if (isInvestigating)
        {
            return;
        }


        //If the player has been spotted, chase them
        if(hasBeenAlerted == true)
        {
            isInvestigating = false;
            playerLastKnownPosition = EventBus.Instance.playerLastKnownPosition;
            animator.SetBool("IsAttacking", true);
            if(canSeePlayer == true)
            {
                
                FollowPlayer(playerCurrentPosition, playerLastKnownPosition, canSeePlayer);
            }
            else
            {
                FollowPlayer(playerCurrentPosition, playerLastKnownPosition, canSeePlayer);;
            }

        }

        //Otherwise, return to their position
        else
        {
            animator.SetBool("IsAttacking", false);
            if(!agent.pathPending)
            {
                rigidBody.velocity = Vector3.zero;
                
            }
            // Using distance included y which varied when enemies collided.
            if(myCurrentNode.position.x == transform.position.x && myCurrentNode.position.z == transform.position.z)
            {
                transform.rotation = startRotation;
                agent.ResetPath();
            }

            Vector3 distance = transform.position - myCurrentNode.position;
            distance.y = 0;

            if((distance).magnitude > 0.2f)
            {
                FollowNode(myCurrentNode.position);
            }
        }
    }

    void FollowPlayer(Vector3 playerPosition, Vector3 lastKnownPosition, bool canSeePlayer)
    {
        
        if(canSeePlayer == true)
        {
            Vector3 distanceFromPlayer = playerPosition - transform.position;
            float distance = Vector3.Distance(playerPosition,transform.position);
            distanceFromPlayer.Normalize();
            //Debug.Log("Here's the Distance: " + distance);
            if(distance <= mininumDistanceFromPlayer)
            {
                rigidBody.velocity = distanceFromPlayer * 0;
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", true);
                LookAtPlayer(player.position);
                agent.SetDestination(rigidBody.position);
                //In the if statement, cancel the path since we don't want the enemy to move.
                //Set the destination to player position instead. 
            }
            else
            {
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                agent.SetDestination(playerCurrentPosition);
                //Might need to get rid of rotation since nav mesh should be able to handle it
                if (distanceFromPlayer * moveSpeed != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(distanceFromPlayer * moveSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }          
        }
        //If the enemy cannot see the player, it will go to their last known location
        else
        {
            Vector3 distanceFromPlayer = playerLastKnownPosition - transform.position;
            float distance = Vector3.Distance(playerLastKnownPosition,transform.position);
            distanceFromPlayer.Normalize();
            //Debug.Log("Here's the Distance: " + distance);       

            if(distance <= mininumDistanceFromPlayer)
            {
                rigidBody.velocity = distanceFromPlayer * 0;
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", false);
                LookAtPlayer(player.position);
                agent.SetDestination(rigidBody.position);
                //In the if statement, cancel the path since we don't want the enemy to move.
                //Set the destination to player position instead. 
            }
            else
            {
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                agent.SetDestination(playerLastKnownPosition);   
                if (distanceFromPlayer * moveSpeed != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(distanceFromPlayer * moveSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }            
            }
                        
        }

    }
    public void LookAtPlayer(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void FollowNode(Vector3 nodePosition)
    {
        animator.SetBool("IsMoving", true);
        animator.SetBool("IsAttacking", false);
        Vector3 distanceFromNode = nodePosition - transform.position;
        float distance = Vector3.Distance(nodePosition, transform.position);
        distanceFromNode.Normalize();
        agent.SetDestination(nodePosition);
        if (distanceFromNode * moveSpeed != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(distanceFromNode * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
        }
    }

}
