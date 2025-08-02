using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickAdvance : MonoBehaviour, IPointerClickHandler
{
    public GameObject GSM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GSM == null)
        {
            GSM = GameObject.Find("EventSystem");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameState state = GSM.GetComponent<GameStateManager>().GetGameState();
        if(state == GameState.START)
        {
            state = GameState.PLAY;
            GSM.SendMessage("MoveToGameplay");
        }
        else if(state == GameState.SUMMARY)
        {
            GSM.SendMessage("AdvanceSummaryText");
        }
    }

}
