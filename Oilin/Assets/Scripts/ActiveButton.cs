using UnityEngine;
using System.Collections;

public class ActiveButton : MonoBehaviour {

	public bool on = false;
	public Material unlockMat;

	private GameObject player;
	private GameObject plum;

	void Awake() 
	{
		player = GameObject.Find ("N40");
		plum = GameObject.Find ("Plum");
	}

	void OnTriggerStay(Collider c)
	{
		if (c.gameObject == player || c.gameObject == plum)
		{
			if (Input.GetKeyDown("r"))
			{
				on = true;
				Renderer screen = transform.Find("prop_switchUnit_screen_001").renderer;
				screen.material = unlockMat;
			}
		}
	}
}
