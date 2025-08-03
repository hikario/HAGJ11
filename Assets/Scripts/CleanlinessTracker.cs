using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private GameObject hch;
    private HairColorHolder hairColorHolder;
    #endregion

    #region VFXVars
    [SerializeField]
    public ParticleSystem sparkleParticles;
    public ParticleSystem wetVFX;
    private bool isWet;
    #endregion

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
        curClean = 0;
        starScore = GameObject.Find("StarScore");
        muddyAmt = Shader.PropertyToID("_MaskAmount");
        dirtAmt = Shader.PropertyToID("_MaskAmount");
        beardCleanAmt = Shader.PropertyToID("_CombAmt");
        muddy.material.SetFloat(muddyAmt, waterMax);
        dirt.material.SetFloat(dirtAmt, soapMax);
        hch = GameObject.FindGameObjectWithTag("HairColorHolder");
        if (hch != null)
        {
            hairColorHolder = hch.GetComponent<HairColorHolder>();
            currentHairColor = hairColorHolder.storedHairColor;
        }
    }

    void Update()
    {
        CleanLevel();
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
                    waterMax = waterMax - (cleanTickAmount * Time.deltaTime);
                    currentCleanlinessLevel = currentCleanlinessLevel - (cleanTickAmount * Time.deltaTime);
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
                        soapMax = soapMax - (cleanTickAmount * Time.deltaTime); 
                        currentCleanlinessLevel = currentCleanlinessLevel - (cleanTickAmount * Time.deltaTime);
                        dirt.material.SetFloat(dirtAmt, soapMax);
                    }
                }
                if (combActive)
                {
                    if (combMax > 0.5)
                    {
                        combMax = combMax - (cleanTickAmount * Time.deltaTime);
                        currentCleanlinessLevel = currentCleanlinessLevel - (cleanTickAmount * Time.deltaTime);
                        // beard.material.SetFloat(beardCleanAmt, (combMax / maxThird);
                    }
                    if (combMax <= ((maxThird/3) * 2))
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
        if (isWet)
        {
            if (!isDye)
            {
                currentHairColor = currentHairColor + ((dyeTickAmount * 3) * Time.deltaTime);
                hairColorHolder.StoreHairColor();
            }
            else
            {
                if (currentHairColor.r <= 1)
                {
                    currentHairColor.b = currentHairColor.b - ((dyeTickAmount.b * 3) * Time.deltaTime);
                    currentHairColor.r = currentHairColor.r + ((dyeTickAmount.r * 3) * Time.deltaTime);
                    currentHairColor.g = currentHairColor.g - (dyeTickAmount.g * Time.deltaTime);
                    hairColorHolder.StoreHairColor();
                }
            }
        }
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
        if (currentHairColor.r > 0.55f)
        {
            currentHairColor.r = 0.55f;
        }
        if (currentHairColor.g > 0.4f)
        {
            currentHairColor.g = 0.4f;
        }
        if (currentHairColor.b > 0.3f)
        {
            currentHairColor.b = 0.3f;
        }
    }

    void EnsureMinColor()
    {
        if (currentHairColor.r < 0.4f)
        {
            currentHairColor.r = 0.4f;
        }
        if (currentHairColor.g < 0.15)
        {
            currentHairColor.g = 0.15f;
        }
        if (currentHairColor.b < 0.1f)
        {
            currentHairColor.b = 0.1f;
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
