﻿using UnityEngine;
using System.Collections;

public class CategoryButton : MonoBehaviour {

	public void togglePanel (GameObject panel) {
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("CategoryPanel")) {
			go.SetActive (false);
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("PartPanel")) {
			go.SetActive (false);
		}
		panel.SetActive (!panel.activeSelf);
	}
}
