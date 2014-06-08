using UnityEngine;
using System.Collections;

public class PlayGesture : MonoBehaviour {

	Material normal;
	Material hover;
	public bool chooseLevel = false;
	int indexLevel = 1;
	GameObject ra, la;
	RightArrowGesture rightArrow;
	LeftArrowGesture leftArrow;
	// Use this for initialization
	void Start () {
		normal = Resources.Load ("play", typeof(Material)) as Material;
		hover = Resources.Load ("playhover", typeof(Material)) as Material;
		ra = GameObject.Find ("rightarrow");
		la = GameObject.Find ("leftarrow");
		rightArrow = ra.GetComponent<RightArrowGesture>();
		leftArrow = la.GetComponent<LeftArrowGesture>();

	}
	public void RewardIndex()
	{
		if (indexLevel > 1)
			indexLevel--;
		renderer.material = Resources.Load ("level" + indexLevel.ToString(), typeof(Material)) as Material;
	}
	public void ForwardIndex()
	{
		if (indexLevel < 5)
			indexLevel++;
		renderer.material = Resources.Load ("level" + indexLevel.ToString(), typeof(Material)) as Material;

	}
	void OnMouseDown()
	{
		if (!chooseLevel)
		{
			rightArrow.showed = true;
			leftArrow.showed = true;

			chooseLevel = true;
		} else {
			Application.LoadLevel ("level" + indexLevel.ToString());
		}
	}
	void OnMouseOver()
	{
		if (chooseLevel) {
			renderer.material = Resources.Load ("level" + indexLevel.ToString() + "hover", typeof(Material)) as Material;
		}
		else
			renderer.material = hover;
	}
	void OnMouseExit()
	{
		if (chooseLevel)
			renderer.material = Resources.Load ("level" + indexLevel.ToString(), typeof(Material)) as Material;
		else
			renderer.material = normal;
	}

	// Update is called once per frame
	void Update () {
	}
}
