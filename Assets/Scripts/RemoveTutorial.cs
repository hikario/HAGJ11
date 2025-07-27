using UnityEngine;

public class RemoveTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialCanvas;

    public void DeactivateCanvas()
    {
        tutorialCanvas.SetActive(false); 
    }
}
