﻿using UnityEngine;
using System.Collections;

public class GoNextLevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		guiTexture.texture = Resources.Load ("gonextlevel", typeof(Texture)) as Texture;
	}
	void OnMouseDown()
	{
		//Application.LoadLevel ("level" + indexLevel.ToString());
	}
	void OnMouseOver()
	{
		guiTexture.texture = Resources.Load ("gonextlevelhover", typeof(Texture)) as Texture;
	}
	void OnMouseExit()
	{
		guiTexture.texture = Resources.Load ("gonextlevel", typeof(Texture)) as Texture;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
