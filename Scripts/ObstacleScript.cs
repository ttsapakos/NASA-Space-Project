using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	public float damage;
	public float maxLifeTime;

	private ParticleSystem explosionEffect;
	private float timeToDestroy;

	// Use this for initialization
	void Start () {
		explosionEffect = GetComponentInChildren<ParticleSystem> ();
		init ();
		timeToDestroy = Time.time + maxLifeTime;
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

	void checkLifeTime () {
		if (Time.time > timeToDestroy) {
			explosionEffect.Play ();
			GameObject.Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		checkLifeTime ();
	}
}
