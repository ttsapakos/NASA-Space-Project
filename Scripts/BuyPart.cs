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
			GameObject.FindGameObjectWithTag ("NoseCone").GetComponent<NoseConeScript> ().maxPower = statsObject.GetComponent<PartStats> ().modifier;
		}
	}

	public void buyWings (GameObject statsObject) {
		if (playerStats.getTotalMoney () >= statsObject.GetComponent<PartStats> ().cost) {
			playerStats.addAmount (-1 * statsObject.GetComponent<PartStats> ().cost);
			GameObject.FindGameObjectWithTag ("Wings").GetComponent<SpriteRenderer> ().sprite = statsObject.GetComponent<PartStats> ().image;
			GameObject.FindGameObjectWithTag ("Wings").GetComponent<WingsScript> ().rotateMultiplier = statsObject.GetComponent<PartStats> ().modifier;
		}
	}

	public void buyMagnet (GameObject statsObject) {
		if (playerStats.getTotalMoney () >= statsObject.GetComponent<MagnetStats> ().cost) {
			playerStats.addAmount (-1 * statsObject.GetComponent<MagnetStats> ().cost);
			GameObject magnet = GameObject.FindGameObjectWithTag ("Magnet");
			ParticleSystem.ShapeModule sm = magnet.GetComponent<ParticleSystem> ().shape;
			sm.radius = statsObject.GetComponent<MagnetStats> ().radius;
			magnet.GetComponent<ParticleSystem> ().startColor    = statsObject.GetComponent<MagnetStats> ().color;
			magnet.GetComponent<ParticleSystem> ().startLifetime = statsObject.GetComponent<MagnetStats> ().lifeTime;
			ParticleSystem.EmissionModule em = magnet.GetComponent<ParticleSystem> ().emission;
			em.rate = statsObject.GetComponent<MagnetStats> ().rate;
			magnet.GetComponent<CircleCollider2D> ().radius = statsObject.GetComponent<MagnetStats> ().radius;
		}
	}

}
