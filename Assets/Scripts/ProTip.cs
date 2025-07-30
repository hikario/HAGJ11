using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ProTip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDisplay;
    [SerializeField]
    private string tipText;

    public float typingSpeed = 0.1f; // Speed of typing in seconds

    void OnEnable()
    {
        textDisplay.text = string.Empty;
        StartCoroutine(TypeText());
    }

    public IEnumerator TypeText()
    {
        foreach (char letter in tipText)
        {
            textDisplay.text += letter; // Append each letter to the text
            yield return new WaitForSeconds(typingSpeed); // Wait for the specified duration
        }
    }
}
