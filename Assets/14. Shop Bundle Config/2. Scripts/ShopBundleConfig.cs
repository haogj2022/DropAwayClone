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
                if (item.Type == ItemType.Propellers || item.Type == ItemType.Magnets || item.Type == ItemType.Time_Freezes)
                {
                    item.AmountText.text += "x";
                }

                item.AmountText.text += item.Amount.ToString();

                if (item.Type == ItemType.Infinite_Lives)
                {
                    item.AmountText.text += "h";
                }
            }
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

            if (item.Type == ItemType.Infinite_Lives)
            {
                Debug.Log($"Obtained {item.Amount} hours of {item.Type}");
                continue;
            }

            Debug.Log($"Obtained {item.Amount} {item.Type}");

            if (item.Type == ItemType.Coins)
            {
                Shop.Instance.UpdateCurrentCoin(item.Amount);
            }
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
    Infinite_Lives,
    Propellers,
    Magnets,
    Time_Freezes,
    No_Ads
}
