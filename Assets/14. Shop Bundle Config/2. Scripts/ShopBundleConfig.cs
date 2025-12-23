using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBundleConfig : MonoBehaviour
{
    [SerializeField] private string BundleName;
    [SerializeField] private TMP_Text BundleNameText;
    [SerializeField] private string BundlePrice;
    [SerializeField] private TMP_Text BundlePriceText;
    [SerializeField] private float InfiniteLivesHours;
    [SerializeField] private TMP_Text InfiniteLivesHoursText;
    [SerializeField] private bool RemoveAds;
    [SerializeField] private Button BuyBundleButton;
    [SerializeField] private List<BundleItem> BundleItemList = new();

    private void Start()
    {
        BundleNameText.text = BundleName;
        BundlePriceText.text = BundlePrice;
        BuyBundleButton.onClick.AddListener(BuyBundle);

        foreach (BundleItem item in BundleItemList)
        {
            if (item.AmountText != null)
            {
                if (item.Type == ItemType.Propellers || item.Type == ItemType.Magnets || item.Type == ItemType.TimeFreezes)
                {
                    item.AmountText.text = $"x{item.Amount}";
                    continue;
                }

                item.AmountText.text = item.Amount.ToString();
            }
        }

        if (InfiniteLivesHoursText != null)
        {
            InfiniteLivesHoursText.text = $"{InfiniteLivesHours}h";
        }

        Invoke(nameof(CheckRemoveAds), 0.1f);
    }

    private void CheckRemoveAds()
    {
        if (GameManager.Instance.Data.RemoveAds && RemoveAds)
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
        Debug.Log($"Bought {BundleName} for {BundlePrice}");

        foreach (BundleItem item in BundleItemList)
        {
            GameManager.Instance.UpdatePlayerData(item.Type, item.Amount);
            Debug.Log($"Obtained {item.Amount} {item.Type}");

            if (item.Type == ItemType.Coins)
            {
                Shop.Instance.UpdateCurrentCoin(item.Amount);
            }
        }

        if (InfiniteLivesHours > 0)
        {
            GameManager.Instance.UpdatePlayerData(InfiniteLivesHours);
            Debug.Log($"Obtained {InfiniteLivesHours} hours of Infinite Lives");
        }

        if (RemoveAds)
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
    public ItemType Type;
    public int Amount;
    public TMP_Text AmountText;
}

public enum ItemType
{
    Coins,
    Propellers,
    Magnets,
    TimeFreezes,
}
