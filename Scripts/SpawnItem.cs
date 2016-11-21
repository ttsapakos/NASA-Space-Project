using UnityEngine;
using System.Collections;

public class SpawnItem : MonoBehaviour {

	public GameObject item;
	// the factor by which the spawned object is offset from the player
	public float spawnDistance;
	// the speed past which the player must be movind in order for items to spawn
	public float spawnSpeedThreshold;
	// How long of a delay there is between items spawning
	public float spawnFrequency;
	// The radius around the spawn point within which items will spawn
	public float spawnRadius;
	// the maximum multiplier of size a spawned item can be
	public float maxSpawnSizeMultiplier;

	private float nextTimeToSearch = 0;
	private GameObject player;
	private GameObject closestPlanet;
	private GameController gc;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void Spawn () {
		if (nextTimeToSearch <= Time.time) {
			Vector3 playerVelocity = player.GetComponent<Rigidbody2D> ().velocity;
			float playerVeloMagnitude = playerVelocity.magnitude;

			closestPlanet = gc.getClosestPlanet (this.gameObject.transform.position);
			if (playerVeloMagnitude > spawnSpeedThreshold && !closestPlanet.CompareTag ("Earth")) {
				Vector3 playerVeloNormalized = playerVelocity.normalized;
				float x = player.transform.position.x + (playerVeloNormalized.x * spawnDistance) + Random.Range (-1 * spawnRadius, spawnRadius);
				float y = player.transform.position.y + (playerVeloNormalized.y * spawnDistance) + Random.Range (-1 * spawnRadius, spawnRadius);;
				Vector3 newItemPos = new Vector3 (x, y, 0);

				GameObject newItem = Instantiate (item);

				float widthMultiplier = Random.Range (1, maxSpawnSizeMultiplier);
				newItem.transform.localScale = new Vector3 (newItem.transform.localScale.x * widthMultiplier, newItem.transform.localScale.y * widthMultiplier, 1);
				newItem.transform.position = newItemPos;
				nextTimeToSearch = Time.time + spawnFrequency;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		Spawn ();
	}
}
