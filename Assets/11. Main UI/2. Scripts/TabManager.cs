using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public static TabManager Instance;

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
    private int OffsetY = 60;
    private Vector3 IconRotation = new Vector3(0, 0, 15);
    private Vector3 IconScale = new Vector3(1.2f, 1.2f, 1);
    private float Duration = 0.1f;

    public Action OnShopButtonClicked;
    public Action OnHomeButtonClicked;
    public Action OnSettingsButtonClicked;

    private void Awake()
    {
        Instance = this;
        ShopButton.onClick.AddListener(HighlightShop);
        HomeButton.onClick.AddListener(HighlightHome);
        SettingsButton.onClick.AddListener(HighlightSettings);
        ShopIconStartPos.y = ShopIcon.transform.localPosition.y;
        HomeIconStartPos.y = HomeIcon.transform.localPosition.y;
        SettingsIconStartPos.y = SettingsIcon.transform.localPosition.y;
    }

    private void Start()
    {
        Invoke(nameof(HighlightHome), Duration);
    }

    private void HighlightShop()
    {
        OnShopButtonClicked();

        ShopText.enabled = true;
        HomeText.enabled = false;
        SettingsText.enabled = false;

        ShopIcon.transform.DOLocalMoveY(ShopIconStartPos.y + OffsetY, Duration, false);
        ShopIcon.transform.DOLocalRotate(IconRotation, Duration, RotateMode.Fast);
        ShopIcon.transform.DOScale(IconScale, Duration);

        HomeIcon.transform.DOLocalMoveY(HomeIconStartPos.y, Duration, false);
        HomeIcon.transform.DOLocalRotate(Vector3.zero, Duration, RotateMode.Fast);
        HomeIcon.transform.DOScale(Vector3.one, Duration);

        SettingsIcon.transform.DOLocalMoveY(SettingsIconStartPos.y, Duration, false);
        SettingsIcon.transform.DOLocalRotate(Vector3.zero, Duration, RotateMode.Fast);
        SettingsIcon.transform.DOScale(Vector3.one, Duration);

        Highlight.transform.DOLocalMoveX(ShopButton.transform.localPosition.x, Duration, false);
    }

    private void HighlightHome()
    {
        OnHomeButtonClicked();

        ShopText.enabled = false;
        HomeText.enabled = true;
        SettingsText.enabled = false;

        ShopIcon.transform.DOLocalMoveY(ShopIconStartPos.y, Duration, false);
        ShopIcon.transform.DOLocalRotate(Vector3.zero, Duration, RotateMode.Fast);
        ShopIcon.transform.DOScale(Vector3.one, Duration);

        HomeIcon.transform.DOLocalMoveY(HomeIconStartPos.y + OffsetY, Duration  , false);
        HomeIcon.transform.DOLocalRotate(IconRotation, Duration, RotateMode.Fast);
        HomeIcon.transform.DOScale(IconScale, Duration);

        SettingsIcon.transform.DOLocalMoveY(SettingsIconStartPos.y, Duration, false);
        SettingsIcon.transform.DOLocalRotate(Vector3.zero,  Duration, RotateMode.Fast);
        SettingsIcon.transform.DOScale(Vector3.one, Duration);

        Highlight.transform.DOLocalMoveX(HomeButton.transform.localPosition.x, Duration, false);
    }

    private void HighlightSettings()
    {
        OnSettingsButtonClicked();

        ShopText.enabled = false;
        HomeText.enabled = false;
        SettingsText.enabled = true;

        ShopIcon.transform.DOLocalMoveY(ShopIconStartPos.y, Duration, false);
        ShopIcon.transform.DOLocalRotate(Vector3.zero, Duration, RotateMode.Fast);
        ShopIcon.transform.DOScale(Vector3.one, Duration);

        HomeIcon.transform.DOLocalMoveY(HomeIconStartPos.y, Duration, false);
        HomeIcon.transform.DOLocalRotate(Vector3.zero, Duration, RotateMode.Fast);
        HomeIcon.transform.DOScale(Vector3.one, Duration);

        SettingsIcon.transform.DOLocalMoveY(SettingsIconStartPos.y + OffsetY, Duration, false);
        SettingsIcon.transform.DOLocalRotate(IconRotation, Duration, RotateMode.Fast);
        SettingsIcon.transform.DOScale(IconScale, Duration);

        Highlight.transform.DOLocalMoveX(SettingsButton.transform.localPosition.x, Duration, false);
    }
}
