using UnityEngine;
using UnityEngine.UI;

public class BoardTile : MonoBehaviour
{
    [SerializeField] private Image TileImage;

    private TileColor CurrentColor;

    public void SetData(TileColor newColor)
    {
        CurrentColor = newColor;

        switch (CurrentColor)
        {
            case TileColor.Black:
                TileImage.color = new Color(0, 0, 0, 0.5f);
                break;
        }
    }
}

public enum TileColor
{
    Black = 0
}