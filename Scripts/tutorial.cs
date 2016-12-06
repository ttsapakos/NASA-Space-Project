using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tutorial : MonoBehaviour {

	public Texture [] slides;
	private int currentSlide;
	public Texture showing;

	// Use this for initialization
	void Start () {
		currentSlide = 0;
		GetComponent<RawImage> ().texture = slides [currentSlide];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			currentSlide += 1;
			if (currentSlide < slides.Length) {
				GetComponent<RawImage> ().texture = slides [currentSlide];
			} else {
				this.gameObject.SetActive (false);
			}
		}
	}
}
