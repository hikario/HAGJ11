using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    private List<GameObject> buttonList;

    void Start()
    {
        buttonList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            buttonList.Add(child.gameObject);
        }
        Debug.Log(buttonList.ToString());
    }

    public void DisableButtons()
    {
        foreach (GameObject button in buttonList)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }

    public void EnableButtons()
    {
        foreach (GameObject button in buttonList)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }
}
