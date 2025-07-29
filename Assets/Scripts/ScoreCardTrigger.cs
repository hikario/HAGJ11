using UnityEngine;

public class ScoreCardTrigger : MonoBehaviour
{
    public GameObject starScore;
    
    void OnEnable()
    {
        starScore.GetComponent<StarScore>().StarAward();
    }
}
