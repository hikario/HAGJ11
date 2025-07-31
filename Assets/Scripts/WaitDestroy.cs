using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

public class WaitDestroy : MonoBehaviour
{
    public GameObject nextVideoObject;
    public VideoPlayer thisVideo;
    public VideoPlayer nextVideo;
    public float frameCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        CalculateVideoLength();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Frames());
            
    }

    public void EnableChildren()
    {
        for (int i = 0; i < nextVideoObject.transform.childCount; i++)
        {
            nextVideoObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void CalculateVideoLength()
    {
        // Get frame count and frame rate from the VideoPlayer. 
        ulong fC = thisVideo.frameCount;
        float frameRate = thisVideo.frameRate;

        frameCount = fC / frameRate;
    }

    IEnumerator Frames()
    {
        yield return new WaitForSeconds(frameCount);
        Debug.Log("framecountUP!!!!");
        nextVideoObject.SetActive(true);
        EnableChildren();
        DestroyVideoGO();
    }

    void DestroyVideoGO()
    {
        Destroy(gameObject);
    }
}
