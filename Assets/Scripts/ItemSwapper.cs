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
    private bool isClicking;
    private bool dye;

    void Start()
    {
        cleanlinessTracker = GameObject.Find("CleanlinessTracker");
        isClicking = false;
    }

    void Update()
    {
        if (isActive)
        {
            ActivateTool();
            if(onViking == true)
            {
                if (isClicking == true)
                {
                    if (isWash)
                    {
                        cleanlinessTracker.GetComponent<CleanlinessTracker>().Wash();
                    }
                    else
                    {
                        cleanlinessTracker.GetComponent<CleanlinessTracker>().Dye(dye);
                    }
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
            isClicking = true;
            cleanlinessTracker.GetComponent<CleanlinessTracker>().StartWashTimer(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentPar.Stop();
            isClicking = false;
            cleanlinessTracker.GetComponent<CleanlinessTracker>().StartWashTimer(false);
        }
    }

    // Info from VikingHoverSensor.cs
    public void IsHovering(bool hovering)
    {
        onViking = hovering;
    }

    // Setting which item was generated
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

    // coded for bath item buttons to know if they are wash or dye
    public void ActivateIsWash(bool wash)
    {
        isWash = wash;
    }

    public void SetDye(bool isDye)
    {
        dye = isDye;
    }
}
