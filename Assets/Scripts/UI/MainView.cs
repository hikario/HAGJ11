using UnityEngine;
using UnityEngine.UIElements;

public class MainView : MonoBehaviour
{

    [SerializeField]
    VisualTreeAsset m_ClothingTemplate;

    [SerializeField]
    public Wardrobe wardrobe;

    void OnEnable()
    {
        var uiDoc = GetComponent<UIDocument>();

        // Debug.Log(LoadWardrobeFromJSONFile());
        // LoadClothingFromJSONFile();

        var uiBacker = new UIBacking();
        // uiBacker.InitializeClothingList(uiDoc.rootVisualElement, m_ClothingTemplate, wardrobe);
        uiBacker.InitializeClothingList(uiDoc.rootVisualElement, m_ClothingTemplate);
    }


}
