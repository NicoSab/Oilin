using UnityEngine;
using System.Collections;

public class ExitGesture : MonoBehaviour {
	Material normal;
	Material hover;
	// Use this for initialization
	void Start () {
		normal = Resources.Load ("exit", typeof(Material)) as Material;
		hover = Resources.Load ("exithover", typeof(Material)) as Material;
	}
	void OnMouseDown()
	{
		Application.Quit ();
	}
	void OnMouseOver()
	{
		renderer.material = hover;
	}
	void OnMouseExit()
	{
		renderer.material = normal;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
