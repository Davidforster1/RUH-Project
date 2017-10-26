using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class streamVideo : MonoBehaviour {

    public RawImage image;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    // Use this for initialization
    void Start () {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
	}

    IEnumerator playVideo()
    {

        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //We want to play from video clip not from url

        videoPlayer.source = VideoSource.VideoClip;

        // Video clip from Url
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";
       // videoPlayer.url = "Assets/StreamingAssets/diabetesVideo.mp4";


        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Play Video
        videoPlayer.Play();
    }
}
