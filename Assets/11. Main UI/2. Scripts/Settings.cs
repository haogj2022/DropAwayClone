using DG.Tweening;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private RectTransform SettingsRect;

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
        float newLeft = 2340;
        SettingsRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnHomeButtonClicked()
    {
        float newLeft = 1170;
        SettingsRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnSettingsButtonClicked()
    {
        float newLeft = 0;
        SettingsRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }
}
