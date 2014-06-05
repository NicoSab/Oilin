using UnityEngine;
using System.Collections;

public class ActiveDoor : MonoBehaviour {

	public GameObject buttonUnit = null;
	public bool requireKey = false;
	public bool requireActivation = true;

	private ActiveButton ab;
	private GameObject player;
	private GameObject plum;
	private Animator anim;
	private bool open = false;

	void Awake()
	{
		ab = buttonUnit.GetComponent<ActiveButton>();
		player = GameObject.Find("N40");
		plum = GameObject.Find("Plum");
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == player || c.gameObject == plum)
		{
			if (requireKey) {
				open = true;
			} else {
				if (requireActivation) {
					if (ab.on)
						open = true;
				}
				else {
					open = true;
				}
			}
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (c.gameObject == player || c.gameObject == plum)
				open = false;
	}

	void Update()
	{
		anim.SetBool("Open", open);
	}
}
