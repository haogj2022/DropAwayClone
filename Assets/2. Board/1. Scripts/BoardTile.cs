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
            case ColorIndex.Black:
                TileImage.color = new Color(0, 0, 0, 0.5f);
                break;
        }
    }
}