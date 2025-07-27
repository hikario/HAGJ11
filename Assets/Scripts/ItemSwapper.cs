using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSwapper : MonoBehaviour
{
    // Make private later
    public ParticleSystem particleSystem;
    public GameObject itemSprite;

    public ParticleSystem currentPar;
    public GameObject currentSprite;

    public bool isActive;
    public bool activateOnViking;

    void Update()
    {
        if (isActive)
        {
            if (!activateOnViking)
            {
                var screenPoint = Input.mousePosition;
                var pos = Camera.main.ScreenToWorldPoint(screenPoint);
                Vector2 v2pos = new Vector2(pos.x, pos.y);
                
                currentPar.transform.position = v2pos;
                currentSprite.transform.position = v2pos;
                if (Input.GetMouseButtonDown(0))
                {
                    currentPar.Play();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    currentPar.Stop();
                }
                //Put bucket png on cursor
            }
        }
    }

    public void SwapItem()
    {
        Debug.Log("Swapped!!!");
        if (currentPar)
        {
            Destroy(currentPar.gameObject);
        }
        if (currentSprite)
        {
            Destroy(currentSprite);
        }
        if (isActive)
        {
            currentPar = Instantiate(particleSystem);
            currentSprite = Instantiate(itemSprite);
        }
    }
}
