using UnityEngine;
using System.Collections;

public class LeftArrowGesture : MonoBehaviour {

	Material normal;
	Material hover;
	public bool showed;
	float timer = 0f;
	float maxx = 0.5f;
	float maxy = 0.4f;
	float maxz = 0.4f;
	GameObject play;
	PlayGesture playgesture;

	// Use this for initialization
	void Start () {
		play = GameObject.Find ("play");
		playgesture = play.GetComponent<PlayGesture>();
		normal = Resources.Load ("leftarrow", typeof(Material)) as Material;
		hover = Resources.Load ("leftarrowhover", typeof(Material)) as Material;
	}
	void OnMouseDown()
	{
		playgesture.RewardIndex ();
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
		if (showed && timer < 3f)
		{
			Vector3 v = new Vector3();
			v.x = maxx * timer / 3f;
			v.y = maxy * timer / 3f;
			v.z = maxz * timer / 3f;
			transform.localScale = v;
			timer += 0.5f;
		}
	}
}
