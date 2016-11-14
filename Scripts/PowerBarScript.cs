using UnityEngine;
using System.Collections;

public class PowerBarScript : MonoBehaviour {

	private float maxPower;
	private float currentPower;
	private float initialWidth;
	private float currentWidth;

	// Use this for initialization
	void Start () {
		initialWidth = GetComponent<RectTransform> ().localScale.x;
		init ();
	}

	public void init() {
		GameObject hull = GameObject.FindGameObjectWithTag ("NoseCone");
		maxPower = hull.GetComponent<NoseConeScript> ().maxPower;
		currentPower = maxPower;
	}

	void updateHealth() {
		if (currentPower > maxPower) {
			currentPower = maxPower;
		}

		if (currentPower < 0) {
			currentPower = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		currentPower = GetComponentInParent<PlayerController> ().getCurrentPower ();
		updateHealth ();
		GetComponent<RectTransform> ().localScale = new Vector3(initialWidth * (currentPower / maxPower), GetComponent<RectTransform> ().localScale.y, 0);
	}
}
