using UnityEngine;
using System.Collections;

public class BuyPart : MonoBehaviour {

	public PlayerStats playerStats;

	public void buyThruster (GameObject statsObject) {
		if (playerStats.getTotalMoney () >= statsObject.GetComponent<PartStats> ().cost) {
			playerStats.addAmount (-1 * statsObject.GetComponent<PartStats> ().cost);
			GameObject.FindGameObjectWithTag ("Thruster").GetComponent<SpriteRenderer> ().sprite = statsObject.GetComponent<PartStats> ().image;
			GameObject.FindGameObjectWithTag ("Thruster").GetComponent<ThrusterScript> ().thrust = statsObject.GetComponent<PartStats> ().modifier;
		}
	}

	public void buyFuelPod (GameObject statsObject) {
		if (playerStats.getTotalMoney () >= statsObject.GetComponent<PartStats> ().cost) {
			playerStats.addAmount (-1 * statsObject.GetComponent<PartStats> ().cost);
			GameObject.FindGameObjectWithTag ("FuelPod").GetComponent<SpriteRenderer> ().sprite = statsObject.GetComponent<PartStats> ().image;
			GameObject.FindGameObjectWithTag ("FuelPod").GetComponent<FuelPodScript> ().maxFuel = statsObject.GetComponent<PartStats> ().modifier;
		}
	}

	public void buyHull (GameObject statsObject) {
		if (playerStats.getTotalMoney () >= statsObject.GetComponent<PartStats> ().cost) {
			playerStats.addAmount (-1 * statsObject.GetComponent<PartStats> ().cost);
			GameObject.FindGameObjectWithTag ("Hull").GetComponent<SpriteRenderer> ().sprite = statsObject.GetComponent<PartStats> ().image;
			GameObject.FindGameObjectWithTag ("Hull").GetComponent<HullScript> ().maxHealth = statsObject.GetComponent<PartStats> ().modifier;
		}
	}

	public void buyNoseCone (GameObject statsObject) {
		if (playerStats.getTotalMoney () >= statsObject.GetComponent<PartStats> ().cost) {
			playerStats.addAmount (-1 * statsObject.GetComponent<PartStats> ().cost);
			GameObject.FindGameObjectWithTag ("NoseCone").GetComponent<SpriteRenderer> ().sprite = statsObject.GetComponent<PartStats> ().image;
			GameObject.FindGameObjectWithTag ("NoseCone").GetComponent<NoseConeScript> ().maxOxygen = statsObject.GetComponent<PartStats> ().modifier;
		}
	}

	public void buyWings (GameObject statsObject) {
		if (playerStats.getTotalMoney () >= statsObject.GetComponent<PartStats> ().cost) {
			playerStats.addAmount (-1 * statsObject.GetComponent<PartStats> ().cost);
			GameObject.FindGameObjectWithTag ("Wings").GetComponent<SpriteRenderer> ().sprite = statsObject.GetComponent<PartStats> ().image;
			GameObject.FindGameObjectWithTag ("Wings").GetComponent<WingsScript> ().rotateMultiplier = statsObject.GetComponent<PartStats> ().modifier;
		}
	}

}
