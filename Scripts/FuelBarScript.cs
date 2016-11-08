using UnityEngine;
using System.Collections;

public class FuelBarScript : MonoBehaviour {

	private float maxFuel;
	private float currentFuel;
	private float initialWidth;
	private float currentWidth;

	// Use this for initialization
	void Start () {
		initialWidth = GetComponent<RectTransform> ().localScale.x;
		init ();
	}

	public void init() {
		GameObject fuelPod = GameObject.FindGameObjectWithTag ("FuelPod");
		maxFuel = fuelPod.GetComponent<FuelPodScript> ().maxFuel;
		currentFuel = maxFuel;
	}

	void updateFuel() {
		if (currentFuel > maxFuel) {
			currentFuel = maxFuel;
		}

		if (currentFuel < 0) {
			currentFuel = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		currentFuel = GetComponentInParent<PlayerController> ().getCurrentFuel();
		updateFuel ();
		GetComponent<RectTransform> ().localScale = new Vector3(initialWidth * (currentFuel / maxFuel), GetComponent<RectTransform> ().localScale.y, 0);
	}
}
