using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button CloseButton;
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject Panel;

    private void Start()
    {
        CloseButton.onClick.AddListener(ClosePauseMenu);
        InGame.Instance.OnPauseButtonClicked += OnPauseButtonClicked;
    }

    private void OnDestroy()
    {
        CloseButton.onClick.RemoveListener(ClosePauseMenu);
        InGame.Instance.OnPauseButtonClicked -= OnPauseButtonClicked;
    }

    private void OnPauseButtonClicked()
    {
        Background.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        Background.SetActive(false);
    }
}
