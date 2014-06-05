using UnityEngine;
using System.Collections;

public class PlayGesture : MonoBehaviour {

	Material normal;
	Material hover;
	// Use this for initialization
	void Start () {
		normal = Resources.Load ("play", typeof(Material)) as Material;
		hover = Resources.Load ("playhover", typeof(Material)) as Material;
	}
	void OnMouseDown()
	{
		Application.LoadLevel ("level1");
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
