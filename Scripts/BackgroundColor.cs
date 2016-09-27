using UnityEngine;
using System.Collections;

public class BackgroundColor : MonoBehaviour {

	public GameController gc;
	public Renderer rend;
	public float atmosphereHeight;

	public float colorOffset;

	private Color initialColor;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		initialColor = rend.material.color;
	}

	void updateBackgroundColor() {
		float fadeFactor = (gc.getCurrectDistance() - colorOffset) / atmosphereHeight;
		rend.material.color = new Color (initialColor.r - fadeFactor, initialColor.g - fadeFactor, initialColor.b - fadeFactor, initialColor.a - fadeFactor);
	}
	
	// Update is called once per frame
	void Update () {
		updateBackgroundColor ();
	}
}
