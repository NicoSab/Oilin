using UnityEngine;
using System.Collections;

public class PlumMovement : MonoBehaviour {

	private Transform player;
	private Vector3 relPlumPos;
	private Vector3 newPos;
	public float smooth = 1.5f;

	void Awake()
	{
		player = GameObject.Find("Robot Kyle").transform;

		relPlumPos = transform.position - player.position;
	}

	void FixedUpdate()
	{
		Vector3 standardPos = player.position + relPlumPos;
		newPos = standardPos;
		transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
		SmoothLookAt();
	}
	
	void SmoothLookAt ()
	{
		Vector3 relPlayerPosition = player.position - transform.position;

		Quaternion lookAtRotation = Quaternion.LookRotation(-relPlayerPosition, Vector3.up);

		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, 2*smooth * Time.deltaTime);
	}
}
