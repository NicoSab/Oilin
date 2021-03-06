using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
	public float fieldOfViewAngle = 110f;				// Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight;							// Whether or not the player is currently sighted.
	public Vector3 personalLastSighting;				// Last place this enemy spotted the player.
	

	private Animator anim;								// Reference to the Animator.
	private EnemyAnimation enemyAnimation;				// Reference to the EnemyAnimation script.
	private LastPlayerSighting lastPlayerSighting;		// Reference to last global sighting of the player.
    private GameObject player;							// Reference to the player.
	private PlayerHealth playerHealth;					// Reference to the player's health script.
	private Vector3 previousSighting;					// Where the player was sighted last frame.
	
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		enemyAnimation = GetComponent<EnemyAnimation>();
		lastPlayerSighting = GameObject.Find("gameController").GetComponent<LastPlayerSighting>();
		player = GameObject.Find("N40");
		playerHealth = player.GetComponent<PlayerHealth>();
		
		// Set the personal sighting and the previous sighting to the reset position.
		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
	}
	protected bool paused;
	
	void OnPauseGame ()
	{
		paused = true;
	}
	
	void OnResumeGame ()
	{
		paused = false;
	}
	void Update ()
	{
		if (!paused) 
		{
			// If the last global sighting of the player has changed...
			if(lastPlayerSighting.position != previousSighting)
				// ... then update the personal sighting to be the same as the global sighting.
				personalLastSighting = lastPlayerSighting.position;
			
			// Set the previous sighting to the be the sighting from this frame.
			previousSighting = lastPlayerSighting.position;
			
			// If the player is alive...
			if(!enemyAnimation.enemyDead && playerHealth.health > 0f)
				// ... set the animator parameter to whether the player is in sight or not.
				anim.SetBool("PlayerInSight", playerInSight);
			else
				// ... set the animator parameter to false.
				anim.SetBool("PlayerInSight", false);
		}
	}

	void OnTriggerStay (Collider other)
    {
		// If the player has entered the trigger sphere...
        if(other.gameObject == player)
        {
			// By default the player is not in sight.
			playerInSight = false;
			
			// Create a vector from the enemy to the player and store the angle between it and forward.
            Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			// If the angle between forward and where the player is, is less than half the angle of view...
			if(!enemyAnimation.enemyDead && angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;

				// ... and if a raycast towards the player hits something...
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, 10f))
				{
					// ... and if the raycast hits the player...
					if(hit.collider.gameObject == player)
					{
						// ... the player is in sight.
						playerInSight = true;
						
						// Set the last global sighting is the players current position.
						lastPlayerSighting.position = player.transform.position;
					}
				}
			}
        }
    }

	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if(other.gameObject == player)
			// ... the player is not in sight.
			playerInSight = false;
	}
}
