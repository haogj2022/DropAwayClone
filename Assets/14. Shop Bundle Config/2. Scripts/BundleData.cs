using UnityEngine;

[CreateAssetMenu(fileName = "BundleData", menuName = "GameData/ShopBundle")]
public class BundleData : ScriptableObject
{
    public string BundleName;
    public int BundlePrice;
    public int Coins;
    public int Propellers;
    public int Magnets;
    public int TimeFreezes;
    public float InfiniteLivesHours;
    public bool RemoveAds;
}
