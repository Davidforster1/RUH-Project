using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class playVideo : MonoBehaviour {

    [SerializeField]
    private string videoName;

    private string movie; //= "TD1AppWelcomeVideo002.mp4";  // The file's name which resides in the StreamingAssets folder

	// Use this for initialization
	void Start () {
        movie = videoName;
        StartCoroutine(streamVideo(movie));     // play the movie 
    }

    private IEnumerator streamVideo(string video)
    {
        Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.Fill); //Changing the controls and scaling of the movie
        yield return new WaitForEndOfFrame();
        Debug.Log("The Video playback is now completed.");  // debugging for the console
    }
}
