// using UnityEngine;
// using UnityEngine.UI;
// using System;

// // Format for the data file: 
// // # Object:
// // #   "type": Clothing Article Type from enum ArticleType {Top, Bottom, Hat, Shoes, Accessory},
// // #   "name": Name,
// // #   "image": Image Location,
// // #   "point_allocation":
// // #       "point_type": value
// public enum ArticleType {Top, Bottom, Hat, Shoes, Accessory}

// public class ClothesGameLoadIn : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         var textFile = Resources.Load<TextAsset>("Clothes_Data");

//         var wardrobe = Wardrobe.CreateInstance<Wardrobe>();

//         JsonUtility.FromJsonOverwrite(textFile.text, wardrobe);

//         // Wardrobe is all the clothes in the data JSON.
//         // System.Reflection.FieldInfo[] clothingTypes = wardrobe.GetType().GetFields();
//         // foreach (System.Reflection.FieldInfo clothingType in clothingTypes)
//         // {
//         //     // Create Header
//         //     GameObject header = CreateHeaderObject();
//         //     header.name = "Header" + clothingType.Name;
//         //     header.GetComponent<TMPro.TextMeshProUGUI>().text = clothingType.Name;
//         //     header.transform.SetParent(gameObject.transform);

//         //     // Corresponding body component needs to have the Grid Layout
//         //     GameObject body = CreateBodyObject();
//         //     LoadClothingType((ClothingObject[])clothingType.GetValue(wardrobe), body);
//         //     body.transform.SetParent(gameObject.transform);
//         // }
//     }

//     GameObject CreateBodyObject()
//     {
//         GameObject ngo = new GameObject();

//         ngo.AddComponent<GridLayoutGroup>();
//         ngo.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
//         ngo.GetComponent<GridLayoutGroup>().constraintCount = 3;

//         return ngo;
//     }


//     GameObject CreateImageObject()
//     {
//         GameObject ngo = new GameObject();
//         ngo.AddComponent<CanvasRenderer>();
//         ngo.AddComponent<RectTransform>();
//         ngo.AddComponent<Image>();

//         return ngo;
//     }

//     GameObject CreateHeaderObject()
//     {
//         GameObject ngo = new GameObject();
//         ngo.AddComponent<CanvasRenderer>();
//         ngo.AddComponent<RectTransform>();
//         ngo.AddComponent<TMPro.TextMeshProUGUI>();

//         return ngo;
//     }

//     GameObject CreateTextObject()
//     {
//         GameObject ngo = new GameObject();
//         ngo.AddComponent<CanvasRenderer>();
//         ngo.AddComponent<RectTransform>();
//         ngo.AddComponent<TMPro.TextMeshProUGUI>();

//         // ngo.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);
//         ngo.GetComponent<TMPro.TextMeshProUGUI>().enableAutoSizing = true;

//         return ngo;
//     }

//     void LoadClothingType(ClothingObject[] clothes, GameObject parentObject)
//     {
//         foreach (ClothingObject clothing in clothes)
//         {
//             Debug.Log("Found " + clothing.clothingName);
//             // Now that we have individual clothing pieces, we
//             // create the Game Objects that will be used to
//             // represent the different clothings
//             GameObject ngo = new GameObject();
//             ngo.name = clothing.clothingName;
//             ngo.AddComponent<VerticalLayoutGroup>();
//             ngo.GetComponent<VerticalLayoutGroup>().childControlWidth = true;
//             ngo.GetComponent<VerticalLayoutGroup>().childControlHeight = false;
//             ngo.AddComponent<OnClothingClick>();
            
//             GameObject imageObject = CreateImageObject();
//             imageObject.name = "Image";
//             imageObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(clothing.image);
            
//             GameObject textObject = CreateTextObject();
//             textObject.name = "Text";
//             textObject.GetComponent<TMPro.TextMeshProUGUI>().text = clothing.clothingName;

//             imageObject.transform.SetParent(ngo.transform);
//             textObject.transform.SetParent(ngo.transform);

//             ngo.transform.SetParent(parentObject.transform);
//         }
//     }
// }

// [System.Serializable]
// public class ClothingObject : ScriptableObject
// {
//     public string clothingName;
//     public ArticleType type;
//     public string image;
//     public Points point_allocation;
// }

// [System.Serializable]
// public class Wardrobe : ScriptableObject
// {
//     public ClothingObject[] tops;
//     public ClothingObject[] bottoms;
//     public ClothingObject[] hats;
//     public ClothingObject[] shoes;
// }

// [System.Serializable]
// public class Points
// {
//     public int cool;
//     public int dark;
//     public int fancy;
// }