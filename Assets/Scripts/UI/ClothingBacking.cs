using UnityEngine;
using UnityEngine.UIElements;

public class ClothingBacking
{
    Image m_ImageTile;
    Label m_NameSelector;

    public void SetVisualElement(VisualElement visualElement)
    {
        m_ImageTile = visualElement.Q<Image>("ImageTile");
        m_NameSelector = visualElement.Q<Label>("NameSelector");
    }


    // Set the clothing parameters from a clothing object
    // This means set the background on the label and
    // the name on the button.
    public void SetClothingData(ClothingObject clothing)
    {
        m_ImageTile.image = Resources.Load<Texture2D>(clothing.image);
        m_NameSelector.text = clothing.clothingName;
    }

}
