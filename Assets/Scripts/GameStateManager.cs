using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

public enum GameState { START, PLAY, SUMMARY, END};

public class GameStateManager : MonoBehaviour
{
    // Event
    public static UnityEvent<ClothingObject> onTopChange;
    public static UnityEvent<ClothingObject> onBottomChange;
    public static UnityEvent<ClothingObject> onFootwearChange;
    public static UnityEvent<ClothingObject> onHatChange;
    public static UnityEvent onSumButtonClick;

    public GameObject UIDoc;
    public GameObject MimirCanvas;
    public GameObject MimirText;

    public GameState state;
    public string[] outputStrings;
    private int outStringIndex;
    private Points totalP;

    CharacterStatus cs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cs = new CharacterStatus();

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

        if (onHatChange == null)
        {
            onHatChange = new UnityEvent<ClothingObject>();
        }
        onHatChange.AddListener(cs.UpdateHat);

        if (onSumButtonClick == null)
        {
            onSumButtonClick = new UnityEvent();
        }
        onSumButtonClick.AddListener(MoveToNextPhase);

        // After setting up all of these events, start the game by disabling the main UI and enabling the pro tip.
        UIDoc = GameObject.Find("UIDocument");
        MimirCanvas = GameObject.Find("MimirCanvas");
        MimirText = GameObject.Find("MimirTextBubble/Text (TMP)");

        UIDoc.SetActive(false);
        MimirCanvas.SetActive(true);
        MimirText.GetComponent<TMPro.TMP_Text>().text = "Time to dress for the occasion.";
        state = GameState.START;

        outputStrings = new string[9];
        outStringIndex = 0;
        outputStrings[0] = "Interesting choices.";
        outputStrings[1] = "You chose ";
        outputStrings[2] = "<transition text>";
        outputStrings[3] = "You then chose ";
        outputStrings[4] = "<transition text>";
        outputStrings[5] = "You also chose ";
        outputStrings[6] = "<transition text>";
        outputStrings[7] = "You lastly chose ";
    }

    public GameState GetGameState()
    {
        return state;
    }

    void ModifyOutputStrings()
    {
        // Add specifics to output strings
        outputStrings[1] = outputStrings[1] + cs.top.clothingName + ". " + cs.top.description;
        outputStrings[3] = outputStrings[3] + cs.bottom.clothingName + ". " + cs.bottom.description;
        outputStrings[5] = outputStrings[5] + cs.footwear.clothingName + ". " + cs.footwear.description;
        outputStrings[7] = outputStrings[7] + cs.hat.clothingName + ". " + cs.hat.description;

        // Add variable transition words
        if(cs.top.point_allocation.cool < 0)
        {
            outputStrings[2] = "A bold decision. ";
        }
        else
        {
            outputStrings[2] = "A calculated choice. ";
        }
        if(cs.bottom.point_allocation.cool < 0)
        {
            outputStrings[2] = outputStrings[2] + "However, ";
            outputStrings[4] = "An unfortunate choice. ";
        }
        else
        {
            outputStrings[2] = outputStrings[2] + "Furthermore, ";
            outputStrings[4] = "An excellent choice. ";
        }
        if(cs.footwear.point_allocation.cool < 0)
        {
            outputStrings[4] = outputStrings[4] + "Concerningly, ";
            outputStrings[6] = "A worrisome selection. ";
        }
        else
        {
            outputStrings[4] = outputStrings[4] + "Impressively, ";
            outputStrings[6] = "Innovative and forward thinking. ";
        }
        if(cs.hat.point_allocation.cool < 0)
        {
            outputStrings[6] = outputStrings[6] + "Sadly, ";
        }
        else
        {
            outputStrings[6] = outputStrings[6] + "Wisely, "; 
        }

        if(totalP.cool > 10)
        {
            outputStrings[8] = "All in all, a really cool outfit.";
        }
        else if(totalP.dark > 10)
        {
            outputStrings[8] = "In summary, a moderately edgy outfit.";
        }
        else if(totalP.fancy > 10)
        {
            outputStrings[8] = "To conclude, a classy but convoluted outfit.";
        }
        else
        {
            outputStrings[8] = "Bit generic of an outfit, innit?";
        }
    }

    void MoveToGameplay()
    {
        MimirCanvas.SetActive(false);
        UIDoc.SetActive(true);
        state = GameState.PLAY;
    }

    void MoveToNextPhase()
    {

        state = GameState.SUMMARY;
        totalP = TallyStatus();
        MimirCanvas.SetActive(true);
        UIDoc.SetActive(false);
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
            ClothingObject clothing = fieldInfo.GetValue(cs) as ClothingObject; 
            total.cool += clothing.point_allocation.cool;
            total.dark += clothing.point_allocation.dark;
            total.fancy += clothing.point_allocation.fancy;
        }

        Debug.Log("Cool: " + total.cool.ToString());
        Debug.Log("Dark: " + total.dark.ToString());
        Debug.Log("Fancy: " + total.fancy.ToString());
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
    public ClothingObject hat;


    public void UpdateTop(ClothingObject newTop)
    {
        top = newTop;
    }

    public void UpdateBottom(ClothingObject newBottom)
    {
        bottom = newBottom;
    }

    public void UpdateFootwear(ClothingObject newFootwear)
    {
        footwear = newFootwear;
    }

    public void UpdateHat(ClothingObject newHat)
    {
        hat = newHat;
    }

}