using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FSM : MonoBehaviour
{

    public enum FSMStates{ // States the AI can be in
        None,
        Wander,
        Attack,
        Idle,
        Dead,
    }

    public FSMStates currentState; // Current state

    public float movementSpeed = 15.0f;  // Speed the enemy AI moves to a location (or towards the player)
    public float rotationSpeed = 10.0f;  // Speed the enemy AI rotates
    public float attackSpeed = 5.0f;  // Speed/interval between each attack

    protected Transform playerPosition; // Players position the AI will chase when in range
    protected Vector3 destination; // Destination location when wandering
    public float wanderRadius = 20.0f; // Radius the AI will pick which is a random spot within this radius to wander towards

    protected bool Dead; // Determines if the AI is dead - intiate dead state

    public int health = 100; // Health value of the enemy



    // Start is called before the first frame update
    void Start()
    {

        currentState = FSMStates.Wander; // Start wandering when first created

        // Get object/tag of the player to be used in determining the players position to the AI
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerPosition = playerObject.transform;
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


    }

    // Attack State

    protected void UpdateAttackState()
    {


    }

    // Idle State

    protected void UpdateIdleState()
    {


    }

    // Dead State
    protected void UpdateDeadState()
    {


    }

}
