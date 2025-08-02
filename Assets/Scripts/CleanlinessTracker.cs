using UnityEngine;

public class CleanlinessTracker : MonoBehaviour
{
    #region CleanlinessVars
    public float max = 30;
    public float cleanTickAmount = 0.01f;
    public float currentCleanlinessLevel;

    private float curWash;

    private float cleanLevel1;
    private float cleanLevel2;
    private float cleanLevel3;

    private float maxThird;
    private bool waterActive;
    private float waterMax;
    private bool soapActive;
    private float soapMax;
    private bool combActive;
    private float combMax;

    private float curClean;

    public Renderer muddy;
    private int muddyAmt;

    public Renderer dirt;
    private int dirtAmt;

    private int beardCleanAmt;
    #endregion

    #region DyeVars
    public Color currentHairColor;
    public Color dyeTickAmount;

    private bool hairdye;

    public SpriteRenderer beard;
    #endregion

    #region VFXVars
    [SerializeField]
    public ParticleSystem sparkleParticles;
    public ParticleSystem wetVFX;
    private bool isWet;
    #endregion

    private bool timer;
    private int tTime;

    private GameObject starScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCleanlinessLevel = max;
        maxThird = max / 3;
        waterMax = maxThird;
        soapMax = maxThird;
        combMax = maxThird;
        currentHairColor = beard.color;
        timer = false;
        curClean = 0;
        starScore = GameObject.Find("StarScore");
        muddyAmt = Shader.PropertyToID("_MaskAmount");
        dirtAmt = Shader.PropertyToID("_MaskAmount");
        beardCleanAmt = Shader.PropertyToID("_CombAmt");
        muddy.material.SetFloat(muddyAmt, waterMax);
        dirt.material.SetFloat(dirtAmt, soapMax);
    }

    void Update()
    {
        CleanLevel();
        WashTimer();
        EnsureMaxColor();
        EnsureMinColor();
        beard.color = currentHairColor;
    }

    public void Wash()
    {
        if (currentCleanlinessLevel > 0)
        {
            if (waterActive)
            { 
                if (waterMax > 0)
                {
                    waterMax = waterMax - cleanTickAmount;
                    currentCleanlinessLevel = currentCleanlinessLevel - cleanTickAmount;
                    muddy.material.SetFloat(muddyAmt, waterMax);
                }
                if (waterMax <= 0)
                {
                    if (!isWet)
                    {
                        isWet = true;
                        wetVFX.Play();
                    }
                }
            }
            if (isWet)
            {
                if (soapActive)
                {
                    if (soapMax > 0)
                    {
                        soapMax = soapMax - cleanTickAmount;
                        currentCleanlinessLevel = currentCleanlinessLevel - cleanTickAmount;
                        dirt.material.SetFloat(dirtAmt, soapMax);
                    }
                }
                if (combActive)
                {
                    if (combMax > 0.5)
                    {
                        combMax = combMax - cleanTickAmount;
                        currentCleanlinessLevel = currentCleanlinessLevel - cleanTickAmount;
                        beard.material.SetFloat(beardCleanAmt, combMax / maxThird);
                    }
                    if (combMax <= maxThird/2)
                    {
                        beard.material.SetFloat(beardCleanAmt, 0);
                    }
                }
            }
            
        }
    }

    public void Dye(bool isDye)
    {
        hairdye = isDye;
        if (!isDye)
        {
            currentHairColor = currentHairColor + dyeTickAmount;
        }
        else
        {
            if (currentHairColor.r <= 1)
            {
                currentHairColor.b = currentHairColor.b - (dyeTickAmount.b);
                currentHairColor.r = currentHairColor.r + (dyeTickAmount.r);
                currentHairColor.g = currentHairColor.g - (dyeTickAmount.g * 0.3f);
            }
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
        sparkleParticles.Play();
    }

    #region ColorRange
    void EnsureMaxColor()
    {
        if (currentHairColor.r > 1)
        {
            currentHairColor.r = 1;
        }
        if (currentHairColor.g > 1)
        {
            currentHairColor.g = 1;
        }
        if (currentHairColor.b > 1)
        {
            currentHairColor.b = 1;
        }
    }

    void EnsureMinColor()
    {
        if (currentHairColor.r < 0.15)
        {
            currentHairColor.r = 0.15f;
        }
        if (currentHairColor.g < 0.30)
        {
            currentHairColor.g = 0.30f;
        }
        if (currentHairColor.b < 0)
        {
            currentHairColor.b = 0;
        }
    }
    #endregion

    #region ActivateBools
    public void ActivateWater()
    {
        waterActive = true;
        soapActive = false;
        combActive = false;
    }

    public void ActivateSoap()
    {
        waterActive = false;
        soapActive = true;
        combActive = false;
    }

    public void ActivateComb()
    {
        waterActive = false;
        soapActive = false;
        combActive = true;
    }

    public void DeactivateAll()
    {
        waterActive = false;
        soapActive = false;
        combActive = false;
    }
    #endregion
}
