using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FSM : MonoBehaviour
{

    public enum FSMStates
    { // States the AI can be in
        None,
        Chase,
        Attack,
        Dead,
    }

    public FSMStates currentState; // Current state

    public float movementSpeed = 15.0f;  // Speed the enemy AI moves to a location (or towards the player)


    public float attackSpeed = 5.0f;  // Speed/interval between each attack

    protected bool Dead; // Determines if the AI is dead - intiate dead state

    public int health = 100; // Health value of the enemy

    public float attackingRange = 5f;

    //animation
    private Animator animator;
    private bool isDeadconfirmed = false;

    private GameObject objPlayer;

    public float attackDelay; // Delay between the attacking animation starting and when a hit is registered on the player
    public float damage; // damage which can be dealt to player

    public float timeElapsed = 0;

    private Vector3 velocity;
    private Vector3 lastpos;

    public Transform playerPosition; // Players position the AI will chase when in range

    //timediff
    private float setTime;
    private float tempTime;

    GameObject objStart;
    Rigidbody objStart_rigidbody;

    public GameObject WinScreen;


    // Start is called before the first frame update
    void Start()
    {

        objStart = GameObject.Find("Boss");

        objStart_rigidbody = objStart.GetComponent<Rigidbody>();

        timeElapsed = 0;

        //gets animations component
        animator = GetComponent<Animator>();

        objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerPosition = objPlayer.transform;

        currentState = FSMStates.Chase; // Start wandering when first created

        if (!playerPosition)
        {
            print("ERROR: Player object not found - assign player object with 'Player' tag");
        }

        Dead = false; // AI is not dead - therefore false
        lastpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        velocity = (transform.position - lastpos) / Time.deltaTime;
        lastpos = transform.position;

        switch (currentState) // Depending on the current state - call respective function to update current state
        {
            case FSMStates.Attack: UpdateAttackState(); break;
            case FSMStates.Chase: UpdateChaseState(); break;
            case FSMStates.Dead: UpdateDeadState(); break;

        }

        if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }

    // Attack State
    protected void UpdateChaseState()
    {

        //sets the animation
        animator.SetFloat("Velocity", velocity.magnitude);

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
        if (timeElapsed >= attackSpeed)
        {
            animator.SetBool("attacking", true);
            objPlayer.SendMessage("giveDamage", damage);
            timeElapsed = 0;
        }
    }

    // Dead State
    protected void UpdateDeadState()
    {
        // Stop moving
        //nav.SetDestination(transform.position);
        objStart_rigidbody.velocity = transform.forward * 0;

        //play dead animation
        animator.SetBool("isDead", true);
        WinScreen.SetActive(true);
        // Delete after animated has played
        Destroy(gameObject, 5f);
        if (!isDeadconfirmed)
        {
            isDeadconfirmed = true;
            

        }

    }

    public void giveDamage(int damage) // If hit - apply damage
    {
        health -= damage;

    }

}
