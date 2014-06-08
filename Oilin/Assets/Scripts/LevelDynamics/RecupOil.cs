using UnityEngine;
using System.Collections;

public class RecupOil : MonoBehaviour {
	private PlayerOil playerOil;
	private GameObject player;
	
	// Use this for initialization
	void Awake () {
		player = GameObject.Find("N40");
		playerOil = player.GetComponent<PlayerOil>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{	
			playerOil.playerOil += 1;
			
			Destroy(gameObject);
		}
	}
}
