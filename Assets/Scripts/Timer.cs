using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 30f; // Set the countdown duration
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText; // Optional: Assign a UI Text element in the Inspector
    private string timerString;

    void Start()
    {
        UpdateTimerDisplay();
    }

    public void StartTimer()
    {
        // Start the timer
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Decrease time
                
                UpdateTimerDisplay(); // Update UI
            }
            else
            {
                Debug.Log("Time's up!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = (int)(timeRemaining % 60);
        int minutes = (int)(timeRemaining / 60);

        timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }
}
