﻿using UnityEngine;
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
	private bool hasKey;

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
				hasKey = player.GetComponent<PlayerKey>().playerHasKey;
				if (hasKey)
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
	protected bool paused;
	
	void OnPauseGame ()
	{
		paused = true;
	}
	
	void OnResumeGame ()
	{
		paused = false;
	}
	void Update()
	{
		if (!paused) 
			anim.SetBool("Open", open);
	}
}
