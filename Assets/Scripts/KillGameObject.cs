using UnityEngine;

public class KillGameObject : MonoBehaviour
{
    [SerializeField]
    private string tag;

    void Start()
    {
        GameObject killObj = GameObject.FindGameObjectWithTag(tag);
        if (killObj)
            Destroy(killObj);
    }
}
