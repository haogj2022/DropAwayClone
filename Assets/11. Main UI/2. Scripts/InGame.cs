using System;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public static InGame Instance;

    [SerializeField] private Button PauseButton;

    public Action OnPauseButtonClicked;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PauseButton.onClick.AddListener(PauseGame);
    }

    private void OnDestroy()
    {
        PauseButton.onClick.RemoveListener(PauseGame);
    }

    private void PauseGame()
    {
        OnPauseButtonClicked();
        Timer.Instance.StopTimer();
    }
}
