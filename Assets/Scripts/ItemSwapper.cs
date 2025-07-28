using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSwapper : MonoBehaviour
{
    // Make private later
    public ParticleSystem particleSystem;
    public GameObject itemSprite;
    private GameObject currentItem;

    public ParticleSystem currentPar;
    public GameObject currentSprite;

    public bool isActive;
    private bool onViking;
    private GameObject cleanlinessTracker;
    private bool isWash;

    void Start()
    {
        cleanlinessTracker = GameObject.Find("CleanlinessTracker");
    }

    void Update()
    {
        if (isActive)
        {
            ActivateTool();
            if (onViking == true)
            {
                if (isWash)
                {
                    cleanlinessTracker.GetComponent<CleanlinessTracker>().Wash();
                }
                else
                {
                    cleanlinessTracker.GetComponent<CleanlinessTracker>().Dye();
                }
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

    void ActivateTool()
    {
        var screenPoint = Input.mousePosition;
        var pos = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector2 v2pos = new Vector2(pos.x, pos.y);

        currentPar.transform.position = v2pos;
        currentSprite.transform.position = v2pos;
        if (Input.GetMouseButtonDown(0))
        {
            currentPar.Play();
            cleanlinessTracker.GetComponent<CleanlinessTracker>().StartWashTimer();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentPar.Stop();
            cleanlinessTracker.GetComponent<CleanlinessTracker>().StopWashTimer();
        }
    }

    public void IsHovering()
    {
        onViking = true;
    }

    public void NotHovering()
    {
        onViking = false;
    }

    public void SetCurrentGameObject(GameObject curItem)
    {
        if (curItem == null)
        {
            currentItem = null;
        }
        else
        {
            currentItem = curItem;
        }
    }

    public void ActivateIsWash()
    {
        isWash = true;
    }

    public void DeactivateIsWash()
    {
        isWash = false;
    }
}
