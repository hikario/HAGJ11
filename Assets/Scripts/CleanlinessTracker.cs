using UnityEngine;

public class CleanlinessTracker : MonoBehaviour
{
    #region CleanlinessVars
    public float max = 30;
    public float cleanTickAmount = 0.01f;
    public float currentCleanlinessLevel;
    #endregion

    #region DyeVars
    public Vector3 beginningHairColor;
    public Vector3 currentHairColor;
    public Vector3 dyeTickAmount;
    #endregion

    #region VFXVars
    [SerializeField]
    public ParticleSystem sparkleParticles;
    #endregion

    private bool timer;
    private int tTime;

    private float cleanLevel1;
    private float cleanLevel2;
    private float cleanLevel3;

    private float curClean;

    private GameObject starScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCleanlinessLevel = max;
        currentHairColor = beginningHairColor;
        timer = false;
        curClean = 0;
        starScore = GameObject.Find("StarScore");
    }

    void Update()
    {
        CleanLevel();
        WashTimer();
    }

    public void Wash()
    {
        if (currentCleanlinessLevel > 0)
        {
            currentCleanlinessLevel = currentCleanlinessLevel - cleanTickAmount;
        }
    }

    public void Dye()
    {
        if (currentHairColor.x < 255)
        {
            currentHairColor = currentHairColor + dyeTickAmount;
        }
    }

    void WashTimer()
    {
        if (timer)
        {
            tTime += Mathf.RoundToInt(Time.deltaTime);
        }
        else
        {
            tTime = 0; // Reset
        }
        // Debug.Log(tTime);
    }

    public void StartWashTimer(bool run)
    {
        timer = run;
    }

    // Determine cleanlevel for stars
    void CleanLevel()
    {
        cleanLevel1 = max / 4;
        cleanLevel2 = cleanLevel1 * 2;
        cleanLevel3 = cleanLevel1 * 3;
        starScore.GetComponent<StarScore>().StarLevel(curClean);

        if (currentCleanlinessLevel >= cleanLevel2 && currentCleanlinessLevel < cleanLevel3)
        {
            curClean = 1;
        }
        if (currentCleanlinessLevel >= cleanLevel1 && currentCleanlinessLevel < cleanLevel2)
        {
            curClean = 2;
        }
        if (Mathf.RoundToInt(currentCleanlinessLevel) == (Mathf.RoundToInt(cleanLevel1)))
        {
            curClean = 3;
            SetSparkle();
        }
    }

    // Set Sparkle when curClean = 3
    void SetSparkle()
    {
        Debug.Log("setting sparkle!!!!");
        sparkleParticles.Play();
    }
}
