using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private GameObject itemSprite;
    [SerializeField]
    private GameObject itemSwapper;
    [SerializeField]
    private AudioClip audioClip;

    private GameObject instancedGO;

    public bool dye;

    public void DeactivateItem()
    {
        itemSwapper.GetComponent<ItemSwapper>().isActive = false;
        itemSwapper.GetComponent<ItemSwapper>().SwapItem();
        itemSwapper.GetComponent<ItemSwapper>().SetCurrentGameObject(null);
    }

    public void ActivateItem()
    {
        itemSwapper.GetComponent<ItemSwapper>().isActive = true;
        itemSwapper.GetComponent<ItemSwapper>().particleSystem = particleSystem;
        itemSwapper.GetComponent<ItemSwapper>().itemSprite = itemSprite;
        itemSwapper.GetComponent<ItemSwapper>().SwapItem();
        itemSwapper.GetComponent<ItemSwapper>().SetCurrentGameObject(gameObject);
        itemSwapper.GetComponent<ItemSwapper>().SetDye(dye);
        if (audioClip != null)
        {
            itemSwapper.GetComponent<ItemSwapper>().SetAudio(audioClip);
        }
    }
}
