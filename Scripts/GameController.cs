using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private GameObject[] planets;
	private float maxDistance;
	private float currentDistance;

	// Use this for initialization
	void Start () {
		planets = GameObject.FindGameObjectsWithTag ("Planet");
		maxDistance = 0;
	}

	public float getMaxDistance() {
		return maxDistance;
	}

	public float getCurrectDistance() {
		return currentDistance;
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
		if (distance > maxDistance) {
			maxDistance = distance;
		}

		currentDistance = distance;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
