using DG.Tweening;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private RectTransform ShopRect;

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

    private void OnShopButtonClicked()
    {
        float newLeft = 0;
        ShopRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnHomeButtonClicked()
    {
        float newLeft = -1080;
        ShopRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnSettingsButtonClicked()
    {
        float newLeft = -2160;
        ShopRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }
}
