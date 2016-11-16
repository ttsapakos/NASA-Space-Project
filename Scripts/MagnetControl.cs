using UnityEngine;
using System.Collections;

public class MagnetControl : MonoBehaviour {

	private bool magnetActive = false;
	private ParticleSystem ps;
	private PlayerController pc;
	private Collider2D coll;

	// Use this for initialization
	void Start () {
		ps = this.gameObject.GetComponent<ParticleSystem> ();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		ps.Stop ();
		coll = this.gameObject.GetComponent<CircleCollider2D> ();
		coll.enabled = false;
	}

	void updateKeyPress () {
		if (Input.GetKeyDown (KeyCode.T)) {
			magnetActive = true;
		}

		if (Input.GetKeyUp (KeyCode.T)) {
			magnetActive = false;
		}
	}

	void activateMagnet () {
		if (!ps.isPlaying) {
			ps.Play ();
			coll.enabled = true;
		}
	}

	void deactivateMagnet () {
		if (ps.isPlaying) {
			ps.Stop ();
			ps.Clear ();
			coll.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateKeyPress ();
		if (magnetActive && pc.getCurrentPower () > 0 && pc.isActive ()) {
			activateMagnet ();
			pc.decayPower ();
		} else {
			deactivateMagnet ();
		}
	}
}
