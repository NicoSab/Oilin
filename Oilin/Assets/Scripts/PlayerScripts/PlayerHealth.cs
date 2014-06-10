using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float health = 5f;							// How much health the player has left.
	public float resetAfterDeathTime = 3f;				// How much time from the player dying to the level reseting.
	public AudioClip deathClip;							// The sound effect of the player dying.
	public Texture2D barreVie;
	public Texture2D barreVieBg;
	
	private EndLevel endLevel;
	private Animator anim;								// Reference to the animator component.
	private PlayerMovement playerMovement;				// Reference to the player movement script.
	private LastPlayerSighting lastPlayerSighting;		// Reference to the LastPlayerSighting script.
	private float timer;								// A timer for counting to the reset of the level once the player is dead.
	private bool playerDead;							// A bool to show if the player is dead or not.
	private int chanceBeforeDying = 3;
	private GlowEffect flash;
	private bool flashLaunched = false;
	private bool flashEnding = false;
	private float timerFlash;

	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
		flash = GameObject.Find ("Main Camera").GetComponent<GlowEffect> ();
		GameObject tmp = GameObject.Find ("EndLevel");
		if (tmp != null)
			endLevel = tmp.GetComponent<EndLevel>();
		GameObject gameController = GameObject.Find ("gameController");
		if (gameController != null)
			lastPlayerSighting = gameController.GetComponent<LastPlayerSighting>();
	}

	void OnGUI()
	{
		if (!LevelCountDown.ended)
		{
			if (barreVieBg != null)
				GUI.DrawTexture(new Rect(20,30,barreVieBg.width, barreVieBg.height), barreVieBg);
			if (barreVie != null)
				GUI.DrawTexture(new Rect(20,30,barreVie.width * health / 5, barreVie.height), barreVie);
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
    void Update ()
	{
		if (!paused) 
		{
			if (health <= 0)
			{
				if(chanceBeforeDying == 0)
				{
					// ... and if the player is not yet dead...
					if(!playerDead)
						// ... call the PlayerDying function.
						PlayerDying();
					else
					{
						// Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
						PlayerDead();
						LevelReset();
					}
				}
				else
				{
					health = 5;
					--chanceBeforeDying;
					Transform safePoint = chooseSafePoint();
					LaunchFlash();
					transform.position = safePoint.position;
				}
			}

			if (flashEnding && timerFlash >= 0f)
			{
				flash.glowTint = new Color(timerFlash / 10f, timerFlash / 10f, timerFlash / 10f);
				timerFlash -= 0.5f;
			}
			if (flashEnding && timerFlash < 0f)
			{
				flashEnding = false;
				timerFlash = 0f;
			}
			if (flashLaunched && timerFlash < 3f)
			{
				flash.glowTint = new Color(timerFlash / 3f, timerFlash / 3f, timerFlash / 3f);
				timerFlash += 0.5f;
			}

			if (flashLaunched && timerFlash >= 3f)
			{
				flashEnding = true;
				flashLaunched = false;
				timerFlash = 10f;
			}

		}
	}

	void LaunchFlash()
	{
		flashLaunched = true;
		timerFlash = 0f;
	}
	
	void PlayerDying ()
	{
		// The player is now dead.
		playerDead = true;
		
		// Set the animator's dead parameter to true also.
		anim.SetBool("Dead", playerDead);
		
		// Play the dying sound effect at the player's location.
		//AudioSource.PlayClipAtPoint(deathClip, transform.position);
	}
	
	
	void PlayerDead ()
	{
		// If the player is in the dying state then reset the dead parameter.
		if(anim.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Dying"))
			anim.SetBool("Dead", false);
		
		// Disable the movement.
		anim.SetFloat("Speed", 0f);
		playerMovement.enabled = false;
		
		// Reset the player sighting to turn off the alarms.
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
		
		// Stop the footsteps playing.
		//audio.Stop();
	}
	
	
	void LevelReset ()
	{
		// Increment the timer.
		timer += Time.deltaTime;
		
		//If the timer is greater than or equal to the time before the level resets...
		if(timer >= resetAfterDeathTime)
			// ... reset the level.
			endLevel.EndOfLevel();
	}
	
	
	public void TakeDamage (float amount)
    {
		// Decrement the player's health by amount.
        health -= amount;
    }

	Transform chooseSafePoint()
	{
		float delta;
		Transform res = null;
		float min = 1000f;

		foreach (Transform safePoint in lastPlayerSighting.SafePoints) {
			delta = Mathf.Sqrt(Mathf.Pow(safePoint.position.x - transform.position.x, 2) 
			                   + Mathf.Pow(safePoint.position.z - transform.position.z, 2));
			if (delta < min)
			{
				min = delta;
				res = safePoint;
			}
		}

		return res;
	}
}
