using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject resetGUI;

	private PlayerController playerController;
	private PlayerStats playerStats;
	private GameObject[] planets;
	private Text resetText;
	private float maxDistance;
	private float currentDistance;
	private bool canReset;
	private float offset;

	// Use this for initialization
	void Start () {
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		playerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();

		planets = GameObject.FindGameObjectsWithTag ("Planet");
		resetText = resetGUI.GetComponent<Text> ();
		init ();
	}

	// gets called at the start, as well as every time the player resets
	void init() {
		resetGUI.SetActive (false);
		maxDistance = 0;
		canReset = false;
	}

	void reset() {
		init ();
		playerController.reset ();
	}

	public float getMaxDistance() {
		return maxDistance;
	}

	public float getCurrentDistance() {
		return currentDistance;
	}

	public void setCanReset(bool b) {
		canReset = b;
	}

	public bool getCanReset() {
		return canReset;
	}

	public void setOffset(float f) {
		offset = f;
	}

	public GameObject getClosestPlanet (Vector3 playerPos) {
		GameObject result = planets[0];
		foreach (GameObject planet in planets) {
			if (Vector3.Distance (planet.transform.position, playerPos) < Vector3.Distance (result.transform.position, playerPos)) {
				result = planet;
			}
		}

		return result;
	}

	public void setTotalDistance(float distance) {
		if ((distance - offset) > maxDistance) {
			maxDistance = (distance - offset);
		}

		currentDistance = (distance - offset);
	}

	float distanceToDollars() {
		return (float)System.Math.Round((getMaxDistance () / 10), 2);
	}

	void showReset() {
		resetText.text = "Total Distance: " + getMaxDistance () + "km\n" + "Payout: $" + distanceToDollars () + "\n\nPress Enter to Continue";
		resetGUI.SetActive (true);
		if (Input.GetKeyDown (KeyCode.Return)) {
			playerStats.addAmount (distanceToDollars ());
			reset ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (canReset)
			showReset ();
	}
}
