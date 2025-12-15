using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private Slider TimeLeft;
    [SerializeField] private Image TimeBar;
    [SerializeField] private float StartTime;
    [SerializeField] private bool IsActive;

    private float CurrentTime;
    private int NearTimeUp = 10;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentTime = StartTime;
        Invoke(nameof(AnimateText), StartTime - NearTimeUp);
    }

    private void AnimateText()
    {
        TimeBar.color = Color.red;
        TimerText.color = TimeBar.color;
        TimerText.transform.DOLocalJump(TimerText.transform.localPosition, 20, 1, 1, false).SetEase(Ease.OutExpo).SetLoops(NearTimeUp);
    }

    public void ContinueTimer()
    {
        IsActive = true;
    }

    public void StopTimer()
    {
        IsActive = false;
    }

    private void Update()
    {
        TimeLeft.value = CurrentTime / StartTime;

        if (CurrentTime > 0)
        {
            if (IsActive)
            {
                CurrentTime -= Time.deltaTime;
            }
        }
        else
        {
            CurrentTime = 0;
        }

        int minutes = Mathf.FloorToInt(CurrentTime / 60);
        int seconds = Mathf.FloorToInt(CurrentTime % 60);
        TimerText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }
}
