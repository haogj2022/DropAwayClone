using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] private Button SwitchButton;
    [SerializeField] private Image SwitchImage;
    [SerializeField] private Sprite OnSprite;
    [SerializeField] private Sprite OffSprite;
    [SerializeField] private GameObject SwitchHandle;
    private bool isOn = true;
    private float Offset = 63;

    private void Start()
    {
        SwitchButton.onClick.AddListener(SwitchOnOff);
    }

    private void OnDestroy()
    {
        SwitchButton.onClick.RemoveListener(SwitchOnOff);
    }

    private void SwitchOnOff()
    {
        if (isOn)
        {
            isOn = false;
            SwitchImage.sprite = OffSprite;
            SwitchHandle.transform.DOLocalMoveX(-Offset, 0.1f, false);
        }
        else
        {
            isOn = true;
            SwitchImage.sprite = OnSprite;
            SwitchHandle.transform.DOLocalMoveX(Offset, 0.1f, false);
        }
    }
}
