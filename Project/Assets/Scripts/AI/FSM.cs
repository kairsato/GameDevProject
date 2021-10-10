using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FSM : MonoBehaviour
{

    public enum FSMStates
    { // States the AI can be in
        None,
        Wander,
        Chase,
        Attack,
        Idle,
        Dead,
    }


    public FSMStates currentState; // Current state

    public float movementSpeed = 15.0f;  // Speed the enemy AI moves to a location (or towards the player)

    public float rotationSpeed = 1.0f;  // Speed the enemy AI rotates

    public float attackSpeed = 5.0f;  // Speed/interval between each attack

    public float arrowLifeTime = 5f;

    public Transform playerPosition; // Players position the AI will chase when in range
    protected Vector3 destination; // Destination location when wandering
    public float wanderRadius = 20.0f; // Radius the AI will pick which is a random spot within this radius to wander towards

    protected bool Dead; // Determines if the AI is dead - intiate dead state

    public int health = 100; // Health value of the enemy
    public int goldAmount = 50;


    //attacking section here
    public float attackingRange = 5f;

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
    private bool isDeadconfirmed = false; 

    //timediff
    private float setTime;
    private float tempTime;

    private GameObject objPlayer;

    public float attackDelay; // Delay between the attacking animation starting and when a hit is registered on the player
    public float damage; // damage which can be dealt to player



    public string enemyType;




    public float attackAngle = 10;
    private bool aniRan;

    public float animationDamageTime;

    public float timeElapsed = 0;



    // Start is called before the first frame update
    void Start()
    {
        // theanimation = GetComponent<Animator>();

        timeElapsed = 0;

        //gets animations component
        animator = GetComponent<Animator>();


        //setdefault point
        currentWayPoint = 0;

        //set
        nav = GetComponent<NavMeshAgent>();



        objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerPosition = objPlayer.transform;
        currentState = FSMStates.Wander; // Start wandering when first created

        // Get object/tag of the player to be used in determining the players position to the AI
        // GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        // playerPosition = playerObject.transform;
        if (!playerPosition)
        {
            print("ERROR: Player object not found - assign player object with 'Player' tag");
        }

        Dead = false; // AI is not dead - therefore false

        if (waypointList.Length != 0)
        {
            waypointList[0] = GameObject.Find("wayPoint (0)");
            waypointList[1] = GameObject.Find("wayPoint (1)");
            waypointList[2] = GameObject.Find("wayPoint (2)");
        }
        else {

            currentState = FSMStates.Chase;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

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
        animator.SetFloat("Velocity", nav.velocity.magnitude / 3);

        // Moves towards waypoint
        nav.SetDestination(waypointList[currentWayPoint].transform.position);


        //go from 1 point to another
        if (nav.remainingDistance < 0.5f)
        {
            currentWayPoint += 1;
        }
        if (currentWayPoint > waypointList.Length - 1)
        {
            currentWayPoint = 0;
        }

        //transition out if user earshot or within seeing range.
        if (Vector3.Distance(playerPosition.position, transform.position) < earshotRange || ((Vector3.Angle(transform.forward, playerPosition.transform.position - transform.position) < visibleFOV && Vector3.Distance(playerPosition.position, transform.position) < visibleRange)))
        {
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

        if (Vector3.Distance(playerPosition.position, transform.position) < attackingRange)
        {
            currentState = FSMStates.Attack;
            setTime = Time.time;

            
        }
    }

    // Attack State
    protected void UpdateAttackState()
    {
        if (enemyType == "melee")
        {
            //sets the animation
            animator.SetBool("Attacking", true);
            //make navigation idle
            nav.SetDestination(transform.position);
        }




        Quaternion turretRotation = Quaternion.LookRotation(playerPosition.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * rotationSpeed);


        animator = GetComponent<Animator>();
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1);

        if (enemyType == "melee") {
            if (!aniRan && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) <= 0.35) {
                aniRan = true;
            }

            if ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) > 0.35 && aniRan)
            {
                //if the enemy isn't facing the play don't apply damage

               
                if (Vector3.Dot(((playerPosition.transform.position - transform.position).normalized), transform.forward) > animationDamageTime) {
                    objPlayer.SendMessage("giveDamage", damage);
                }






                aniRan = false;
            }






            //When it attacks. It continues attacking with +3 range until 2 seconds of when it was in the state was run. 
            //all this does is give the chance for the enemy to complete an attack and miss the player instead going back into the chasing state.
            if ((Time.time - setTime - 2) < 0)
            {
                tempTime = 3;
            }
            else
            {
                tempTime = 0;
            }


        }

        if (enemyType == "ranged")
        {

            if (timeElapsed > attackSpeed)
            {

             //sets the animation
            animator.SetBool("Attacking", true);
            //make navigation idle
            nav.SetDestination(transform.position);


                // Create an arrow at fire it towards the player

                Vector3 ArrowPos = new Vector3(0f, 2f, 0f);
                var arrow = (GameObject)Instantiate(GameObject.Find("EnemyArrow"), transform.position + (transform.forward * 2) + ArrowPos, transform.rotation);

                Rigidbody arrowEnemy = arrow.GetComponent<Rigidbody>();
                arrowEnemy.velocity = transform.forward * 30;

                Destroy(arrow, arrowLifeTime);
                timeElapsed = 0;


            }


        }

        if (enemyType == "magic")
        {

            if (timeElapsed > attackSpeed)
            {
                //sets the animation
                animator.SetBool("Attacking", true);
                //make navigation idle
                nav.SetDestination(transform.position);
                if ((Vector3.Distance(playerPosition.position, transform.position) - tempTime) < attackingRange) {
                    objPlayer.SendMessage("giveDamage", damage);
                    
                }
                timeElapsed = 0;

            }

        }

        //if outside attack range 
        if ((Vector3.Distance(playerPosition.position, transform.position) - tempTime) > attackingRange)
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
        // Stop moving
        nav.SetDestination(transform.position);
        //play dead animation
        animator.SetBool("isDead", true);
        // Delete after animated has played
        Destroy(gameObject, 5f);
        if (!isDeadconfirmed) {
            isDeadconfirmed = true;
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("giveGold", goldAmount);
        }
       
    }

    void OnDrawGizmosSelected()
    {
        //seeing range
        temp = transform.position;
        temp.y += 3.5f;
        float totalFOV = visibleFOV;
        float rayRange = visibleRange;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(temp, leftRayDirection * rayRange);
        Gizmos.DrawRay(temp, rightRayDirection * rayRange);

        //hearing range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, earshotRange);


    }

   

    public void giveDamage(int damage) // If hit - apply damage
    {
        health -= damage;

    }

    
}
