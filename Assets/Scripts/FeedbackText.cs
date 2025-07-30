using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class FeedbackText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDisplay;
    [SerializeField]
    private string goodText;
    [SerializeField]
    private string midText;
    [SerializeField]
    private string badText;

    public float typingSpeed = 0.1f; // Speed of typing in seconds

    private string fullText; // The complete text to be typed

    public StarScore starScore;
    public int starAmt;

    void Start()
    {
    }

    void OnEnable()
    {
        starScore = GameObject.Find("StarScore").GetComponent<StarScore>();

        starAmt = starScore.stars;
        if (starAmt == 0)
        {
            DisplayBad();
        }
        if (starAmt == 1)
        {
            DisplayBad();
        }
        if (starAmt == 2)
        {
            DisplayMid();
        }
        if (starAmt == 3)
        {
            DisplayGood();
        }
    }

    // Calls which text is displayed
    public void DisplayGood()
    {
        fullText = goodText; // Store the full text
        textDisplay.text = string.Empty; // Clear the text
        StartCoroutine(TypeText());
    }
    public void DisplayMid()
    {
        fullText = midText; // Store the full text
        textDisplay.text = string.Empty; // Clear the text
        StartCoroutine(TypeText());
    }
    public void DisplayBad()
    {
        fullText = badText; // Store the full text
        textDisplay.text = string.Empty; // Clear the text
        StartCoroutine(TypeText());
    }

    public IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            textDisplay.text += letter; // Append each letter to the text
            yield return new WaitForSeconds(typingSpeed); // Wait for the specified duration
        }
    }
}
