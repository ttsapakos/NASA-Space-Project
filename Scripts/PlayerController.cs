using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameController gc;
	public float thrust;
	public float rotateMultiplier;
	public float minZoom;
	public float maxZoom;
	public float smoothSpeed;

	private Rigidbody2D rb;
	private GameObject closestPlanet;
	private ParticleSystem explosionEffect;
	private GameObject Earth;
	private bool thruster;
	private bool leftGround;
	private bool rotateCCW;
	private bool rotateCW;
	private bool canMove;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		explosionEffect = GetComponentInChildren<ParticleSystem> ();
		explosionEffect.Pause ();
		thruster = false;
		leftGround = false;
		rotateCW = false;
		rotateCCW = false;
		canMove = true;
		closestPlanet = gc.getClosestPlanet (transform.position);
		Earth = closestPlanet;
	}

	void applyGravityAndAirResistance () {
		rb.AddForce((closestPlanet.transform.position - transform.position).normalized * closestPlanet.transform.localScale.x / 4);
	}
		
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

		if (thruster) {
			// Applies force to the rocket at the right angle
			float xThrust = Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * thrust * -1;
			float yThrust = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * thrust;
			Vector2 force = new Vector3 (xThrust, yThrust);
			rb.AddForce (force);
		}
	}

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


	void zoomOut () {
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


	void OnCollisionEnter2D(Collision2D col) {
		if (leftGround) {
			canMove = false;
			rb.velocity = Vector3.zero;
			this.GetComponent<SpriteRenderer> ().enabled = false;
			explosionEffect.Play ();
		}
	}

	// Update is called once per frame
	void Update () {
		closestPlanet = gc.getClosestPlanet (transform.position);
		gc.setTotalDistance(Vector3.Distance(Earth.transform.position, transform.position));
		applyGravityAndAirResistance ();
		updateThruster ();
		updateRotation ();
		zoomOut ();
	}
}
