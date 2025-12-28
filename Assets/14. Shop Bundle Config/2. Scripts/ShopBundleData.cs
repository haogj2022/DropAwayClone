using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopBundleData", menuName = "GameData/ShopBundleData")]
public class ShopBundleData : ScriptableObject
{
    public List<Bundle> BundleList;
}

[System.Serializable]
public class Bundle
{
    public string Name;
    public int Price;
    public BundleGroup Group;
    public List<Item> ItemList;
    public bool RemoveAds;
}

[System.Serializable]
public class Item
{
    public ItemType CurrentItemType;
    public Sprite ItemSprite;
    public int ItemAmount;
}

public enum ItemType
{
    Coins,
    Propellers,
    Magnets,
    TimeFreezes,
    InfiniteLivesHours,
    RemoveAds,
}

public enum BundleGroup
{
    Bundles,
    NoAds,
    Coins,
}