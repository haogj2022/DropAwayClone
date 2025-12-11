using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] private Button ShopButton;
    [SerializeField] private Button HomeButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private TMP_Text ShopText;
    [SerializeField] private TMP_Text HomeText;
    [SerializeField] private TMP_Text SettingsText;
    [SerializeField] private Image ShopIcon;
    [SerializeField] private Image HomeIcon;
    [SerializeField] private Image SettingsIcon;
    [SerializeField] private Image Highlight;

    private Vector3 ShopIconStartPos;
    private Vector3 HomeIconStartPos;
    private Vector3 SettingsIconStartPos;

    private void Start()
    {
        ShopButton.onClick.AddListener(HighlightShop);
        HomeButton.onClick.AddListener(HighlightHome);
        SettingsButton.onClick.AddListener(HighlightSettings);
        ShopIconStartPos.y = ShopIcon.transform.localPosition.y;
        HomeIconStartPos.y = HomeIcon.transform.localPosition.y;
        SettingsIconStartPos.y = SettingsIcon.transform.localPosition.y;
        HighlightHome();
    }

    private void HighlightShop()
    {
        ShopText.enabled = true;
        HomeText.enabled = false;
        SettingsText.enabled = false;

        ShopIcon.transform.DOLocalMoveY(ShopIconStartPos.y + 50, 0.1f, false);
        ShopIcon.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.1f, RotateMode.Fast);
        ShopIcon.transform.DOScale(new Vector3(1.25f, 1.25f, 1), 0.1f);

        HomeIcon.transform.DOLocalMoveY(HomeIconStartPos.y, 0.1f, false);
        HomeIcon.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast);
        HomeIcon.transform.DOScale(Vector3.one, 0.1f);

        SettingsIcon.transform.DOLocalMoveY(SettingsIconStartPos.y, 0.1f, false);
        SettingsIcon.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast);
        SettingsIcon.transform.DOScale(Vector3.one, 0.1f);

        Highlight.transform.DOLocalMoveX(ShopButton.transform.localPosition.x, 0.1f, false);
    }

    private void HighlightHome()
    {
        ShopText.enabled = false;
        HomeText.enabled = true;
        SettingsText.enabled = false;

        ShopIcon.transform.DOLocalMoveY(ShopIconStartPos.y, 0.1f, false);
        ShopIcon.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast);
        ShopIcon.transform.DOScale(Vector3.one, 0.1f);

        HomeIcon.transform.DOLocalMoveY(HomeIconStartPos.y + 50, 0.1f, false);
        HomeIcon.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.1f, RotateMode.Fast);
        HomeIcon.transform.DOScale(new Vector3(1.25f, 1.25f, 1), 0.1f);

        SettingsIcon.transform.DOLocalMoveY(SettingsIconStartPos.y, 0.1f, false);
        SettingsIcon.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast);
        SettingsIcon.transform.DOScale(Vector3.one, 0.1f);

        Highlight.transform.DOLocalMoveX(HomeButton.transform.localPosition.x, 0.1f, false);
    }

    private void HighlightSettings()
    {
        ShopText.enabled = false;
        HomeText.enabled = false;
        SettingsText.enabled = true;

        ShopIcon.transform.DOLocalMoveY(ShopIconStartPos.y, 0.1f, false);
        ShopIcon.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast);
        ShopIcon.transform.DOScale(Vector3.one, 0.1f);

        HomeIcon.transform.DOLocalMoveY(HomeIconStartPos.y, 0.1f, false);
        HomeIcon.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast);
        HomeIcon.transform.DOScale(Vector3.one, 0.1f);

        SettingsIcon.transform.DOLocalMoveY(SettingsIconStartPos.y + 50, 0.1f, false);
        SettingsIcon.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.1f, RotateMode.Fast);
        SettingsIcon.transform.DOScale(new Vector3(1.25f, 1.25f, 1), 0.1f);

        Highlight.transform.DOLocalMoveX(SettingsButton.transform.localPosition.x, 0.1f, false);
    }
}
