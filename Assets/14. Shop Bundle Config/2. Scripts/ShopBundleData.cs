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

    public Dictionary<BundleGroup, Color> BundleGroupColors = new Dictionary<BundleGroup, Color>()
    {
        { BundleGroup.Bundles, new Color32(214, 53, 129, 255) },
        { BundleGroup.NoAds, new Color32(172, 59, 206, 255) },
        { BundleGroup.Coins, new Color32(115, 229, 255, 255) },
    };
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