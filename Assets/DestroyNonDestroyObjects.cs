using UnityEngine;

public class DestroyNonDestroyObjects : MonoBehaviour
{

    private GameObject audioSource;
    private GameObject hairColorHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GameObject.Find("Audio Source");
        hairColorHolder = GameObject.Find("HairColorHolder");
    }

    public void Cleanup()
    {
        audioSource.SendMessage("DestroySelf");
        hairColorHolder.SendMessage("DestroySelf");
    } 
}
