using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Draggable : MonoBehaviour
{
    [SerializeField] private Image BaseImage;

    public int GridCount { get; private set; }
    public Vector2Int StartGrid { get; private set; }
    public Vector2Int[] ShapeGrid { get; private set; }
    public List<Image> ShapeImages { get; private set; } = new List<Image>();

    private Shape CurrentShape;
    private TMP_Text ShapeText;
    private Vector2Int TextGrid;
    private float ImageWidth, ImageHeight;

    public void SetData(Color color, Vector2Int startGrid, Shape currentShape)
    {
        BaseImage.color = color;
        StartGrid = startGrid;
        CurrentShape = currentShape;
        ImageWidth = BaseImage.rectTransform.rect.width;
        ImageHeight = BaseImage.rectTransform.rect.height;
        ShapeGrid = Data.Cells[CurrentShape];
        TextGrid = Data.TextOffset[CurrentShape];
        GridCount = ShapeGrid.Length;
        CreateShape();
    }

    public void UpdateStartGrid(Vector2Int newGrid)
    {
        StartGrid = newGrid;
    }

    public Color GetColor()
    {
        return BaseImage.color;
    }

    public void DecreaseGridCount(int amount)
    {
        GridCount -= amount;
        ShapeText.text = $"{GridCount}";
    }

    private void CreateShape()
    {
        for (int i = 0; i < ShapeGrid.Length; i++)
        {
            BoardController.Instance.UpdateDraggableDictionary(StartGrid + ShapeGrid[i], this);

            if (ShapeGrid[i] == new Vector2Int(0, 0))
            {
                BaseImage.name = $"Block {StartGrid}";
                ShapeImages.Add(BaseImage);
                continue;
            }

            GameObject newBlock = PoolingSystem.Spawn(
                BaseImage.gameObject,
                transform,
                BaseImage.transform.localScale,
                new Vector2(ShapeGrid[i].x * ImageWidth, ShapeGrid[i].y * ImageHeight),
                Quaternion.identity);

            newBlock.name = $"Block {StartGrid + ShapeGrid[i]}";

            Image newImage = newBlock.GetComponent<Image>();
            newImage.color = BaseImage.color;
            ShapeImages.Add(newImage);

            if (ShapeGrid[i] == TextGrid)
            {
                ShapeText = ShapeImages[i].GetComponentInChildren<TMP_Text>();
                ShapeText.text = $"{GridCount}";
            }
        }
    }
}
