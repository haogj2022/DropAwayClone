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

    private ColorIndex CurrentColor;
    private Shape CurrentShape;
    private TMP_Text ShapeText;
    private Vector2Int TextGrid;
    private float ImageWidth, ImageHeight;

    public void SetData(ColorIndex currentColor, Vector2Int startGrid, Shape currentShape)
    {
        ChangeColor(currentColor);
        StartGrid = startGrid;
        CurrentShape = currentShape;
        ImageWidth = BaseImage.rectTransform.rect.width;
        ImageHeight = BaseImage.rectTransform.rect.height;
        ShapeGrid = ShapeData.Cells[CurrentShape];
        TextGrid = ShapeData.TextOffset[CurrentShape];
        GridCount = ShapeGrid.Length;
        ClearShape();
        CreateNewShape();
    }

    private void ChangeColor(ColorIndex currentColor)
    {
        CurrentColor = currentColor;

        switch (CurrentColor)
        {
            case ColorIndex.Red:
                BaseImage.color = new Color(1, 0.25f, 0.25f, 1);
                break;
            case ColorIndex.Orange:
                BaseImage.color = new Color(1, 0.5f, 0, 1);
                break;
            case ColorIndex.Yellow:
                BaseImage.color = new Color(1, 0.75f, 0, 1);
                break;
            case ColorIndex.Green:
                BaseImage.color = new Color(0, 0.5f, 0, 1);
                break;
            case ColorIndex.Blue:
                BaseImage.color = new Color(0, 0.5f, 1, 1);
                break;
            case ColorIndex.Purple:
                BaseImage.color = new Color(0.75f, 0, 1, 1);
                break;
            case ColorIndex.Pink:
                BaseImage.color = new Color(1, 0.5f, 1, 1);
                break;
            case ColorIndex.Light_Blue:
                BaseImage.color = new Color(0, 0.75f, 1, 1);
                break;
            case ColorIndex.Cyan:
                BaseImage.color = new Color(0, 1, 0.75f, 1);
                break;
            case ColorIndex.Lime:
                BaseImage.color = new Color(0, 1, 0, 1);
                break;
        }
    }

    public void UpdateStartGrid(Vector2Int newGrid)
    {
        StartGrid = newGrid;
    }

    public ColorIndex GetColorIndex()
    {
        return CurrentColor;
    }

    public Shape GetShape()
    {
        return CurrentShape;
    }

    public void DecreaseGridCount(int amount)
    {
        GridCount -= amount;
        ShapeText.text = $"{GridCount}";
    }

    private void ClearShape()
    {
        for (int i = 0; i < ShapeImages.Count; i++)
        {
            if (ShapeImages[i] != BaseImage)
            {
                Destroy(ShapeImages[i].gameObject);
            }
        }

        ShapeImages.Clear();
    }

    private void CreateNewShape()
    {
        for (int i = 0; i < ShapeGrid.Length; i++)
        {
            if (ShapeGrid[i] != new Vector2Int(0, 0))
            {
                Image newBlock = PoolingSystem.Spawn<Image>(
                    BaseImage.gameObject,
                    transform,
                    BaseImage.transform.localScale,
                    new Vector2(ShapeGrid[i].x * ImageWidth, ShapeGrid[i].y * ImageHeight),
                    Quaternion.identity);

                newBlock.name = $"Block {StartGrid + ShapeGrid[i]}";
                newBlock.color = BaseImage.color;
                ShapeImages.Add(newBlock);
            }

            if (ShapeGrid[i] == new Vector2Int(0, 0))
            {
                BaseImage.name = $"Block {StartGrid}";
                ShapeImages.Add(BaseImage);
            }

            if (ShapeGrid[i] == TextGrid)
            {
                ShapeText = ShapeImages[i].GetComponentInChildren<TMP_Text>();
                ShapeText.text = $"{GridCount}";
            }

            if (ShapeGrid[i] != TextGrid)
            {
                ShapeText = ShapeImages[i].GetComponentInChildren<TMP_Text>();
                ShapeText.text = string.Empty;
            }
        }
    }
}
