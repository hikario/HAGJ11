using UnityEngine;

public class VikingHoverSensor : MonoBehaviour
{
    [SerializeField]
    private GameObject itemSwapper;

    void OnMouseEnter()
    {
        Debug.Log("hovering");
        itemSwapper.GetComponent<ItemSwapper>().IsHovering();
    }
    void OnMouseExit()
    {
        Debug.Log("not hovering");
        itemSwapper.GetComponent<ItemSwapper>().NotHovering();
    }
}
