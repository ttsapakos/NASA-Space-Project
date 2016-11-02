using UnityEngine;
using System.Collections;

public class RotatePlanet : MonoBehaviour {

	public float rotateFactor = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (0, 0, rotateFactor));
	}
}
