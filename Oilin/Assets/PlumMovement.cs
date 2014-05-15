using UnityEngine;
using System.Collections;

public class PlumMovement : MonoBehaviour {

	private Transform player;
	private Vector3 relPlumPos;
	private Vector3 newPos;
	public float smooth = 1.5f;
	public float turnSmoothing = 15f;	// A smoothing value for turning the player.
	public float speedDampTime = 0.1f;	// The damping for the speed parameter

	private bool activeMov = false;
	private Animator anim;

	void Awake()
	{
			player = GameObject.Find("Robot Kyle").transform;
			relPlumPos = transform.position - player.position;
			anim = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if (!activeMov) {
			Vector3 standardPos = player.position + relPlumPos;
			newPos = standardPos;
			transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
			SmoothLookAt();
		} else {
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			
			MovementManagement(h, v);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown("space")) {
			GameObject.Find("Robot Kyle").SendMessage("setActive", activeMov);
			if (!activeMov)
				Camera.main.SendMessage("SetPlayer", transform);
			else
				Camera.main.SendMessage("SetPlayer", player);
			activeMov = !activeMov;
		}
	}
	
	void SmoothLookAt ()
	{
		Vector3 relPlayerPosition = player.position - transform.position;

		Quaternion lookAtRotation = Quaternion.LookRotation(-relPlayerPosition, Vector3.up);

		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, 2*smooth * Time.deltaTime);
	}

	void MovementManagement (float horizontal, float vertical)
	{	
		// If there is some axis input...
		if(horizontal != 0f || vertical != 0f)
		{
			// ... set the players rotation and set the speed parameter to 5.5f.
			Rotating(-horizontal, -vertical);

			transform.position += new Vector3 (horizontal * speedDampTime, 0, vertical * speedDampTime);

			//transform.Translate(vertical * speedDampTime, 0, horizontal * speedDampTime);
			//anim.SetFloat("Speed", 5.5f, speedDampTime, Time.deltaTime);
		}
		//else
			// Otherwise set the speed parameter to 0.
			//anim.SetFloat("Speed", 0);
	}
	
	
	void Rotating (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(newRotation);
	}
}
