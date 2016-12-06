using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

	public string winText = "Pants Delivered! Congratulations!";

	float [] rewardDistances = new float[]{
		30.0f,
		1500.0f,
		4000.0f,
		8000.0f,
		15000.0f,
	};

	float [] rewardAmounts = new float[]{
		1000.0f,
		20000.0f,
		100000.0f,
		500000.0f,
		2000000.0f
	};

	bool [] rewardReached = new bool[]{
		false,
		false,
		false,
		false,
		false
	};

	// Reset text gui object
	public GameObject resetGUI;
	// the distance at which the planet can be removed
	public float removePlanetDistance;
	// the health bar
	public HealthBarScript healthBar;
	// the oxygen bar
	public PowerBarScript powerBar;
	// the fuel bar
	public FuelBarScript fuelBar;

	private PlayerController playerController;
	private PlayerStats playerStats;
	// list of planets
	private List<GameObject> planets;
	// start planet, earth
	private GameObject earth;
	private Text resetText;
	// max distance the player has gone from earth
	private float maxDistance;
	private float currentDistance;
	private bool canReset;
	// offset from center of planet for distance traveled calculation
	private float offset;
	private float nextTimeToSearch = 0;
	private GameObject closestPlanet;
	// The store object
	private GameObject store;
	// The player's resource bars
	private GameObject resourceBars;


	// Use this for initialization
	void Start () {
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		playerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();
		earth = GameObject.FindGameObjectWithTag ("Earth");
		resetText = resetGUI.GetComponent<Text> ();
		planets = new List<GameObject> ();
		store = GameObject.FindGameObjectWithTag ("Store");
		resourceBars = GameObject.FindGameObjectWithTag ("ResourceBars");
		store.SetActive (false);
		init ();
	}

	// gets called at the start, as well as every time the player resets
	void init() {
		resetGUI.SetActive (false);
		maxDistance = 0;
		canReset = false;
		for (int i = 0; i < planets.Count; i++) {
			GameObject.Destroy (planets [i]);
		}
		planets.Clear ();
		closestPlanet = earth;
	}

	// initialize everything and set the store to active
	void reset() {
		init ();
		resourceBars.SetActive (false);
		store.SetActive (true);
		playerController.reset ();
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Obstacle")) {
			GameObject.Destroy (go);
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("ResourcePickup")) {
			GameObject.Destroy (go);
		}
	}

	// Called to start the round
	public void startRound () {
		healthBar.init ();
		fuelBar.init ();
		powerBar.init ();
		resourceBars.SetActive (true);
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

	public void addPlanet(GameObject planet) {
		planets.Add (planet);
	}

	// Returns the closest planet to the given position
	public GameObject getClosestPlanet (Vector3 playerPos) {
		if (nextTimeToSearch <= Time.time) {
			for (int i = 0; i < planets.Count; i++) {
				float distanceToPlanet = Vector3.Distance (planets[i].transform.position, playerPos);
				if (distanceToPlanet < Vector3.Distance (closestPlanet.transform.position, playerPos)) {
					closestPlanet = planets[i];
				}
					
				if (distanceToPlanet > removePlanetDistance) {
					GameObject planet = planets [i];
					planets.RemoveAt (i);
					GameObject.Destroy (planet);
				}
			}
			nextTimeToSearch = Time.time + 0.5f;
		}
			
		return closestPlanet;
	}

	// sets the total distance traveled from earth
	public void setTotalDistance(float distance) {
		if ((distance - offset) > maxDistance) {
			maxDistance = Mathf.Abs(distance - offset);
		}

		currentDistance = Mathf.Abs(distance - offset);
	}

	// Calculates the amount of money to be awarded based on the total distance upon crashing
	float distanceToDollars() {
		return (float)System.Math.Round((getMaxDistance () * 10), 2);
	}

	// shows reset text, with distance traveled and money earned
	void showReset() {
		string tempText = "Total Distance: " + getMaxDistance () + "km\n";
		float tempReward = distanceToDollars ();

		for (int i = 0; i < rewardDistances.Length; i++) {
			if (getMaxDistance () >= rewardDistances [i] && !rewardReached [i]) {
				rewardReached [i] = true;
				tempReward += rewardAmounts [i];
				tempText += "Congratulations on reaching " + rewardDistances [i] + "km! Bonus: $" + rewardAmounts [i] + "\n";
				if (i == rewardDistances.Length - 1) {
					tempText += winText + '\n';
				}
			}
		}
			
		resetText.text = tempText + "Payout: $" + tempReward + "\n\nPress Enter to Continue";
		playerStats.addAmount (tempReward);
		resetGUI.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (canReset) {
			if (!resetGUI.activeSelf) {
				showReset ();
			}
			if (Input.GetKeyDown (KeyCode.Return)) {
				reset ();
			}
		}
	}
}
