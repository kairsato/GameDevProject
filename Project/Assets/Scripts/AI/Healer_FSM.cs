using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Healer_FSM : MonoBehaviour
{

    public enum FSMStates{ // States the AI can be in
        None,
        Follow,
        Heal,
        Defend,
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

    public int health = 100; // Health value of the healer

    //attacking section here
    public float idleRange = 3f;
    public float healRange = 6f;


    //
    private Vector3 temp;

    //Navigation
    private int currentWayPoint;
    private NavMeshAgent nav;
    public GameObject[] waypointList;

    //animation
    private Animator animator;

    //timediff
    private float setTime;
    private float tempTime;


    // Start is called before the first frame update
    void Start()
    {
        //gets animations component
        animator = GetComponent<Animator>();


        //setdefault point
        currentWayPoint = 0;

        //set
        nav = GetComponent<NavMeshAgent>();

        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerPosition = objPlayer.transform;
        currentState = FSMStates.Follow; // Start wandering when first created

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
            case FSMStates.Follow: UpdateFollowState(); break;
            case FSMStates.Heal: UpdateHealState(); break;
            case FSMStates.Defend: UpdateDefendState(); break;
            case FSMStates.Idle: UpdateIdleState(); break;
            case FSMStates.Dead: UpdateDeadState(); break;

        }

        if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }

       

    }

    // Wandering State

    protected void UpdateFollowState()
    {

        //sets the animation
        animator.SetFloat("movVelocity", nav.velocity.magnitude/3);

        // Moves towards player
        nav.SetDestination(playerPosition.transform.position);

     
       
        //transition out if user earshot or within seeing range.
        if (Vector3.Distance(playerPosition.position, transform.position) < idleRange  ) {
            currentState = FSMStates.Idle;           

        }else if (Vector3.Distance(playerPosition.position, transform.position) < idleRange &&  health < 100 )        {
            currentState = FSMStates.Heal;
            setTime = Time.time;

        }

       



    }

    // Attack State
    protected void UpdateHealState()
    {

        // stays still
        nav.SetDestination(transform.position);

        //sets the animation
        animator.SetBool("Healing", true);



        //only heal if the player exit the range for 3 seconds and health is below full
        if (health < 100 && (setTime - Time.time) > 3f && Vector3.Distance(playerPosition.position, transform.position) < healRange)
        {

            Debug.Log("Healed Player");
            currentState = FSMStates.Idle;
        }




        //move out if moving
        if (Vector3.Distance(playerPosition.position, transform.position) > healRange) {
            currentState = FSMStates.Follow;           
        }
    }

    // Attack State
    protected void UpdateDefendState()
    {
       //in development
       

    }

    // Idle State

    protected void UpdateIdleState()
    {
        animator.SetFloat("movVelocity", 0f);
        nav.SetDestination(transform.position);


        if (Vector3.Distance(playerPosition.position, transform.position) >= idleRange)
        {
            currentState = FSMStates.Follow;           
        }else if (Vector3.Distance(playerPosition.position, transform.position) < idleRange && health < 100)
        {
            currentState = FSMStates.Heal;
            setTime = Time.time;

        }
    }

    // Dead State
    protected void UpdateDeadState()
    {
        // Stop moving
        nav.SetDestination(transform.position);
        //play dead animation
        animator.SetBool("isDead", true);
        // Delete after animated has played
        Destroy(gameObject, 5f);
    }

    void OnDrawGizmosSelected()
    {
       


    }

    public void giveDamage(int damage) // If hit - apply damage
    {
        health -= damage;

    }

}
