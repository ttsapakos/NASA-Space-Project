using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetTextEquipped : MonoBehaviour {

	public void setTextEquipped() {
		this.gameObject.GetComponentInChildren<Text> ().text = "EQUIP";
	}

}
