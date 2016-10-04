using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	public float damage;
	private ParticleSystem explosionEffect;

	// Use this for initialization
	void Start () {
		explosionEffect = GetComponentInChildren<ParticleSystem> ();
		init ();
	}

	void init() {
		this.GetComponent<SpriteRenderer> ().enabled = true;
		this.GetComponent<CircleCollider2D> ().enabled = true;
		explosionEffect.Clear ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		this.GetComponent<SpriteRenderer> ().enabled = false;
		this.GetComponent<CircleCollider2D> ().enabled = false;
		explosionEffect.Play ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
