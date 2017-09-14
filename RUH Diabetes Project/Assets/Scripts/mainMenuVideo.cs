using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuVideo : MonoBehaviour {

    private string movie = "diabetesVideo.mp4";  // The file's name which resides in the StreamingAssets folder

	// Use this for initialization
	void Start () {
        StartCoroutine(streamVideo(movie));     // play the movie 
    }

    private IEnumerator streamVideo(string video)
    {
        Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.Fill); //Changing the controls and scaling of the movie
        yield return new WaitForEndOfFrame();
        Debug.Log("The Video playback is now completed.");  // debugging for the console
        SceneManager.LoadScene("yearSelection");            // loading next scene
    }
}
