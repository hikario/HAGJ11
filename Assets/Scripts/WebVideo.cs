using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

public class WebVideo : MonoBehaviour
{
    [SerializeField] string videoFileName;

    private VideoPlayer videoPlayer;

    void Awake()
    {
        
        videoPlayer = GetComponent<VideoPlayer>();
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;
        PlayVideo();
    }

    public void PlayVideo()
    {
        if (videoPlayer)
        {
            videoPlayer.Play();
        }
    }
}
