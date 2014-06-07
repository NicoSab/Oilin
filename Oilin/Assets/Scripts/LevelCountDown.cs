using UnityEngine;
using System.Collections;

public class LevelCountDown : MonoBehaviour {

	public int startTime = 120;
	private string textTime;
	private EndLevel endLevel;

	void Awake()
	{
		endLevel = GameObject.Find ("EndLevel").GetComponent<EndLevel>();
	}

	void OnGUI()
	{
		int guiTime = (int)(startTime - Time.time);

		int min = guiTime / 60;
		int sec = guiTime % 60;

		if (guiTime <= 0)
			endLevel.EndOfLevel();

		textTime = string.Format("{0:00} : {1:00}", min, sec);
		GUI.Label(new Rect(400, 25, 200, 30), textTime);
	}
}
