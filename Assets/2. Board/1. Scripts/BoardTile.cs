using UnityEngine;
using UnityEngine.UI;

public class BoardTile : MonoBehaviour
{
    [SerializeField] private Image TileImage;

    private ColorIndex CurrentColor;

    public void SetData(ColorIndex newColor)
    {
        CurrentColor = newColor;

        switch (CurrentColor)
        {
            case ColorIndex.Light_Gray:
                TileImage.color = new Color(0.6f, 0.7f, 0.8f, 1);
                break;
        }
    }
}