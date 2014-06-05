using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float turnSmoothing = 15f;	// A smoothing value for turning the player.
	public float speedDampTime = 0.1f;	// The damping for the speed parameter
	public bool activ = true;
	public GameObject[] enemies;

	private Animator anim;				// Reference to the animator component.

	void Awake ()
	{
		anim = GetComponent<Animator>();
	}
	
	
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		MovementManagement(h, v);

		if (Input.GetKeyDown("a")) {
			Transform enemy = findNearestEnemy();
			Rotating(enemy.position.x, enemy.position.z);
			anim.SetTrigger("Punch");
		}
	}
	
	
	void Update ()
	{
	}

	public void setActive(bool activeNew)
	{
		activ = activeNew;
	}
	
	void MovementManagement (float horizontal, float vertical)
	{	
		// If there is some axis input...
			if(activ && (horizontal != 0f || vertical != 0f))
			{
				// ... set the players rotation and set the speed parameter to 5.5f.
				Rotating(horizontal, vertical);
				anim.SetFloat("Speed", 5.5f, speedDampTime, Time.deltaTime);
			}
			else
				// Otherwise set the speed parameter to 0.
				anim.SetFloat("Speed", 0);
	}
	
	void Rotating (float horizontal, float vertical)
	{
		print ("here");
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(newRotation);
	}

	Transform findNearestEnemy ()
	{
		float deltaX;
		float deltaZ;
		Transform res = null;
		float min = 1000f;
			
		foreach (GameObject enemy in enemies) {
			deltaX = enemy.transform.position.x - transform.position.x;
			deltaZ = enemy.transform.position.z - transform.position.z;
			float dist = Mathf.Sqrt(Mathf.Pow (deltaX, 2) + Mathf.Pow(deltaZ, 2));
			if (dist < min) {
				min = dist;
				res = enemy.transform;
			}
		}
		return res;
	}
}
