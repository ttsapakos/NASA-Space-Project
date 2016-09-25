using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D rb;
	public float thrust;
	public float rotateMultiplier;

	private bool thruster;
	private bool leftGround;
	private bool rotateCCW;
	private bool rotateCW;
	private Transform planet;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		thruster = false;
		leftGround = false;
		rotateCW = false;
		rotateCCW = false;
		planet = GameObject.FindGameObjectWithTag ("Earth").transform;
	}

	void applyGravityAndAirResistance () {
		rb.AddForce((planet.position - transform.position).normalized * planet.localScale.x / 4);
	}
		
	void updateThruster () {
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

	void OnCollisionEnter2D(Collision2D col) {
		if (leftGround) {
			Debug.Log ("collision detected");
		}
	}

	// Update is called once per frame
	void Update () {
		applyGravityAndAirResistance ();
		updateThruster ();
		updateRotation ();
	}
}
