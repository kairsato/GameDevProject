using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SimpleFSM : MonoBehaviour 
{
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
        Dead,
    }

	// Current state that the NPC is reaching
	public FSMState curState;

	protected Transform playerTransform;// Player Transform

	// Waypoints
	public GameObject[] waypointList;
	//Navmesh agent
	private NavMeshAgent nav;

	//WayPointNumber
	protected int WPN = 0;

	// Turret
	public GameObject turret;
	public float turretRotSpeed = 4.0f;
	
    // Bullet
	public GameObject bullet;
	public GameObject bulletSpawnPoint;

	// Bullet shooting rate
	public float shootRate = 3.0f;
	protected float elapsedTime;

    // Whether the NPC is destroyed or not
    protected bool bDead;
    public int health = 100;

	// Ranges for chase and attack
	public float chaseRange = 35.0f;
	public float attackRange = 20.0f;
	public float attackRangeStop = 10.0f;

	public GameObject explosion;
	public GameObject smokeTrail;

    /*
     * Initialize the Finite state machine for the NPC tank
     */
	void Start() {
	//declaring what the navmesh agent is
	nav = GetComponent<NavMeshAgent>();

        curState = FSMState.Patrol;

        bDead = false;
        elapsedTime = 0.0f;

        // Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        if(!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

	}


    // Update each frame
    void Update() {
        switch (curState) {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.Attack: UpdateAttackState(); break;
            case FSMState.Dead: UpdateDeadState(); break;
        }

        // Update the time
        elapsedTime += Time.deltaTime;

        // Go to dead state if no health left
        if (health <= 0)
            curState = FSMState.Dead;
    }

	/*
     * Patrol state
     */
    protected void UpdatePatrolState() {
        
	// NavMeshAgent move code goes here
	nav.SetDestination(waypointList[WPN].transform.position);
	//go from 1 point to another
	if(!nav.pathPending && nav.remainingDistance < 0.5f){
	    WPN += 1;
	}
	if(WPN > 3){
	    WPN = 0;
	}
        // Check the distance with player tank
        // When the distance is near, transition to chase state
        if (Vector3.Distance(transform.position, playerTransform.position) <= chaseRange) {
            curState = FSMState.Chase;
        }
    }


    /*
     * Chase state
	 */
    protected void UpdateChaseState() {

	// NavMeshAgent move code goes here
	nav.SetDestination(playerTransform.position);
	// updating the state if the player goes out of the chase range
	if(Vector3.Distance(transform.position, playerTransform.position) > chaseRange){
	    curState = FSMState.Patrol;
	}
        // Check the distance with player tank
        // When the distance is near, transition to attack state
		float dist = Vector3.Distance(transform.position, playerTransform.position);
		if (dist <= attackRange) {
            curState = FSMState.Attack;
        }
        // Go back to patrol is it become too far
        else if (dist >= chaseRange) {
			curState = FSMState.Patrol;
		}
		
	}
	

	/*
	 * Attack state
	 */
    protected void UpdateAttackState() {
		// Check the distance with the player tank
        float dist = Vector3.Distance(transform.position, playerTransform.position);
		if (dist > attackRange) {
			curState = FSMState.Chase;
		}
		// Transition to patrol if the tank is too far
        else if (dist >= chaseRange) {
			curState = FSMState.Patrol;
		}
	if(dist <= attackRangeStop){
		nav.isStopped = true;
	}else if(dist > attackRangeStop){
		nav.isStopped = false;
	}

        // Always Turn the turret towards the player
		if (turret) {
			Quaternion turretRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        	turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, turretRotation, Time.deltaTime * turretRotSpeed); 
		}

        // Shoot the bullets
        ShootBullet();
    }


    /*
     * Dead state
     */
    protected void UpdateDeadState() {
	//disable the navmesh
	nav.enabled = false;
        // Show the dead animation with some physics effects
        if (!bDead) {
            bDead = true;
            Explode();
        }
    }


    /*
     * Shoot Bullet
     */
    private void ShootBullet() {
        if (elapsedTime >= shootRate) {
			if ((bulletSpawnPoint) & (bullet)) {
            	// Shoot the bullet
            	Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			}
            elapsedTime = 0.0f;
        }
    }

    // Apply Damage if hit by bullet
    public void ApplyDamage(int damage ) {
    	health -= damage;
    }


    protected void Explode() {
        float rndX = Random.Range(8.0f, 12.0f);
        float rndZ = Random.Range(8.0f, 12.0f);
        for (int i = 0; i < 3; i++) {
            GetComponent<Rigidbody>().AddExplosionForce(10.0f, transform.position - new Vector3(rndX, 2.0f, rndZ), 45.0f, 40.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 10.0f, rndZ));
        }

		if (smokeTrail) {
			GameObject clone = Instantiate(smokeTrail, transform.position, transform.rotation) as GameObject;
			clone.transform.parent = transform;
		}
		Invoke ("CreateFinalExplosion", 1.4f);
		Destroy(gameObject, 1.5f);
	}
	
	
	protected void CreateFinalExplosion() {
		if (explosion) 
			Instantiate(explosion, transform.position, transform.rotation);
	}


	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, chaseRange);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}

}
