using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBundleConfig : MonoBehaviour
{
    [SerializeField] private TMP_Text BundleNameText;
    [SerializeField] private TMP_Text BundlePriceText;
    [SerializeField] private Button BuyBundleButton;
    [SerializeField] private GameObject ItemGroup;
    [SerializeField] private Image BundleBackground;
    [SerializeField] private List<BundleItem> BundleItemList;
    private Bundle CurrentBundle;
    private bool RemoveAds;

    private void Start()
    {
        BuyBundleButton.onClick.AddListener(BuyBundle);
    }

    private void OnDestroy()
    {
        BuyBundleButton.onClick.RemoveListener(BuyBundle);
    }

    public void LoadData(Bundle bundle)
    {
        CurrentBundle = bundle;
        BundleNameText.text = bundle.Name;
        BundlePriceText.text = bundle.Price.ToString();

        if (bundle.ItemList.Count > 2)
        {
            ItemGroup.SetActive(true);
        }

        for (int i = 0; i < bundle.ItemList.Count; i++)
        {
            BundleItemList[i].ItemImage.enabled = true;
            BundleItemList[i].ItemImage.sprite = bundle.ItemList[i].ItemSprite;
            BundleItemList[i].ItemImage.SetNativeSize();
            BundleItemList[i].ItemImage.preserveAspect = true;

            switch (bundle.ItemList[i].CurrentItemType)
            {
                case ItemType.Coins:
                    BundleItemList[i].AmountText.text = bundle.ItemList[i].ItemAmount.ToString();
                    break;
                case ItemType.Propellers:
                case ItemType.Magnets:
                case ItemType.TimeFreezes:
                    BundleItemList[i].AmountText.text = $"x{bundle.ItemList[i].ItemAmount}";
                    break;
                case ItemType.InfiniteLivesHours:
                    BundleItemList[i].AmountText.text = $"{bundle.ItemList[i].ItemAmount}h";
                    break;
                case ItemType.RemoveAds:
                    BundleItemList[i].AmountText.text = string.Empty;
                    BundleNameText.text = bundle.Name;
                    break;
            }
        }

        BundleBackground.color = bundle.BundleGroupColors[bundle.Group];

        if (bundle.RemoveAds)
        {
            RemoveAds = true;
        }

        if (GameManager.Instance.Data.RemoveAds && RemoveAds)
        {
            BuyBundleButton.interactable = false;
            BundlePriceText.text = "Purchased";
        }
    }

    private void BuyBundle()
    {
        if (CurrentBundle != null)
        {
            Debug.Log($"Bought {CurrentBundle.Name} for {CurrentBundle.Price}");

            foreach (var item in CurrentBundle.ItemList)
            {
                GameManager.Instance.UpdatePlayerData(item.CurrentItemType, item.ItemAmount);

                if (item.CurrentItemType == ItemType.Coins)
                {
                    Shop.Instance.UpdateCurrentCoin(item.ItemAmount);
                }

                if (item.CurrentItemType == ItemType.InfiniteLivesHours)
                {
                    Debug.Log($"Obtained {item.ItemAmount} hours of {item.CurrentItemType}");
                }
                else if (item.CurrentItemType == ItemType.RemoveAds)
                {
                    Debug.Log("Removed Ads");
                }
                else
                {
                    Debug.Log($"Obtained {item.ItemAmount} {item.CurrentItemType}");
                }
            }
        }

        if (RemoveAds)
        {
            GameManager.Instance.UpdatePlayerData(ItemType.RemoveAds);
            BuyBundleButton.interactable = false;
            BundlePriceText.text = "Purchased";
            
        }
    }
}

[System.Serializable]
public class BundleItem
{
    public Image ItemImage;
    public TMP_Text AmountText;
}
