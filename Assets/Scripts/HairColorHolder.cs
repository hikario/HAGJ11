using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HairColorHolder : MonoBehaviour
{
    public Color storedHairColor;
    public float storedDirtAmt;
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

    public void StoreHairColor()
    {

        storedHairColor = cleanlinessTracker.currentHairColor;
    }

    public void StoreDirtAmt()
    {

        storedDirtAmt = cleanlinessTracker.soapMax;
    }
}
