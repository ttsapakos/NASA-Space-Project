using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	// the starting money
	public float startingFunds;
	// The player's total money
	private float totalMoney;

	// Use this for initialization
	void Start () {
		totalMoney = startingFunds;
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
