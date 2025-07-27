using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSwapper : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Sprite itemSprite;

    public bool isActive;
    private bool activateOnViking;

    void Update()
    {
        if (isActive)
        {
            if (!activateOnViking)
            {
                var screenPoint = Input.mousePosition;
                var pos = Camera.main.ScreenToWorldPoint(screenPoint);
                particleSystem.transform.position = pos;
                if (Input.GetMouseButtonDown(0))
                {
                    particleSystem.Play();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    particleSystem.Stop();
                }
                //Put bucket png on cursor
            }
        }
    }
}
