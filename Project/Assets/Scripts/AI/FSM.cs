using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FSM : MonoBehaviour
{

    public enum FSMStates{ // States the AI can be in
        None,
        Wander,
        Chase,
        Attack,
        Idle,
        Dead,
    }

    public FSMStates currentState; // Current state

    public float movementSpeed = 15.0f;  // Speed the enemy AI moves to a location (or towards the player)
    public float rotationSpeed = 10.0f;  // Speed the enemy AI rotates
    public float attackSpeed = 5.0f;  // Speed/interval between each attack

    public Transform playerPosition; // Players position the AI will chase when in range
    protected Vector3 destination; // Destination location when wandering
    public float wanderRadius = 20.0f; // Radius the AI will pick which is a random spot within this radius to wander towards

    protected bool Dead; // Determines if the AI is dead - intiate dead state

    public int health = 100; // Health value of the enemy

    //attacking section here
    public float attackingRange = 20f;
    public float earshotRange = 20f;

    public float visibleRange = 30f;
    public float visibleFOV = 45f;


    //
    private Vector3 temp;

    //Navigation
    private int currentWayPoint;
    private NavMeshAgent nav;
    public GameObject[] waypointList;

    //animation
    private Animator animator;
    


    // Start is called before the first frame update
    void Start()
    {
        //gets animations component
        animator = GetComponent<Animator>();


        //setdefault point
        currentWayPoint = 0;

        //set
        nav = GetComponent<NavMeshAgent>();


        currentState = FSMStates.Wander; // Start wandering when first created

        // Get object/tag of the player to be used in determining the players position to the AI
       // GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
       // playerPosition = playerObject.transform;
        if(!playerPosition)
        {
            print("ERROR: Player object not found - assign player object with 'Player' tag");
        }

        Dead = false; // AI is not dead - therefore false

    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState) // Depending on the current state - call respective function to update current state
        {
            case FSMStates.Wander: UpdateWanderState(); break;
            case FSMStates.Attack: UpdateAttackState(); break;
            case FSMStates.Chase: UpdateChaseState(); break;
            case FSMStates.Idle: UpdateIdleState(); break;
            case FSMStates.Dead: UpdateDeadState(); break;

        }

        if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }

       

    }

    // Wandering State

    protected void UpdateWanderState()
    {
        //sets the animation
        animator.SetFloat("Velocity", nav.velocity.magnitude/3);

        // Moves towards waypoint
        nav.SetDestination(waypointList[currentWayPoint].transform.position);

     
        //go from 1 point to another
        if (nav.remainingDistance < 0.5f)
        {
            currentWayPoint += 1;
        }
        if (currentWayPoint > waypointList.Length-1)
        {
            currentWayPoint = 0;
        }

        //transition out if user earshot or within seeing range.
        if (Vector3.Distance(playerPosition.position, transform.position) < earshotRange  || ((Vector3.Angle(transform.forward, playerPosition.transform.position - transform.position) < visibleFOV && Vector3.Distance(playerPosition.position, transform.position) < visibleRange))) {
            currentState = FSMStates.Chase;           

        }
       

    }

    // Attack State
    protected void UpdateChaseState()
    {
       
        //sets the animation
        animator.SetFloat("Velocity", nav.velocity.magnitude / 3);

        //increases speed to chase the player
        nav.speed = 10;

        // Moves towards player
        nav.SetDestination(playerPosition.position);

        //Once it has acquired you it doesn't simply forget and thus it only transitions in and out of attacking range.

        if (Vector3.Distance(playerPosition.position, transform.position) < attackingRange) {
            currentState = FSMStates.Attack;
        }
    }

    // Attack State
    protected void UpdateAttackState()
    {
        //sets the animation
        animator.SetBool("Attacking", true);

        //make navigation idle
        nav.SetDestination(transform.position);

        //if outside attack range
        if (Vector3.Distance(playerPosition.position, transform.position) > attackingRange)
        {
            currentState = FSMStates.Chase;
            animator.SetBool("Attacking", false);
        }

    }

    // Idle State

    protected void UpdateIdleState()
    {
        animator.speed = 0;

    }

    // Dead State
    protected void UpdateDeadState()
    {
        //play dead animation
       
    }

    void OnDrawGizmosSelected()
    {
        temp = transform.position;
        temp.y += 3.5f;
      


        float totalFOV = 80.0f;
        float rayRange = 20.0f;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(temp, leftRayDirection * rayRange);
        Gizmos.DrawRay(temp, rightRayDirection * rayRange);
    }

    public void giveDamage(int damage) // If hit - apply damage
    {
        health -= damage;

    }

}
