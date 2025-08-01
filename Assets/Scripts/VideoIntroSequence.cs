using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;


public class VideoIntroSequence : MonoBehaviour
{
    [SerializeField] string loopVideoName;
    [SerializeField] string endVideoName;

    private VideoPlayer videoPlayer;

    void Start()
    {

        videoPlayer = GetComponent<VideoPlayer>();
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, loopVideoName);
        videoPlayer.url = videoPath;
        videoPlayer.Play();
    }

    void PlayEnd()
    {
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, endVideoName);
        videoPlayer.url = videoPath;
        videoPlayer.Play();
    }
}
