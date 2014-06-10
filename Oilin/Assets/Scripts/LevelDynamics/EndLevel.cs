using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {
	
	private GameObject player;                      // Reference to the player.
	private Animator playerAnim;                    // Reference to the players animator component.
	private CameraMovement camMovement;             // Reference to the camera movement script.
	private ScreenFader sf;	 						// Reference to the screen fader;
	private float timer;
	private float end1 = 1f;
	private float end2 = 2f;
	private bool col = false;
	private PlayerKey playerKey;
	private EndMenuScreen endMenu;

	void Awake ()
	{
		// Setting up references.
		player = GameObject.Find("N40");
		playerAnim = player.GetComponent<Animator>();
		playerKey = player.GetComponent<PlayerKey>();
		camMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
		sf = GameObject.Find("ScreenFader").GetComponent<ScreenFader>();
		endMenu = GameObject.Find("EndScreen").GetComponent<EndMenuScreen>();

	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player && playerKey.playerHasKey)
		{
			col = true;
		}
	}
	
	public void EndOfLevel()
	{
		timer += Time.deltaTime;

		if(timer >= end1)
		{
			playerAnim.SetFloat("Speed", 0f);
			camMovement.enabled = false;
			player.transform.parent = transform;
			LevelCountDown.ended = true;
			endMenu.SaveScore();
			endMenu.Show();
		}
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
		{
			if (col)
				EndOfLevel();
		}
	}
}