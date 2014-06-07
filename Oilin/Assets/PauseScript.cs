using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {
	GameMode mode = GameMode.PLAYING;
	
	public enum GameMode
	{
		PLAYING,
		LOST,
		PAUSED
	}
	// Use this for initialization
	void Start () {
		mode = GameMode.PLAYING;

	}
	
	// Update is called once per frame
	void Update () {

		if (mode == GameMode.PLAYING) {


			if (Input.GetKeyUp ("p")) {

				guiTexture.color = new Color(guiTexture.color.r, guiTexture.color.g, guiTexture.color.b, 134/255f);
				mode = GameMode.PAUSED;
				Object[] objects = FindObjectsOfType (typeof(GameObject));
				foreach (GameObject go in objects) {
					go.SendMessage ("OnPauseGame", SendMessageOptions.DontRequireReceiver);
				}
				Time.timeScale = 0;
			}
		}
		else
		{
			if (Input.GetKeyUp ("p")) {
				guiTexture.color = new Color(guiTexture.color.r, guiTexture.color.g, guiTexture.color.b, 0f);
				mode = GameMode.PLAYING;
				Object[] objects = FindObjectsOfType (typeof(GameObject));
				foreach (GameObject go in objects) {
					go.SendMessage ("OnResumeGame", SendMessageOptions.DontRequireReceiver);
				}
				Time.timeScale = 1;

			}
		}

	}
}
