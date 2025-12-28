using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    [SerializeField] private RectTransform ShopRect;
    [SerializeField] private TMP_Text CurrentCoinText;
   
    private int CurrentCoin;

    public Action<int> OnCurrentCoinUpdated;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TabManager.Instance.OnShopButtonClicked += OnShopButtonClicked;
        TabManager.Instance.OnHomeButtonClicked += OnHomeButtonClicked;
        TabManager.Instance.OnSettingsButtonClicked += OnSettingsButtonClicked;
    }

    private void OnDestroy()
    {
        TabManager.Instance.OnShopButtonClicked -= OnShopButtonClicked;
        TabManager.Instance.OnHomeButtonClicked -= OnHomeButtonClicked;
        TabManager.Instance.OnSettingsButtonClicked -= OnSettingsButtonClicked;
    }

    public void UpdateCurrentCoin(int amount)
    {
        CurrentCoin += amount;
        CurrentCoinText.text = CurrentCoin.ToString();
        OnCurrentCoinUpdated(CurrentCoin);
    }

    private void OnShopButtonClicked()
    {
        float newLeft = 0;
        ShopRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnHomeButtonClicked()
    {
        float newLeft = -1170;
        ShopRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnSettingsButtonClicked()
    {
        float newLeft = -2340;
        ShopRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }
}
