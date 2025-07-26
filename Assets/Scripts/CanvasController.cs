using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public void DisableCanvasComponent()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = false;
        }
    }

    public void EnableCanvasComponent()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = true;
        }
    }
}
