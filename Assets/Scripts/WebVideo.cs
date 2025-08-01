using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

public class WebVideo : MonoBehaviour
{
    [SerializeField] string videoFileName;

    //public bool destroyAfterPlay;
    //public float frameCount;
    //public GameObject videoGO;

    private VideoPlayer videoPlayer;

    //private bool isPrepared;

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
            //CalculateVideoLength();
            //Debug.Log("framecount is " + frameCount);
            //if (destroyAfterPlay)
            //{
            //    StartCoroutine(Frames());
            //}
        }
    }

    //void CalculateVideoLength()
    //{
    //    // Get frame count and frame rate from the VideoPlayer. 
    //    ulong fC = videoPlayer.frameCount;
    //    float frameRate = videoPlayer.frameRate; ;

    //    frameCount = fC / frameRate;
    //}

    //IEnumerator Frames()
    //{
    //    if (frameCount >= 0)
    //    {
    //        yield return new WaitForSeconds(frameCount);
    //        DestroyVideoGO();
    //    }
    //}

    //void DestroyVideoGO()
    //{
    //    Destroy(videoGO);
    //}
}
