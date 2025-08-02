using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HairColorHolder : MonoBehaviour
{
    public Color storedHairColor;
    private GameObject ct;
    private CleanlinessTracker cleanlinessTracker;

    private static HairColorHolder _Instance;
    public static HairColorHolder Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }
        ct = GameObject.FindGameObjectWithTag("CleanlinessTracker");
        if (ct != null)
        {
            cleanlinessTracker = ct.GetComponent<CleanlinessTracker>();
        }
    }

    //void Update()
    //{
    //    if (cleanlinessTracker != null)
    //    {
    //        StoreHairColor();
    //    }
    //}
    public void StoreHairColor()
    {

        storedHairColor = cleanlinessTracker.currentHairColor;
    }
}
