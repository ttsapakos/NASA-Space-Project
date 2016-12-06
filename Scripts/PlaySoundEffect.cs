using UnityEngine;
using System.Collections;

public class PlaySoundEffect : MonoBehaviour {

	AudioSource source;

	public void playEffect(AudioClip effect) {
		source = this.GetComponent<AudioSource> ();
		source.PlayOneShot (effect, 0.5f);
	}

}
