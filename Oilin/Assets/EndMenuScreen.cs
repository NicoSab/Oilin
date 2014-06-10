using UnityEngine;
using System.Collections;

public class EndMenuScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void Show()
	{
		GameObject endScreen = GameObject.Find ("EndScreen");
		DisplayGameObject (endScreen, 132);
		GameObject bestScoreTitle = GameObject.Find ("BEST SCORE");
		DisplayGameObject (bestScoreTitle);

		GameObject gonextlevel = GameObject.Find ("Gonextlevel");
		DisplayGameObject (gonextlevel);
		GameObject timescore = GameObject.Find ("Timescore");
		DisplayGameObject (timescore);
		timescore.guiText.text = LevelCountDown.timeScore.ToString ();

		GameObject bestScore = GameObject.Find ("BestTotalscore");
		DisplayGameObject (bestScore);
		bestScore.guiText.text = GetHiScore ().ToString ();

		GameObject title;
		if (LevelCountDown.timeScore <= 0)
			title = GameObject.Find ("YOU LOSE");
		else
			title = GameObject.Find ("YOU WON");
		DisplayGameObject (title);

		GameObject yourScoreTitle = GameObject.Find ("YOUR SCORE");
		DisplayGameObject (yourScoreTitle);
	}
	void DisplayGameObject(GameObject go, float? opacity = null)
	{
		if (go.guiTexture != null)
			go.guiTexture.color = new Color (go.guiTexture.color.r, go.guiTexture.color.g, go.guiTexture.color.b, opacity == null ? 1f : opacity.Value / 255f);
		else
			go.guiText.color = new Color (go.guiText.color.r, go.guiText.color.g, go.guiText.color.b, opacity == null ? 1f : opacity.Value / 255f);

	}
	
	public void SaveScore ()
	{
		if (GetHiScore () < LevelCountDown.timeScore)
			PlayerPrefs.SetInt (Application.loadedLevelName, LevelCountDown.timeScore);
	}
	
	
	int GetHiScore ()
	{
		return PlayerPrefs.GetInt (Application.loadedLevelName);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
