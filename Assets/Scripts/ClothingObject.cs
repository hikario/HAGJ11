using UnityEngine;
using UnityEngine.UI;
using System;

// Format for the data file: 
// # Object:
// #   "type": Clothing Article Type from enum ArticleType {Top, Bottom, Hat, Shoes, Accessory},
// #   "name": Name,
// #   "image": Image Location,
// #   "point_allocation":
// #       "point_type": value


[System.Serializable]
public class ClothingObject
{
    public string clothingName;
    public string image;
    public Points point_allocation;
    public string description;
}

[System.Serializable]
public class Points
{
    public int historical;
    public int weeb;
    public int stereotypical;
}