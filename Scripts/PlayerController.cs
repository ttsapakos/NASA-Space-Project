using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameController gc;
	public float minZoom;
	public float maxZoom;
	public float smoothSpeed;
	public float fuelDecayRate;
	public float powerDecayRate;
	public float gravityMultiplier;

	private Rigidbody2D rb;
	private GameObject closestPlanet;
	private ParticleSystem explosionEffect;
	private ParticleSystem thrusterEffect;
	private GameObject Earth;

	private GameObject thrusterObject;
	private GameObject wingsObject;
	private GameObject hullObject;
	private GameObject noseConeObject;
	private GameObject fuelPodObject;

	private bool thruster;
	private bool leftGround;
	private bool rotateCCW;
	private bool rotateCW;
	private bool canMove;
	private float thrust;
	private float rotateMultiplier;
	private float maxPower;
	private float currentPower;
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

	public void addHealth (float h) {
		currentHealth += h;
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}

		if (currentHealth <= 0) {
			stop ();
		}
	}

	public void addFuel (float f) {
		currentFuel += f;
		if (currentFuel > maxFuel) {
			currentFuel = maxFuel;
		}
	}

	public void addPower (float p) {
		currentPower += p;
		if (currentPower > maxPower) {
			currentPower = maxPower;
		}
	}

	public float getCurrentPower() {
		return currentPower;
	}

	// can the player move and have they left the ground?
	public bool isActive () {
		return this.canMove && this.leftGround;
	}
		
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		explosionEffect = GameObject.FindGameObjectWithTag ("Explosion").GetComponent<ParticleSystem> ();
		thrusterEffect = GameObject.FindGameObjectWithTag ("ThrusterEffect").GetComponent<ParticleSystem> ();
		Earth = gc.getClosestPlanet (transform.position);
		canMove = true;
		init ();
	}

	// initialize the player for new round
	void init() {
		thruster = false;
		leftGround = false;
		rotateCW = false;
		rotateCCW = false;

		thrusterObject = GameObject.FindGameObjectWithTag ("Thruster");
		wingsObject = GameObject.FindGameObjectWithTag ("Wings");
		hullObject = GameObject.FindGameObjectWithTag ("Hull");
		noseConeObject = GameObject.FindGameObjectWithTag ("NoseCone");
		fuelPodObject = GameObject.FindGameObjectWithTag ("FuelPod");

		// Reset these as they may have been changed in the store(?)
		thrust = GetComponentInChildren<ThrusterScript> ().thrust;
		rotateMultiplier = GetComponentInChildren<WingsScript> ().rotateMultiplier;
		maxHealth = GetComponentInChildren<HullScript> ().maxHealth;
		maxPower = GetComponentInChildren<NoseConeScript> ().maxPower;
		maxFuel = GetComponentInChildren<FuelPodScript> ().maxFuel;

		closestPlanet = Earth;
		currentPower = maxPower;
		currentFuel = maxFuel;
		currentHealth = maxHealth;
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		explosionEffect.Clear ();
		thrusterEffect.Clear ();
		gc.setOffset (Vector3.Distance (Earth.transform.position, transform.position));
		showParts ();
	}

	// reset the player for a new round
	public void reset() {
		init ();
	}

	// public function to allow the store to dictate when the player can move
	public void letMove () {
		this.canMove = true;
		init ();
	}

	// apply gravitational force
	void applyGravity () {
		// The x and y splif values for gravity
		Vector3 gravityDirection = (closestPlanet.transform.position - transform.position).normalized;
		// the force of the gravity based on the distance from the planet
		float distanceForce = closestPlanet.transform.localScale.x / Vector3.Distance (closestPlanet.transform.position, transform.position);
		// total gravity force = direction (x,y), the size of the planet, the multiplier, and the force divided by distance
		Vector3 gravityForce = gravityDirection * closestPlanet.transform.localScale.x * gravityMultiplier * distanceForce;
		rb.AddForce(gravityForce);
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
			float xThrust = Mathf.Sin (transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * thrust * -1;
			float yThrust = Mathf.Cos (transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * thrust;
			Vector2 force = new Vector3 (xThrust, yThrust);
			rb.AddForce (force);
			currentFuel -= fuelDecayRate;
			thrusterEffect.Play ();
		} else {
			thrusterEffect.Stop ();
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
		if ((col.gameObject.CompareTag ("Planet") || col.gameObject.CompareTag ("Earth")) && leftGround) {
			stop ();
		}
	}

	public void decayPower () {
		if (this.leftGround) {
			currentPower -= powerDecayRate;
		}
	}

	void showParts () {
		thrusterObject.GetComponent<SpriteRenderer> ().enabled = true;
		wingsObject.GetComponent<SpriteRenderer> ().enabled = true;
		hullObject.GetComponent<SpriteRenderer> ().enabled = true;
		noseConeObject.GetComponent<SpriteRenderer> ().enabled = true;
		fuelPodObject.GetComponent<SpriteRenderer> ().enabled = true;
	}

	// hide the ship parts
	void hideParts () {
		thrusterObject.GetComponent<SpriteRenderer> ().enabled = false;
		wingsObject.GetComponent<SpriteRenderer> ().enabled = false;
		hullObject.GetComponent<SpriteRenderer> ().enabled = false;
		noseConeObject.GetComponent<SpriteRenderer> ().enabled = false;
		fuelPodObject.GetComponent<SpriteRenderer> ().enabled = false;
	}

	// Stop the gameplay, disable user input, and play the explosion effect
	void stop() {
		currentHealth = 0;
		canMove = false;
		rb.velocity = Vector3.zero;
		rb.freezeRotation = true;
		thrusterEffect.Stop ();
		explosionEffect.Play ();
		gc.setCanReset (true);
		hideParts ();
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
		}
		zoom ();
	}
}
