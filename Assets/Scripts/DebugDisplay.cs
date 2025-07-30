using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DebugDisplay : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private string debugString;
    public GameObject go;

    // Update is called once per frame
    void Update()
    {
        debugString = go.GetComponent<CleanlinessTracker>().currentCleanlinessLevel.ToString();
        debugText.text = debugString;
    }
}
