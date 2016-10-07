using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	private float maxHealth;
	private float currentHealth;
	private float initialWidth;
	private float currentWidth;

	// Use this for initialization
	void Start () {
		initialWidth = GetComponent<RectTransform> ().localScale.x;
		init ();
	}

	void init() {
		GameObject hull = GameObject.FindGameObjectWithTag ("Hull");
		maxHealth = hull.GetComponent<HullScript> ().maxHealth;
		currentHealth = maxHealth;
	}

	void updateHealth() {
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = GetComponentInParent<PlayerController> ().getCurrentHealth();
		updateHealth ();
		GetComponent<RectTransform> ().localScale = new Vector3(initialWidth * (currentHealth / maxHealth), GetComponent<RectTransform> ().localScale.y, 0);
	}
}
