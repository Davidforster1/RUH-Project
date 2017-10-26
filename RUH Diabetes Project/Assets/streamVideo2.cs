﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class streamVideo2 : MonoBehaviour {

    //Raw Image to Show Video Images [Assign from the Editor]
    public RawImage image;
    //Video To Play [Assign from the Editor]
    public VideoClip videoToPlay;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    //Audio
    private AudioSource audioSource;

    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());   
    }

    IEnumerator playVideo()
    {
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        
        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        videoPlayer.isLooping = true;

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.VideoClip;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }


        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Play Video
          videoPlayer.Play();

        //Play Sound
          audioSource.Play();


        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
    }

    // Update is called once per frame
    void Update () {

    }
}