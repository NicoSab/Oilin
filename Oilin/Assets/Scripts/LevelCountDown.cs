using UnityEngine;
using System.Collections;

public class LevelCountDown : MonoBehaviour {

	public int startTime = 120;
	private string textTime;
	private EndLevel endLevel;
	public static int timeScore = 1000;
	public static bool ended = false;
	GameObject scoreTime;

	void Awake()
	{
		endLevel = GameObject.Find ("EndLevel").GetComponent<EndLevel>();
		ended = false;
		scoreTime = GameObject.Find ("ScoreTime");
	}

	void Update()
	{
		int guiTime = (int)(startTime - Time.time);

		int min = guiTime / 60;
		int sec = guiTime % 60;

		if (guiTime <= 0)
			endLevel.EndOfLevel();

		if (!ended)
			timeScore = (guiTime * 1000 / startTime) + PlayerOil.oilScore;
		textTime = timeScore.ToString();
		if (!LevelCountDown.ended)
				scoreTime.guiText.text = textTime;
		else 
				scoreTime.guiText.text = "";
	}
}
