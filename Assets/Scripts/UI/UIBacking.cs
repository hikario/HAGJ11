using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class UIBacking
{
    VisualTreeAsset m_ClothingTemplate;

    ListView m_TopsList;
    ListView m_BottomsList;
    ListView m_FootwearList;
    ListView m_HatsList;

    Button m_Button;

    bool topSelected;
    bool bottomSelected;
    bool footwearSelected;
    bool hatSelected;

    public void InitializeClothingList(VisualElement root, VisualTreeAsset clothingTemplate)//, Wardrobe wardrobe)
    {
        Wardrobe wardrobe = LoadWardrobeFromJSONFile();

        m_ClothingTemplate = clothingTemplate;

        m_TopsList = root.Q<ListView>("Tops");
        m_BottomsList = root.Q<ListView>("Bottoms");
        m_FootwearList = root.Q<ListView>("Footwear");
        m_HatsList = root.Q<ListView>("Hats");

        m_Button = root.Q<Button>("ConfirmButton");

        m_Button.RegisterCallback<ClickEvent>(OnClickCallback);

        FillTopsList(wardrobe.tops);
        FillBottomsList(wardrobe.bottoms);
        FillFootwearList(wardrobe.footwear);
        FillHatsList(wardrobe.hats);

        m_TopsList.selectionChanged += OnTopSelected;
        m_BottomsList.selectionChanged += OnBottomSelected;
        m_FootwearList.selectionChanged += OnFootwearSelected;
        m_HatsList.selectionChanged += OnHatSelected;

        topSelected = false;
        bottomSelected = false;
        footwearSelected = false;
        hatSelected = false;

        m_Button.SetEnabled(false);
    }

    public void OnDisable()
    {
        m_TopsList.selectionChanged -= OnTopSelected;
        m_BottomsList.selectionChanged -= OnBottomSelected;
        m_FootwearList.selectionChanged -= OnFootwearSelected;
        m_HatsList.selectionChanged -= OnHatSelected;

        m_Button.UnregisterCallback<ClickEvent>(OnClickCallback);
    }

    void OnClickCallback(ClickEvent evt)
    {
        GameStateManager.onSumButtonClick.Invoke();
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

    void FillHatsList(List<ClothingObject> clothingList)
    {
        FillClothingList(m_HatsList, clothingList);
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
        GameStateManager.onTopChange.Invoke(selectedClothing);

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
        GameStateManager.onBottomChange.Invoke(selectedClothing);

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
        GameStateManager.onFootwearChange.Invoke(selectedClothing);

        ControlConfirmationButton();
    }

    void OnHatSelected(IEnumerable<object> selectedItems)
    {
        var selectedClothing = m_HatsList.selectedItem as ClothingObject;

        if (selectedClothing == null)
        {
            hatSelected = false;
            ControlConfirmationButton();

            return;
        }

        hatSelected = true;
        GameStateManager.onHatChange.Invoke(selectedClothing);

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
        if(topSelected && bottomSelected && footwearSelected && hatSelected)
        {
            m_Button.SetEnabled(true);
        }
        else
        {
            m_Button.SetEnabled(false);
        }
    }

}
