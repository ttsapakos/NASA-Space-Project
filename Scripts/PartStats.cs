using UnityEngine;
using System.Collections;

public class PartStats : MonoBehaviour {
	public float cost;
	public Sprite image;
	public float modifier;
	public bool purchased = false;

	void Start () {
		purchased = false;
	}
}
