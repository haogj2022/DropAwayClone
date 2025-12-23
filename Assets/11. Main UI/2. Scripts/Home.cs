using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] private RectTransform HomeRect;
    [SerializeField] private Button PlayButton;
    [SerializeField] private TMP_Text CurrentCoinText;

    private void Start()
    {
        Shop.Instance.OnCurrentCoinUpdated += OnCurrentCoinUpdated;

        TabManager.Instance.OnShopButtonClicked += OnShopButtonClicked;
        TabManager.Instance.OnHomeButtonClicked += OnHomeButtonClicked;
        TabManager.Instance.OnSettingsButtonClicked += OnSettingsButtonClicked;
        PlayButton.onClick.AddListener(PlayGame);
    }

    private void OnDestroy()
    {
        Shop.Instance.OnCurrentCoinUpdated -= OnCurrentCoinUpdated;

        TabManager.Instance.OnShopButtonClicked -= OnShopButtonClicked;
        TabManager.Instance.OnHomeButtonClicked -= OnHomeButtonClicked;
        TabManager.Instance.OnSettingsButtonClicked -= OnSettingsButtonClicked;
        PlayButton.onClick.RemoveListener(PlayGame);
    }

    private void OnCurrentCoinUpdated(int currentCoin)
    {
        CurrentCoinText.text = currentCoin.ToString();
    }

    private void PlayGame()
    {
        LevelLoader.Instance.LoadNextLevel();
    }

    private void OnShopButtonClicked()
    {
        float newLeft = 1170;
        HomeRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnHomeButtonClicked()
    {
        float newLeft = 0;
        HomeRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }

    private void OnSettingsButtonClicked()
    {
        float newLeft = -1170;
        HomeRect.transform.DOLocalMoveX(newLeft, 0.1f, false);
    }
}
