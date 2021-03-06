﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadarScript : MonoBehaviour {

	private GameController gc;
	private PlayerController pc;
	private Vector3 playerPos;
	private GameObject closestPlanet;
	private Text text;
	private LineRenderer lr;
	private float distanceToClosestPlanet;
	private float nextTimeToSearch = 0;
	private bool radarActive;

	public float getDistanceToClosestPlanet () {
		return distanceToClosestPlanet;
	}

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		text = GetComponent<Text> ();
		lr = GetComponent<LineRenderer> ();
		lr.SetWidth (0, 0);
		text.enabled = false;
	}
		
	// Update the radar to point to the closest planet
	void updateRadar () {
		if (nextTimeToSearch <= Time.time) {
			distanceToClosestPlanet = Vector3.Distance (closestPlanet.transform.position, playerPos) - (closestPlanet.transform.localScale.x * closestPlanet.GetComponent<CircleCollider2D> ().radius);
			text.text = distanceToClosestPlanet + "";

			Vector3 difference = (closestPlanet.transform.position - playerPos);
			difference.Normalize ();


			nextTimeToSearch = Time.time + 0.1f;
		}

		Vector3[] positions = new Vector3[2];
		positions [0] = playerPos;
		positions [1] = closestPlanet.transform.position;
		lr.SetPositions (positions);
	}

	//detect if the radar button is being pressed
	void updateKeyPress () {
		if (Input.GetKeyDown (KeyCode.R)) {
			radarActive = true;
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			radarActive = false;
		}
	}


	// Update is called once per frame
	void Update () {
		playerPos = pc.transform.position;
		closestPlanet = gc.getClosestPlanet (playerPos);
		updateRadar ();

		updateKeyPress ();
		if (radarActive && pc.isActive () && pc.getCurrentPower () > 0) {
			lr.SetWidth (0.25f, 0.25f);
			text.enabled = true;
			pc.decayPower ();
		} else {
			lr.SetWidth (0, 0);
			text.enabled = false;
		}
	}
}
