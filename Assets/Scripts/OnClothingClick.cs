using UnityEngine;
using UnityEngine.EventSystems;

public class OnClothingClick : MonoBehaviour, IPointerClickHandler
{
    // On Click
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name + " Object Clicked");
    }
}
