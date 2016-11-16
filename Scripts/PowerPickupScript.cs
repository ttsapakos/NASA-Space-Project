using UnityEngine;
using System.Collections;

public class PowerPickupScript : MonoBehaviour {

	public float powerToAdd;
	public float maxLifeTime;

	private PlayerController playerController;
	private float timeToDestroy;

	// Use this for initialization
	void Start () {
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		timeToDestroy = Time.time + maxLifeTime;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag ("Player") || col.gameObject.CompareTag ("Magnet") || col.gameObject.CompareTag ("Planet")) {
			this.GetComponent<SpriteRenderer> ().enabled = false;
			this.GetComponent<BoxCollider2D> ().enabled = false;
			if (col.gameObject.CompareTag ("Player") || col.gameObject.CompareTag ("Magnet")) {
				playerController.addPower (powerToAdd);
			}
		}
	}

	void checkLifeTime () {
		if (Time.time > timeToDestroy) {
			GameObject.Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		checkLifeTime ();
	}
}
