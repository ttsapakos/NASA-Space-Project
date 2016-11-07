using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoneyDisplay : MonoBehaviour {

	private PlayerStats playerStats;
	private Text moneyText;

	// Use this for initialization
	void Start () {
		playerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();
		moneyText = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = "$" + playerStats.getTotalMoney ().ToString ();
	}
}
