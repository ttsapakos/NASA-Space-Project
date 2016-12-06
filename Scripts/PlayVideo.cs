using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

	public MovieTexture movie;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		audio = GetComponent<AudioSource> ();
		audio.clip = movie.audioClip;
		movie.Play ();
		audio.Play ();
		StartCoroutine("waitForMovieEnd");
	}

	IEnumerator waitForMovieEnd()
	{

		while(movie.isPlaying) // while the movie is playing
		{
			yield return new WaitForEndOfFrame();
		}
		// after movie is not playing / has stopped.
		onMovieEnded();
	}

	void onMovieEnded()
	{
		SceneManager.LoadScene ("GameScene");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			movie.Stop ();
			audio.Stop ();
			onMovieEnded();
		}
	}
}
