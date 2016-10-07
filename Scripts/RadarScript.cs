using UnityEngine;
using System.Collections;

public class RadarScript : MonoBehaviour {

	public Material lineMat;

	private GameController gc;
	private PlayerController pc;
	private Vector3 playerPos;
	private Vector3 planetPos;
	private LineRenderer lr;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		lr = GetComponent<LineRenderer> ();
		lr.material = lineMat;
		lr.SetWidth (0.25f, 0.25f);
	}
		
	void drawLine () {
		Vector3[] positions = new Vector3[2];
		positions [0] = playerPos;
		positions [1] = planetPos;
		lr.SetPositions (positions);
	}

	// Update is called once per frame
	void Update () {
		playerPos = pc.transform.position;
		planetPos = gc.getClosestPlanet (playerPos).transform.position;
		drawLine ();
	}
}
