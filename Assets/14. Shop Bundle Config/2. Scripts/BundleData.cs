using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BundleData", menuName = "GameData/ShopBundle")]
public class BundleData : ScriptableObject
{
    public List<Bundle> BundleList;
}

[System.Serializable]
public class Bundle
{
    public string Name;
    public int Price;
    public List<Item> ItemList;
    public bool RemoveAds;
}

[System.Serializable]
public class Item
{
    public ItemType CurrentType;
    public Sprite Image;
    public int Amount;
}