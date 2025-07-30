using UnityEngine;

public class VikingHoverSensor : MonoBehaviour
{
    // debug
    public SpriteRenderer viking;

    [SerializeField]
    private GameObject itemSwapper;

    void OnMouseEnter()
    {
        Debug.Log("hovering");
        itemSwapper.GetComponent<ItemSwapper>().IsHovering(true);
        // viking.color = Color.green;
    }
    void OnMouseExit()
    {
        Debug.Log("not hovering");
        itemSwapper.GetComponent<ItemSwapper>().IsHovering(false);
        // viking.color = Color.red;
    }
}
