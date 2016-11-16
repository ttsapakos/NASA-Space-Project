using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	public float damage;
	public float maxLifeTime;

	private ParticleSystem explosionEffect;
	private PlayerController playerController;
	private float timeToDestroy;

	// Use this for initialization
	void Start () {
		explosionEffect = GetComponentInChildren<ParticleSystem> ();
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		timeToDestroy = Time.time + maxLifeTime;
		this.GetComponent<SpriteRenderer> ().enabled = true;
		this.GetComponent<CircleCollider2D> ().enabled = true;
		explosionEffect.Clear ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Planet") || col.gameObject.CompareTag("Earth")) {
		this.GetComponent<SpriteRenderer> ().enabled = false;
			this.GetComponent<CircleCollider2D> ().enabled = false;
			explosionEffect.Play ();
			if (col.gameObject.CompareTag ("Player")) {
				playerController.addHealth (-1 * damage);
			}
		}
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
