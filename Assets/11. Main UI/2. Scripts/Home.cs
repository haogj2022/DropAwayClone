using DG.Tweening;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private RectTransform HomeRect;

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
        float newLeft = 1080;
        HomeRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnHomeButtonClicked()
    {
        float newLeft = 0;
        HomeRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnSettingsButtonClicked()
    {
        float newLeft = -1080;
        HomeRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }
}
