using UnityEngine;
using System.Collections;

public class PlanetSpawner : MonoBehaviour {

	// generic planet which we can spawn
	public GameObject planetPrefab;
	// Distances between which a planet can spawn
	public float minSpawnDistance;
	public float maxSpawnDistance;
	// Boundaries between which the planet width can be
	public float minWidth;
	public float maxWidth;
	// the factor by which the spawned planets are moving relative to the player
	public float veloMultiplier;

	// the distance the player must be from the closest planet in order to spawn a new planet
	private float spawnDistance;
	private GameController gc;
	private GameObject player;
	private RadarScript radar;
	private float nextTimeToRun = 0;
	private float lastDistance;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		radar = player.GetComponentInChildren<RadarScript> ();
		spawnDistance = Random.Range (minSpawnDistance, maxSpawnDistance);
		lastDistance = radar.getDistanceToClosestPlanet ();
	}

	// returns true if the player is far enough from the closest planet to spawn a new one and is currently moving away from that planet
	bool checkShouldSpawnNewPlanet () {
		if (nextTimeToRun <= Time.time) {
			nextTimeToRun = Time.time + 1.0f;

			// check if we're far enough away from the closest planet to spawn a new one, as well getting farther away from that planet
			if ((radar.getDistanceToClosestPlanet () >= spawnDistance) && (radar.getDistanceToClosestPlanet () > lastDistance)) {
				return true;
				lastDistance = Mathf.Infinity;
			} else {
				lastDistance = radar.getDistanceToClosestPlanet ();
				return false;
			}
		}

		return false;
	}

	// spawns planet
	void spawnPlanet () {
		Vector3 playerVelo = player.GetComponent<Rigidbody2D> ().velocity.normalized;
		Vector3 newPlanetPos = new Vector3 (player.transform.position.x + (playerVelo.x * spawnDistance), player.transform.position.y + (playerVelo.y * spawnDistance), 0);

		GameObject newPlanet = Instantiate (planetPrefab);
		// set the new position
		newPlanet.transform.position = newPlanetPos;
		// set the new width
		float newWidth = Random.Range (minWidth, maxWidth);
		newPlanet.transform.localScale = new Vector3 (newWidth, newWidth, 1);
		//set the new velocity
		Vector3 newVelo = new Vector3(playerVelo.x * veloMultiplier, playerVelo.y * veloMultiplier, 0);
		newPlanet.GetComponent<Rigidbody2D> ().velocity = newVelo;

		gc.addPlanet (newPlanet);
	}
	
	// Update is called once per frame
	void Update () {
		if (checkShouldSpawnNewPlanet ()) {
			spawnPlanet ();
			spawnDistance = Random.Range (minSpawnDistance, maxSpawnDistance);
		}
	}
}
