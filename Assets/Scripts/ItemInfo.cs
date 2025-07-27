using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private GameObject itemSprite;
    [SerializeField]
    private GameObject itemSwapper;

    public void DeactivateItem()
    {
        itemSwapper.GetComponent<ItemSwapper>().isActive = false;
        itemSwapper.GetComponent<ItemSwapper>().SwapItem();
    }

    public void ActivateItem()
    {
        itemSwapper.GetComponent<ItemSwapper>().isActive = true;
        itemSwapper.GetComponent<ItemSwapper>().particleSystem = particleSystem;
        itemSwapper.GetComponent<ItemSwapper>().itemSprite = itemSprite;
        itemSwapper.GetComponent<ItemSwapper>().SwapItem();
    }
}
