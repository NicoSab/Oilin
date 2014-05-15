using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FadeScreen()
	{
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		guiTexture.color = Color.Lerp(guiTexture.color, Color.black, 1.5f * Time.deltaTime);
		
		if(guiTexture.color.a >= 0.95f)
			Application.LoadLevel(0);
	}
}
