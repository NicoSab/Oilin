using UnityEngine;
using System.Collections;

public class PlayerOil : MonoBehaviour
{
	public int playerOil = 0;
	public Texture2D barreHuile;
	public Texture2D barreHuileBg;
	int countOils;

	void Awake()
	{
		countOils = FindObjectsOfType (typeof(RecupOil)).Length;
	}
	
	void OnGUI()
	{
		if (barreHuileBg != null)
			GUI.DrawTexture(new Rect(Screen.width - 200, 30,barreHuileBg.width, barreHuileBg.height), barreHuileBg);
		if (barreHuile != null)
			GUI.DrawTexture(new Rect(Screen.width - 200, barreHuile.height + 30 - barreHuile.height * playerOil / countOils,
			                         barreHuile.width, barreHuile.height * playerOil / countOils), barreHuile);
	}
}

