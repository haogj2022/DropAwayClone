using UnityEngine;
using UnityEngine.UI;

public class Consumable : MonoBehaviour
{
    [SerializeField] private Image CircleImage;

    private ColorIndex CurrentColor;

    public void SetData(ColorIndex currentColor)
    {
        CurrentColor = currentColor;

        switch (CurrentColor)
        {
            case ColorIndex.Red:
                CircleImage.color = new Color(1, 0.25f, 0.25f, 1);
                break;
            case ColorIndex.Orange:
                CircleImage.color = new Color(1, 0.5f, 0, 1);
                break;
            case ColorIndex.Yellow:
                CircleImage.color = new Color(1, 0.75f, 0, 1);
                break;
            case ColorIndex.Green:
                CircleImage.color = new Color(0, 0.5f, 0, 1);
                break;
            case ColorIndex.Blue:
                CircleImage.color = new Color(0, 0.5f, 1, 1);
                break;
            case ColorIndex.Purple:
                CircleImage.color = new Color(0.75f, 0, 1, 1);
                break;
            case ColorIndex.Pink:
                CircleImage.color = new Color(1, 0.5f, 1, 1);
                break;
            case ColorIndex.Light_Blue:
                CircleImage.color = new Color(0, 0.75f, 1, 1);
                break;
            case ColorIndex.Cyan:
                CircleImage.color = new Color(0, 1, 0.75f, 1);
                break;
            case ColorIndex.Lime:
                CircleImage.color = new Color(0, 1, 0, 1);
                break;
        }
    }

    public ColorIndex GetColorIndex()
    {
        return CurrentColor;
    }
}
