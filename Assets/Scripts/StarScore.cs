using UnityEngine;

public class StarScore : MonoBehaviour
{
    private int stars;
    private GameObject star1;
    private GameObject star2;
    private GameObject star3;

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

        }
        if (stars == 1)
        {

        }
        if (stars == 2)
        {

        }
        if (stars == 3)
        {

        }
    }
}
