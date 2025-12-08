using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private Slider TimeLeft;
    [SerializeField] private Image TimeBar;
    [SerializeField] private float StartTime;

    private float CurrentTime;
    private int NearTimeUp = 10;

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

    private void Update()
    {
        TimeLeft.value = CurrentTime / StartTime;

        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
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
