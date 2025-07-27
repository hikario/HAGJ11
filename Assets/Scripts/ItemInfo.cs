using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private GameObject itemSwapper;

    public void DeactivateItem()
    {
        itemSwapper.GetComponent<ItemSwapper>().isActive = false;
    }

    public void ActivateItem()
    {
        itemSwapper.GetComponent<ItemSwapper>().isActive = true;
        itemSwapper.GetComponent<ItemSwapper>().particleSystem = particleSystem;
        itemSwapper.GetComponent<ItemSwapper>().itemSprite = itemSprite;
    }
}
