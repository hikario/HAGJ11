using UnityEngine;

public class CleanlinessTracker : MonoBehaviour
{
    #region Cleanliness
    public float max = 30;
    public float cleanTickAmount = 0.01f;
    public float currentCleanlinessLevel;
    #endregion

    #region Dye
    public Vector3 beginningHairColor;
    public Vector3 currentHairColor;
    public Vector3 dyeTickAmount;
    #endregion

    private bool timer;
    private float tTime;
    private bool isWash = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCleanlinessLevel = max;
        currentHairColor = beginningHairColor;
        timer = false;
    }

    void Update()
    {
        if (timer)
        {
            tTime += Time.deltaTime;
        }
        else
        {
            tTime = 0; // Reset
        }
        // Debug.Log(tTime);
    }

    public void Wash()
    {
        Debug.Log("is washing!!!");
        if (currentCleanlinessLevel > 0)
        {
            currentCleanlinessLevel = currentCleanlinessLevel - (cleanTickAmount * tTime);
        }
    }

    public void Dye()
    {
        if (currentHairColor.x < 255)
        {
            currentHairColor = currentHairColor + (dyeTickAmount * tTime);
        }
    }

    public void StartWashTimer()
    {
        timer = true;
    }
    
    public void StopWashTimer()
    {
        timer = false;
    }

    public void ActivateIsWash()
    {
        isWash = true;
    }

    public void DeactivateIsWash()
    {
        isWash = false;
    }
}
