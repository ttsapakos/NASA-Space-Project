using UnityEngine;
using System.Collections;

public class RotatePlanet : MonoBehaviour {

	public float maxRotateFactor = 1.0f;

	private float rotateFactor;

	// Use this for initialization
	void Start () {
		rotateFactor = Random.Range (-1 * maxRotateFactor, maxRotateFactor);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (0, 0, rotateFactor));
	}
}
