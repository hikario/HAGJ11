using UnityEngine;

public class ObjectTimerFlipper : MonoBehaviour
{
    [SerializeField]
    private GameObject nextObject;
    public bool turnOff;

    public float delay;

    public void StartFlipper()
    {
        Invoke("FlipObjects", delay);
    }

    public void FlipObjects()
    {
        if(nextObject != null)
        {
            nextObject.SetActive(true);
            nextObject.GetComponent<ObjectTimerFlipper>().StartFlipper();
            Debug.Log("Fwip!!!!");
        }
        if (turnOff)
        {
            gameObject.SetActive(false);
        }
    }
}
