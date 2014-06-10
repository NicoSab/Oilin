using UnityEngine;
using System.Collections;

public class PlayerOil : MonoBehaviour
{
	public int playerOil = 0;
	public Texture2D barreHuile;
	public Texture2D barreHuileBg;
	int countOils;
	public static int oilScore = 0;

	void Awake()
	{
		countOils = FindObjectsOfType (typeof(RecupOil)).Length;
	}
	public void IncrementOil()
	{
		playerOil++;
		oilScore = playerOil * 1000 / countOils;
	}
	void OnGUI()
	{
		if (!LevelCountDown.ended) {
			if (barreHuileBg != null)
					GUI.DrawTexture (new Rect (Screen.width - 50, 30, barreHuileBg.width, barreHuileBg.height), barreHuileBg);
			if (barreHuile != null)
					GUI.DrawTexture (new Rect (Screen.width - 50, barreHuile.height + 30 - barreHuile.height * playerOil / countOils,
                         barreHuile.width, barreHuile.height * playerOil / countOils), barreHuile);
		}
	}
}

