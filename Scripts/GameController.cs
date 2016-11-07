using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	// Reset text gui object
	public GameObject resetGUI;
	// the distance at which the planet can be removed
	public float removePlanetDistance;

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

	// Use this for initialization
	void Start () {
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		playerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();
		earth = GameObject.FindGameObjectWithTag ("Earth");
		resetText = resetGUI.GetComponent<Text> ();
		planets = new List<GameObject> ();
		store = GameObject.FindGameObjectWithTag ("Store");
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

	void reset() {
		init ();
		store.SetActive (true);
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
