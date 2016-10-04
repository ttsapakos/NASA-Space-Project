using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	private float totalMoney;

	// Use this for initialization
	void Start () {
		totalMoney = 0;
	}

	public float getTotalMoney() {
		return totalMoney;
	}

	public void addAmount(float f) {
		totalMoney += f;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
