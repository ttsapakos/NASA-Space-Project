using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D rb;
	public float thrust;

	private bool thruster;
	private bool canCrash;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		thruster = false;
		canCrash = false;
	}

	void updateThruster () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			thruster = true;
			canCrash = true;
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			thruster = false;
		}

		if (thruster) {
			rb.AddForce (new Vector2 (0, thrust));
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (canCrash) {
			Debug.Log ("collision detected");
		}
	}

	// Update is called once per frame
	void Update () {
		updateThruster ();
	}
}
