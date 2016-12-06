using UnityEngine;
using System.Collections;

public class MagnetStats : MonoBehaviour {
	public float cost;
	public float radius;
	public Color color;
	public float lifeTime;
	public float rate;
	public bool purchased = false;

	void Start() {
		purchased = false;
	}
}
