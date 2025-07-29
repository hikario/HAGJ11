using UnityEngine;

public class StarScore : MonoBehaviour
{
    private int stars;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject results;

    void Start()
    {
        stars = 0;
    }

    public void StarLevel(float strs)
    {
        stars = Mathf.RoundToInt(strs);
    }

    public void StarAward()
    {
        if (stars == 0)
        {
            results.GetComponent<ObjectTimerFlipper>().RemoveNext();
            results.GetComponent<ObjectTimerFlipper>().MakeFinalFlip();
        }
        if (stars == 1)
        {
            star1.GetComponent<ObjectTimerFlipper>().RemoveNext();
            star1.GetComponent<ObjectTimerFlipper>().MakeFinalFlip();
        }
        if (stars == 2)
        {
            star2.GetComponent<ObjectTimerFlipper>().RemoveNext();
            star2.GetComponent<ObjectTimerFlipper>().MakeFinalFlip();
        }
        if (stars == 3)
        {
            star3.GetComponent<ObjectTimerFlipper>().MakeFinalFlip();
        }
    }
}
