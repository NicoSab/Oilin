using UnityEngine;
using System.Collections;

public class RecupCard : MonoBehaviour {

	private PlayerKey playerKey;
	private GameObject player;

	// Use this for initialization
	void Awake () {
		player = GameObject.Find("Robot Kyle");
		playerKey = player.GetComponent<PlayerKey>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{

			playerKey.playerHasKey = true;

			Destroy(gameObject);
		}
	}
	
}
