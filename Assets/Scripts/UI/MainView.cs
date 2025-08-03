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

        var uiBacker = new UIBacking();

        GameStateManager gsm = GameObject.Find("EventSystem").GetComponent<GameStateManager>();
        uiBacker.InitializeClothingList(uiDoc.rootVisualElement, m_ClothingTemplate, gsm);
    }


}
