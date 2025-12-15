using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] private RectTransform HomeRect;
    [SerializeField] private Button PlayButton;

    private void Start()
    {
        TabManager.Instance.OnShopButtonClicked += OnShopButtonClicked;
        TabManager.Instance.OnHomeButtonClicked += OnHomeButtonClicked;
        TabManager.Instance.OnSettingsButtonClicked += OnSettingsButtonClicked;
        PlayButton.onClick.AddListener(PlayGame);
    }

    private void OnDestroy()
    {
        TabManager.Instance.OnShopButtonClicked -= OnShopButtonClicked;
        TabManager.Instance.OnHomeButtonClicked -= OnHomeButtonClicked;
        TabManager.Instance.OnSettingsButtonClicked -= OnSettingsButtonClicked;
        PlayButton.onClick.RemoveListener(PlayGame);
    }

    private void PlayGame()
    {
        LevelLoader.Instance.LoadNextLevel();
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
