using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class UIBacking
{
    VisualTreeAsset m_ClothingTemplate;

    ListView m_TopsList;
    ListView m_BottomsList;
    ListView m_FootwearList;
    ListView m_HairsList;
    ListView m_BeardsList;

    Button m_Button;

    GameStateManager GSM; 

    bool topSelected;
    bool bottomSelected;
    bool footwearSelected;
    bool hairSelected;
    bool beardSelected;

    public void InitializeClothingList(VisualElement root, VisualTreeAsset clothingTemplate, GameStateManager gsm)
    {
        Wardrobe wardrobe = LoadWardrobeFromJSONFile();

        GSM = gsm;

        m_ClothingTemplate = clothingTemplate;

        m_TopsList = root.Q<ListView>("Tops");
        m_BottomsList = root.Q<ListView>("Bottoms");
        m_FootwearList = root.Q<ListView>("Footwear");
        m_HairsList = root.Q<ListView>("Hairs");
        m_BeardsList = root.Q<ListView>("Beards");

        m_Button = root.Q<Button>("ConfirmButton");

        m_Button.RegisterCallback<ClickEvent>(OnClickCallback);

        FillTopsList(wardrobe.tops);
        FillBottomsList(wardrobe.bottoms);
        FillFootwearList(wardrobe.footwear);
        FillHairsList(wardrobe.hairs);
        FillBeardsList(wardrobe.beards);

        m_TopsList.selectionChanged += OnTopSelected;
        m_BottomsList.selectionChanged += OnBottomSelected;
        m_FootwearList.selectionChanged += OnFootwearSelected;
        m_HairsList.selectionChanged += OnHairSelected;
        m_BeardsList.selectionChanged += OnBeardSelected;

        topSelected = false;
        bottomSelected = false;
        footwearSelected = false;
        hairSelected = false;
        beardSelected = false;

        m_Button.SetEnabled(false);
    }

    public void OnDisable()
    {
        m_TopsList.selectionChanged -= OnTopSelected;
        m_BottomsList.selectionChanged -= OnBottomSelected;
        m_FootwearList.selectionChanged -= OnFootwearSelected;
        m_HairsList.selectionChanged -= OnHairSelected;
        m_BeardsList.selectionChanged -= OnBeardSelected;

        m_Button.UnregisterCallback<ClickEvent>(OnClickCallback);
    }

    void OnClickCallback(ClickEvent evt)
    {
        GSM.onSumButtonClick.Invoke();
    }

    void FillClothingList(ListView listV, List<ClothingObject> clothingList)
    {
        // Make Item
        listV.makeItem = () =>
        {
            var newListEntry = m_ClothingTemplate.Instantiate();

            var newListEntryLogic = new ClothingBacking();

            newListEntry.userData = newListEntryLogic;

            newListEntryLogic.SetVisualElement(newListEntry);

            return newListEntry;
        };

        // Bind Item
        listV.bindItem = (item, index) =>
        {
            (item.userData as ClothingBacking)?.SetClothingData(clothingList[index]); 
        };

        // Height
        listV.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;

        // Source Array
        listV.itemsSource = clothingList;
    }

    void FillTopsList(List<ClothingObject> clothingList)
    {
        FillClothingList(m_TopsList, clothingList);
    }

    void FillBottomsList(List<ClothingObject> clothingList)
    {
        FillClothingList(m_BottomsList, clothingList);
    }

    void FillFootwearList(List<ClothingObject> clothingList)
    {
        FillClothingList(m_FootwearList, clothingList);
    }

    void FillHairsList(List<ClothingObject> clothingList)
    {
        FillClothingList(m_HairsList, clothingList);
    }

    void FillBeardsList(List<ClothingObject> clothingList)
    {
        FillClothingList(m_BeardsList, clothingList);
    }


    void OnTopSelected(IEnumerable<object> selectedItems)
    {
        var selectedClothing = m_TopsList.selectedItem as ClothingObject;

        if (selectedClothing == null)
        {
            topSelected = false;
            ControlConfirmationButton();
            return;
        }

        topSelected = true;
        GSM.onTopChange.Invoke(selectedClothing);

        ControlConfirmationButton();
    }

    void OnBottomSelected(IEnumerable<object> selectedItems)
    {
        var selectedClothing = m_BottomsList.selectedItem as ClothingObject;

        if (selectedClothing == null)
        {
            bottomSelected = false;
            ControlConfirmationButton();

            return;
        }

        bottomSelected = true;
        GSM.onBottomChange.Invoke(selectedClothing);

        ControlConfirmationButton();

    }

    void OnFootwearSelected(IEnumerable<object> selectedItems)
    {
        var selectedClothing = m_FootwearList.selectedItem as ClothingObject;

        if (selectedClothing == null)
        {
            footwearSelected = false;
            ControlConfirmationButton();

            return;
        }

        footwearSelected = true;
        GSM.onFootwearChange.Invoke(selectedClothing);

        ControlConfirmationButton();
    }

    void OnHairSelected(IEnumerable<object> selectedItems)
    {
        var selectedClothing = m_HairsList.selectedItem as ClothingObject;

        if (selectedClothing == null)
        {
            hairSelected = false;
            ControlConfirmationButton();

            return;
        }

        hairSelected = true;
        GSM.onHairChange.Invoke(selectedClothing);

        ControlConfirmationButton();
    }

    void OnBeardSelected(IEnumerable<object> selectedItems)
    {
        var selectedClothing = m_BeardsList.selectedItem as ClothingObject;

        if (selectedClothing == null)
        {
            beardSelected = false;
            ControlConfirmationButton();

            return;
        }

        beardSelected = true;
        GSM.onBeardChange.Invoke(selectedClothing);

        ControlConfirmationButton();
    }

    Wardrobe LoadWardrobeFromJSONFile()
    {
        var JSONFile = Resources.Load<TextAsset>("Clothes_Data");

        // Wardrobe wardrobe = ScriptableObject.CreateInstance("Wardrobe") as Wardrobe;

        // JsonUtility.FromJsonOverwrite(JSONFile.text, wardrobe);

        Wardrobe wardrobe = JsonUtility.FromJson<Wardrobe>(JSONFile.text);

        return wardrobe;
    }

    void ControlConfirmationButton()
    {
        if(topSelected && bottomSelected && footwearSelected)
        {
            m_Button.SetEnabled(true);
        }
        else
        {
            m_Button.SetEnabled(false);
        }
    }

}
