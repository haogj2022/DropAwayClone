using UnityEngine;
using UnityEngine.UI;

public class BoardTile : MonoBehaviour
{
    [SerializeField] private Image TileImage;

    public void SetData(Color color)
    {
        TileImage.color = color;
    }
}
