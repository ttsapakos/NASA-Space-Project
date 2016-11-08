using UnityEngine;
using System.Collections;

public class OxygenBarScript : MonoBehaviour {

	private float maxOxygen;
	private float currentOxygen;
	private float initialWidth;
	private float currentWidth;

	// Use this for initialization
	void Start () {
		initialWidth = GetComponent<RectTransform> ().localScale.x;
		init ();
	}

	public void init() {
		GameObject hull = GameObject.FindGameObjectWithTag ("NoseCone");
		maxOxygen = hull.GetComponent<NoseConeScript> ().maxOxygen;
		currentOxygen = maxOxygen;
	}

	void updateHealth() {
		if (currentOxygen > maxOxygen) {
			currentOxygen = maxOxygen;
		}

		if (currentOxygen < 0) {
			currentOxygen = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		currentOxygen = GetComponentInParent<PlayerController> ().getCurrentOxygen();
		updateHealth ();
		GetComponent<RectTransform> ().localScale = new Vector3(initialWidth * (currentOxygen / maxOxygen), GetComponent<RectTransform> ().localScale.y, 0);
	}
}
