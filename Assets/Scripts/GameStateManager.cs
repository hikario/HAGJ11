using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState { START, PLAY, SUMMARY, END};

public class GameStateManager : MonoBehaviour
{
    // Event
    public UnityEvent<ClothingObject> onTopChange;
    public UnityEvent<ClothingObject> onBottomChange;
    public UnityEvent<ClothingObject> onFootwearChange;
    public UnityEvent<ClothingObject> onHairChange;
    public UnityEvent<ClothingObject> onBeardChange;
    public UnityEvent onSumButtonClick;

    public GameObject UIDoc;
    public GameObject MimirCanvas;
    public GameObject MimirText;
    public GameObject Viking;

    public GameState state;
    public string[] outputStrings;
    private int outStringIndex;
    private Points totalP;

    CharacterStatus cs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start!");
        Initialize();
    }

    void Initialize()
    {
        Debug.Log("cs = " + cs);
        cs = new CharacterStatus(GameObject.Find("VikingHair").GetComponent<SpriteRenderer>(),
                GameObject.Find("VikingBeard").GetComponent<SpriteRenderer>(),
                GameObject.Find("VikingDirt").GetComponent<SpriteRenderer>());

        if (onTopChange == null)
        {
            onTopChange = new UnityEvent<ClothingObject>();
        }
        onTopChange.AddListener(cs.UpdateTop);

        if (onBottomChange == null)
        {
            onBottomChange = new UnityEvent<ClothingObject>();
        }
        onBottomChange.AddListener(cs.UpdateBottom);

        if (onFootwearChange == null)
        {
            onFootwearChange = new UnityEvent<ClothingObject>();
        }
        onFootwearChange.AddListener(cs.UpdateFootwear);

        if (onHairChange == null)
        {
            onHairChange = new UnityEvent<ClothingObject>();
        }
        onHairChange.AddListener(cs.UpdateHair);

        if (onBeardChange == null)
        {
            onBeardChange = new UnityEvent<ClothingObject>();
        }
        onBeardChange.AddListener(cs.UpdateBeard);

        if (onSumButtonClick == null)
        {
            onSumButtonClick = new UnityEvent();
        }
        onSumButtonClick.AddListener(MoveToNextPhase);

        // After setting up all of these events, start the game by disabling the main UI and enabling the pro tip.
        UIDoc = GameObject.Find("UIDocument");
        MimirCanvas = GameObject.Find("MimirCanvas");
        MimirText = GameObject.Find("MimirTextBubble/Text (TMP)");
        Viking = GameObject.Find("Viking");

        UIDoc.SetActive(false);
        Viking.SetActive(false);
        MimirCanvas.SetActive(true);
        MimirText.GetComponent<TMPro.TMP_Text>().text = "Take a look in your wardrobe. Let's see what you have available.";
        state = GameState.START;


        outputStrings = new string[8];
        outStringIndex = 0;
        outputStrings[0] = "";
        outputStrings[1] = "You chose a ";
        outputStrings[2] = "<transition text>";
        outputStrings[3] = "You then chose some ";
        outputStrings[4] = "<transition text>";
        outputStrings[5] = "You lastly chose some ";
        outputStrings[6] = "<transition text>";
    }

    public GameState GetGameState()
    {
        return state;
    }

    void ModifyOutputStrings()
    {
        // For the opening phrase, we want to see how close to accurate we got
        // Check our total of historical.
        // If 3, then all were selected.
        // If 2 or 1, then only partial.
        // If 0, then nothing accurate. Try again.
        if(totalP.historical == 3)
        {
            outputStrings[0] = "A wonderful choice of outfit.";
            outputStrings[7] = "Overall a proper set of clothes for daily use.";
        }
        else if(totalP.historical > 0 && totalP.historical < 3)
        {
            outputStrings[0] = "Hmm. Interesting choices.";
            outputStrings[7] = "You're drawing an eye to yourself, but it may not be in the way you'd like.";
        }
        else
        {
            outputStrings[0] = "What bold and unorthodox choices.";
            outputStrings[7] = "This is crossing the line. Get back in there and find some different clothes. ";
            if(totalP.weeb == 3)
            {
                outputStrings[7] = outputStrings[7] + "What an unusual mascot. I wonder if there's a connection to Bragi here.";
            }
            else if(totalP.stereotypical == 3)
            {
                outputStrings[7] = outputStrings[7] + "Honestly, horns on a helmet??";
            }
        }

        // Add specifics to output strings
        outputStrings[1] = outputStrings[1] + cs.top.clothingName + ". " + cs.top.description;
        outputStrings[3] = outputStrings[3] + cs.bottom.clothingName + ". " + cs.bottom.description;
        outputStrings[5] = outputStrings[5] + cs.footwear.clothingName + ". " + cs.footwear.description;

        // Add variable transition words
        if(cs.top.point_allocation.historical == 0)
        {
            outputStrings[2] = "A bold decision. ";
        }
        else
        {
            outputStrings[2] = "A calculated choice. ";
        }
        if(cs.bottom.point_allocation.historical == 0)
        {
            outputStrings[2] = outputStrings[2] + "However, ";
            outputStrings[4] = "An unfortunate choice. ";
        }
        else
        {
            outputStrings[2] = outputStrings[2] + "Furthermore, ";
            outputStrings[4] = "An excellent choice. ";
        }
        if(cs.footwear.point_allocation.historical == 0)
        {
            outputStrings[4] = outputStrings[4] + "Concerningly, ";
            outputStrings[6] = "A worrisome selection. ";
        }
        else
        {
            outputStrings[4] = outputStrings[4] + "Impressively, ";
            outputStrings[6] = "Innovative and forward thinking. ";
        }

    }

    void MoveToGameplay()
    {
        MimirCanvas.SetActive(false);
        UIDoc.SetActive(true);
        Viking.SetActive(true);
        state = GameState.PLAY;
    }

    void MoveToNextPhase()
    {

        state = GameState.SUMMARY;
        totalP = TallyStatus();
        MimirCanvas.SetActive(true);
        UIDoc.SetActive(false);
        Viking.SetActive(true);
        ModifyOutputStrings();
        MimirText.GetComponent<TMPro.TMP_Text>().text = outputStrings[outStringIndex]; 
    }

    public Points TallyStatus()
    {
        Points total = new Points();
        Type csType = typeof(CharacterStatus);
        FieldInfo[] fields = csType.GetFields();
        // Add points
        foreach(FieldInfo fieldInfo in fields)
        {
            if(fieldInfo.Name == "top" || fieldInfo.Name == "bottom" || fieldInfo.Name == "footwear")
            {
                Debug.Log(fieldInfo.Name);
                ClothingObject clothing = fieldInfo.GetValue(cs) as ClothingObject; 
                total.historical += clothing.point_allocation.historical;
                total.weeb += clothing.point_allocation.weeb;
                total.stereotypical += clothing.point_allocation.stereotypical;
            }
        }

        Debug.Log("Cool: " + total.historical.ToString());
        Debug.Log("Dark: " + total.weeb.ToString());
        Debug.Log("Fancy: " + total.stereotypical.ToString());
        return total;
    }


    public void AdvanceSummaryText()
    {
        outStringIndex++;
        if(outStringIndex < outputStrings.Length)
        {
            MimirText.GetComponent<TMPro.TMP_Text>().text = outputStrings[outStringIndex]; 
        }
        else
        {
            state = GameState.END;
            MimirCanvas.SetActive(false);
            GameObject.Find("FadeScreenCanvas").GetComponent<SceneFader>().RunFade();
            if(totalP.historical > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}


// Character Status class. This will keep track of the currently worn equipment and make sure that
// there is only one thing equipped at a time.

[System.Serializable]
public class CharacterStatus
{
    public ClothingObject top;
    public ClothingObject bottom;
    public ClothingObject footwear;
    public ClothingObject hair;
    public ClothingObject beard;

    public SpriteRenderer hairSprite;
    public SpriteRenderer beardSprite;
    public SpriteRenderer dirtSprite;

    private Dictionary<string, GameObject> clothingMap;
    private Dictionary<string, string> hairAndBeardMap;

    public void UpdateTop(ClothingObject newTop)
    {
        if(clothingMap == null)
        {
            GenerateClothingMap();
            GenerateHairAndBeardMap();
        }
        // Disable old clothes
        if(top != null)
        {
            Debug.Log(top.clothingName);
            clothingMap[top.clothingName].SetActive(false);
        }

        // Changeover
        top = newTop;

        // Enable new Top;
        if(top != null)
        {
            clothingMap[top.clothingName].SetActive(true);
        }
    }

    public void UpdateBottom(ClothingObject newBottom)
    {
        if(clothingMap == null)
        {
            GenerateClothingMap();
            GenerateHairAndBeardMap();
        }
        // Disable old clothes
        if(bottom != null)
        {
            clothingMap[bottom.clothingName].SetActive(false);
        }

        // Changeover
        bottom = newBottom;

        // Enable new Bottom;
        if(bottom != null)
        {
            clothingMap[bottom.clothingName].SetActive(true);
        }

    }

    public void UpdateFootwear(ClothingObject newFootwear)
    {
        if(clothingMap == null)
        {
            GenerateClothingMap();
            GenerateHairAndBeardMap();
        }
        // Disable old clothes
        if(footwear != null)
        {
            clothingMap[footwear.clothingName].SetActive(false);
        }

        // Changeover
        footwear = newFootwear;

        // Enable new footwear;
        if(footwear != null)
        {
            clothingMap[footwear.clothingName].SetActive(true);
        }
    }

    public void UpdateHair(ClothingObject newHair)
    {
        if(clothingMap == null)
        {
            GenerateClothingMap();
            GenerateHairAndBeardMap();
        }
        hair = newHair;
        if(hair != null)
        {
            Sprite newSprite = Resources.Load<Sprite>(hairAndBeardMap[hair.clothingName]);

            hairSprite.sprite = newSprite;
        }
        else
        {
            Sprite newSprite = Resources.Load<Sprite>(hairAndBeardMap["Wet Hair"]);

            hairSprite.sprite = newSprite;
        }
    }

    public void UpdateBeard(ClothingObject newBeard)
    {
        if(clothingMap == null)
        {
            GenerateClothingMap();
            GenerateHairAndBeardMap();
        }
        beard = newBeard;
        if(beard != null)
        {
            Sprite newSprite = Resources.Load<Sprite>(hairAndBeardMap[beard.clothingName]);

            beardSprite.sprite = newSprite;          
        }
        else
        {
            Sprite newSprite = Resources.Load<Sprite>(hairAndBeardMap["Wet Beard"]);

            beardSprite.sprite = newSprite;
        }
    }

    public CharacterStatus(SpriteRenderer hair, SpriteRenderer beard, SpriteRenderer dirt)
    {
        hairSprite = hair;
        beardSprite = beard;
        dirtSprite = dirt;

        ColorHair();
        if(GameObject.Find("HairColorHolder") != null)
        {
            dirtSprite.material.SetFloat(
            Shader.PropertyToID("_MaskAmount"),
            GameObject.Find("HairColorHolder").GetComponent<HairColorHolder>().storedDirtAmt);            
        }

        top = null;
        bottom = null;
        footwear = null;

        GenerateClothingMap();
        GenerateHairAndBeardMap();

        DisableClothes();
    }

    private void ColorHair()
    {
        // Get old hair color
        GameObject hairColorHolder = GameObject.Find("HairColorHolder");
        if(hairColorHolder != null)
        {
            Color hairColor = hairColorHolder.GetComponent<HairColorHolder>().storedHairColor;

            // Apply to renderers
            hairSprite.color = hairColor;
            beardSprite.color = hairColor;

        }
    }

    private void GenerateClothingMap()
    {
        clothingMap = new Dictionary<string, GameObject>();
        // Shirts
        clothingMap.Add("Kyrtill", GameObject.Find("Shirt1"));
        clothingMap.Add("Mascot Shirt", GameObject.Find("Shirt2"));
        clothingMap.Add("Pauldron", GameObject.Find("Shirt3"));

        // Pants
        clothingMap.Add("Trousers", GameObject.Find("Pants1"));
        clothingMap.Add("Sweatpants", GameObject.Find("Pants2"));
        clothingMap.Add("Pteruges", GameObject.Find("Pants3"));

        // Shoes
        clothingMap.Add("Leather Shoes", GameObject.Find("Footwear2"));
        clothingMap.Add("Flip-Flops", GameObject.Find("Footwear1"));
        clothingMap.Add("Hunting Boots", GameObject.Find("Footwear3"));
    }

    private void GenerateHairAndBeardMap()
    {
        hairAndBeardMap = new Dictionary<string, string>();
        
        // Add hairstyles by listed name, map to sprite names
        hairAndBeardMap.Add("Wet Hair", "Hair/LAG_hair_00_wet");
        hairAndBeardMap.Add("Suebian Knot", "Hair/LAG_hair_1_knot");
        hairAndBeardMap.Add("Long Fringe", "Hair/LAG_hair_2_longfringe");
        hairAndBeardMap.Add("Fringe", "Hair/LAG_hair_3_fringe");
        hairAndBeardMap.Add("Bowl Cut", "Hair/LAG_hair_4_bowl");

        // Add beard styles, map to sprite name
        hairAndBeardMap.Add("Wet Beard", "Beards/LAG_beard_00_wet");
        hairAndBeardMap.Add("Forked", "Beards/LAG_beard_1_fork");
        hairAndBeardMap.Add("Mustache", "Beards/LAG_beard_2_mustache");
        hairAndBeardMap.Add("Braided", "Beards/LAG_beard_3_braid");
        hairAndBeardMap.Add("Full", "Beards/LAG_beard_4_full");
    }

    private void DisableClothes()
    {
        foreach(GameObject clothing in clothingMap.Values)
        {
            clothing.SetActive(false);
        }
    }

}