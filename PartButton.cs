using UnityEngine;
using System.Collections;

public class PartButton : MonoBehaviour {

	public void togglePanel (GameObject panel) {
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("PartPanel")) {
			go.SetActive (false);
		}
		panel.SetActive (!panel.activeSelf);
	}
}
