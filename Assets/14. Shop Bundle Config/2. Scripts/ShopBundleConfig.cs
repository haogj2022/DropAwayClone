using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBundleConfig : MonoBehaviour
{
    [SerializeField] private BundleData Data;
    [SerializeField] private TMP_Text BundleNameText;
    [SerializeField] private TMP_Text BundlePriceText;
    [SerializeField] private Button BuyBundleButton;
    [SerializeField] private List<BundleItem> BundleItemList;

    private void Start()
    {
        BundleNameText.text = Data.BundleName;
        BundlePriceText.text = $"VND {Data.BundlePrice}";
        BuyBundleButton.onClick.AddListener(BuyBundle);

        foreach (BundleItem item in BundleItemList)
        {
            switch (item.CurrentType)
            {
                case ItemType.Coins:
                    item.AmountText.text = Data.Coins.ToString();
                    break;
                case ItemType.Propellers:
                    item.AmountText.text = $"x{Data.Propellers}";
                    break;
                case ItemType.Magnets:
                    item.AmountText.text = $"x{Data.Magnets}";
                    break;
                case ItemType.TimeFreezes:
                    item.AmountText.text = $"x{Data.TimeFreezes}";
                    break;
                case ItemType.InfiniteLivesHours:
                    item.AmountText.text = $"{Data.InfiniteLivesHours}h";
                    break;
            }
        }

        if (GameManager.Instance.Data.RemoveAds && Data.RemoveAds)
        {
            BuyBundleButton.interactable = false;
            BundlePriceText.text = "Purchased";
        }
    }

    private void OnDestroy()
    {
        BuyBundleButton.onClick.RemoveListener(BuyBundle);
    }

    private void BuyBundle()
    {
        Debug.Log($"Bought {Data.BundleName} for VND {Data.BundlePrice}");

        foreach (BundleItem item in BundleItemList)
        {
            switch (item.CurrentType)
            {
                case ItemType.Coins:
                    Shop.Instance.UpdateCurrentCoin(Data.Coins);
                    GameManager.Instance.UpdatePlayerData(item.CurrentType, Data.Coins);
                    Debug.Log($"Obtained {Data.Coins} {item.CurrentType}");
                    break;
                case ItemType.Propellers:
                    GameManager.Instance.UpdatePlayerData(item.CurrentType, Data.Propellers);
                    Debug.Log($"Obtained {Data.Propellers} {item.CurrentType}");
                    break;
                case ItemType.Magnets:
                    GameManager.Instance.UpdatePlayerData(item.CurrentType, Data.Magnets);
                    Debug.Log($"Obtained {Data.Magnets} {item.CurrentType}");
                    break;
                case ItemType.TimeFreezes:
                    GameManager.Instance.UpdatePlayerData(item.CurrentType, Data.TimeFreezes);
                    Debug.Log($"Obtained {Data.TimeFreezes} {item.CurrentType}");
                    break;
                case ItemType.InfiniteLivesHours:
                    GameManager.Instance.UpdatePlayerData(Data.InfiniteLivesHours);
                    Debug.Log($"Obtained {Data.InfiniteLivesHours} hours of {item.CurrentType}");
                    break;
            }
        }

        if (Data.RemoveAds)
        {
            GameManager.Instance.UpdatePlayerData();
            Debug.Log($"Removed Ads");
            BuyBundleButton.interactable = false;
            BundlePriceText.text = "Purchased";
        }
    }
}

[System.Serializable]
public class BundleItem
{
    public ItemType CurrentType;
    public TMP_Text AmountText;
}

public enum ItemType
{
    Coins,
    Propellers,
    Magnets,
    TimeFreezes,
    InfiniteLivesHours,
}
