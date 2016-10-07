using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameController gc;
	public float minZoom;
	public float maxZoom;
	public float smoothSpeed;
	public float fuelDecayRate;
	public float oxygenDecayRate;


	private Rigidbody2D rb;
	private GameObject closestPlanet;
	private ParticleSystem explosionEffect;
	private GameObject Earth;
	private bool thruster;
	private bool leftGround;
	private bool rotateCCW;
	private bool rotateCW;
	private bool canMove;
	private float thrust;
	private float rotateMultiplier;
	private float maxOxygen;
	private float currentOxygen;
	private float maxFuel;
	private float currentFuel;
	private float maxHealth;
	private float currentHealth;

	// Getters for current levels of resources
	public float getCurrentFuel() {
		return currentFuel;
	}

	public float getCurrentHealth() {
		return currentHealth;
	}

	public float getCurrentOxygen() {
		return currentOxygen;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		explosionEffect = GetComponentInChildren<ParticleSystem> ();
		init ();
	}

	// initialize the player for new round
	void init() {
		thruster = false;
		leftGround = false;
		rotateCW = false;
		rotateCCW = false;
		canMove = true;
		this.GetComponent<SpriteRenderer> ().enabled = true;

		// Reset these as they may have been changed in the store(?)
		thrust = GetComponentInChildren<ThrusterScript> ().thrust;
		rotateMultiplier = GetComponentInChildren<WingsScript> ().rotateMultiplier;
		maxHealth = GetComponentInChildren<HullScript> ().maxHealth;
		maxOxygen = GetComponentInChildren<NoseConeScript> ().maxOxygen;
		maxFuel = GetComponentInChildren<FuelPodScript> ().maxFuel;

		currentOxygen = maxOxygen;
		currentFuel = maxFuel;
		currentHealth = maxHealth;
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		closestPlanet = gc.getClosestPlanet (transform.position);
		explosionEffect.Clear ();
		Earth = closestPlanet;
		gc.setOffset (Vector3.Distance (Earth.transform.position, transform.position));
	}

	// reset the player for a new round
	public void reset() {
		init ();
	}

	// apply gravitational force
	void applyGravity () {
		rb.AddForce((closestPlanet.transform.position - transform.position).normalized * closestPlanet.transform.localScale.x / 4);
	}

	// update the player's thruster based on input and turning stats
	void updateThruster () {
		if (!canMove)
			return;
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			thruster = true;
			leftGround = true;
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			thruster = false;
		}

		if (thruster && currentFuel > 0) {
			// Applies force to the rocket at the right angle
			float xThrust = Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * thrust * -1;
			float yThrust = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * thrust;
			Vector2 force = new Vector3 (xThrust, yThrust);
			rb.AddForce (force);
			currentFuel -= fuelDecayRate;
		}
	}

	// update the player's rotation based on input and turning stats
	void updateRotation () {
		if (!canMove)
			return;
		
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			rotateCCW = true;
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			rotateCCW = false;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			rotateCW = true;
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			rotateCW = false;
		}

		if (leftGround) {
			if (rotateCW) {
				transform.Rotate (new Vector3 (0, 0, -1 * rotateMultiplier));
			}

			if (rotateCCW) {
				transform.Rotate (new Vector3 (0, 0, 1 * rotateMultiplier));
			}
		}

	}

	// zoom the camera in or out based on velocity
	void zoom () {
		float targetOrtho = rb.velocity.magnitude;

		if (targetOrtho < minZoom) {
			targetOrtho = minZoom;
		}

		if (targetOrtho > maxZoom)
		{
			targetOrtho = maxZoom;
		}

		Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
	}

	// For collisions with objects whose colliders are triggers, i.e. Obstacles
	void OnTriggerEnter2D(Collider2D col) {
		// Double check that we are indeed hitting and obstacle
		if (col.gameObject.CompareTag ("Obstacle")) {
			currentHealth -= col.gameObject.GetComponent<ObstacleScript> ().damage;
		}
		if (leftGround && currentHealth <= 0) {
			stop ();
		}
	}

	// For collisions with objects whose colliders aren't triggers, i.e. Planets
	void OnCollisionEnter2D(Collision2D col) {
		// double check that we are hitting a planet
		if (col.gameObject.CompareTag("Planet") && leftGround) {
			stop ();
		}
	}

	void decayOxygen () {
		currentOxygen -= oxygenDecayRate;
		if (currentOxygen <= 0) {
			stop ();
		}
	}

	// Stop the gameplay, disable user input, and play the explosion effect
	void stop() {
		currentHealth = 0;
		canMove = false;
		rb.velocity = Vector3.zero;
		rb.freezeRotation = true;
		this.GetComponent<SpriteRenderer> ().enabled = false;
		explosionEffect.Play ();
		gc.setCanReset (true);
	}

	// Update is called once per frame
	void Update () {
		// only update if the player hasn't yet crashed
		if (!gc.getCanReset ()) {
			closestPlanet = gc.getClosestPlanet (transform.position);
			gc.setTotalDistance (Vector3.Distance (Earth.transform.position, transform.position));
			updateThruster ();
			updateRotation ();
			applyGravity ();
			decayOxygen ();
		}
		zoom ();
	}
}
