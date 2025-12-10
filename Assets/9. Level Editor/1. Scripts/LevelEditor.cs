using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    private Dictionary<Vector2Int, BoardTile> BoardTileDictionary = new Dictionary<Vector2Int, BoardTile>();
    private Dictionary<Vector2Int, Draggable> DraggableDictionary = new Dictionary<Vector2Int, Draggable>();
    private Dictionary<Vector2Int, Consumable> ConsumableDictionary = new Dictionary<Vector2Int, Consumable>();

    #region Handle Event
    private void Start()
    {
        RectTransform tileSize = Tile.GetComponent<RectTransform>();
        TileWidth = tileSize.rect.width;
        TileHeight = tileSize.rect.height;
        GridOffset = TileContainer.transform.localPosition.y;

        UpdateButton.onClick.AddListener(UpdateGrid);
        SaveButton.onClick.AddListener(SaveLevel);
        LoadButton.onClick.AddListener(LoadLevel);
        BlockDropdown.onValueChanged.AddListener(SelectShape);

        EmptyToggle.onValueChanged.AddListener(ResetDropdown);
        TileToggle.onValueChanged.AddListener(ResetDropdown);
        CircleToggle.onValueChanged.AddListener(ResetDropdown);
        DeleteShape.onValueChanged.AddListener(ResetDropdown);
    }

    private void OnDisable()
    {
        UpdateButton.onClick.RemoveListener(UpdateGrid);
        SaveButton.onClick.RemoveListener(SaveLevel);
        LoadButton.onClick.RemoveListener(LoadLevel);
        BlockDropdown.onValueChanged.RemoveListener(SelectShape);

        EmptyToggle.onValueChanged.RemoveListener(ResetDropdown);
        TileToggle.onValueChanged.RemoveListener(ResetDropdown);
        CircleToggle.onValueChanged.RemoveListener(ResetDropdown);
        DeleteShape.onValueChanged.RemoveListener(ResetDropdown);
    }
    #endregion Handle Event

    #region Generate Grid Board
    [Header("Board")]
    [SerializeField] private BoardTile Tile;
    [SerializeField] private Draggable Block;
    [SerializeField] private Consumable Circle;
    [SerializeField] private RectTransform TileContainer;
    [SerializeField] private RectTransform BlockContainer;
    [SerializeField] private RectTransform CircleContainer;
    [SerializeField] private TMP_InputField WidthInput;
    [SerializeField] private TMP_InputField HeightInput;
    [SerializeField] private Button UpdateButton;
    private float TileWidth, TileHeight, GridOffset;
    private int BoardWidth, BoardHeight;

    private void UpdateGrid()
    {
        if (int.TryParse(WidthInput.text, out int width) && width > 0)
        {
            BoardWidth = width;
        }

        if (int.TryParse(HeightInput.text, out int height) && height > 0)
        {
            BoardHeight = height;
        }

        ClearGrid();
        CreateNewGrid();
    }

    private void ClearGrid()
    {
        foreach (var boardTile in BoardTileDictionary.Values)
        {
            PoolingSystem.Despawn(Tile.gameObject, boardTile.gameObject);
        }
        BoardTileDictionary.Clear();

        foreach (var draggable in DraggableDictionary.Values)
        {
            PoolingSystem.Despawn(Block.gameObject, draggable.gameObject);
        }
        DraggableDictionary.Clear();

        foreach (var consumable in ConsumableDictionary.Values)
        {
            PoolingSystem.Despawn(Circle.gameObject, consumable.gameObject);
        }
        ConsumableDictionary.Clear();
    }

    private void CreateNewGrid()
    {
        for (int i = 0; i < BoardWidth; i++)
        {
            for (int j = 0; j < BoardHeight; j++)
            {
                CreateBoardTile(new Vector2Int(i, j));
            }
        }
    }

    private void CreateBoardTile(Vector2Int newGrid)
    {
        BoardTile newTile = PoolingSystem.Spawn<BoardTile>(
            Tile.gameObject,
            TileContainer.transform,
            Tile.transform.localScale,
            GridToWorld(newGrid),
            Quaternion.identity);

        newTile.name = $"Tile {newGrid}";
        newTile.SetData(ColorIndex.Black);
        BoardTileDictionary.Add(newGrid, newTile);
    }
    #endregion Generate Grid Board

    #region Save And Load Level
    [Header("Level")]
    [SerializeField] private TMP_InputField NameInput;
    [SerializeField] private Button SaveButton;
    [SerializeField] private Button LoadButton;

    private void SaveLevel()
    {
        JsonData data = new JsonData();
        data.BoardWidth = BoardWidth;
        data.BoardHeight = BoardHeight;

        foreach (var tile in BoardTileDictionary)
        {
            CellData cell = new CellData();
            cell.Column = tile.Key.x;
            cell.Row = tile.Key.y;

            if (DraggableDictionary.ContainsKey(tile.Key) && DraggableDictionary[tile.Key].StartGrid == tile.Key)
            {
                cell.HasBlock = true;
                cell.BlockColor = DraggableDictionary[tile.Key].GetColorIndex();
                cell.BlockShape = DraggableDictionary[tile.Key].GetShape();
            }

            if (ConsumableDictionary.ContainsKey(tile.Key))
            {
                cell.HasCircle = true;
                cell.CircleColor = ConsumableDictionary[tile.Key].GetColorIndex();
            }

            data.BoardTiles.Add(cell);
        }

        JsonManager.SaveJson(data, NameInput.text);
    }

    private void LoadLevel()
    {
        JsonData data = JsonManager.LoadJson(NameInput.text);

        if (data == null)
        {
            return;
        }

        ClearGrid();
        BoardWidth = data.BoardWidth;
        BoardHeight = data.BoardHeight;
        WidthInput.text = BoardWidth.ToString();
        HeightInput.text = BoardHeight.ToString();

        for (int i = 0; i < data.BoardTiles.Count; i++)
        {
            Vector2Int newGrid = new Vector2Int(data.BoardTiles[i].Column, data.BoardTiles[i].Row);
            CreateBoardTile(newGrid);

            if (data.BoardTiles[i].HasBlock)
            {
                CreateShape(newGrid, data.BoardTiles[i].BlockColor, data.BoardTiles[i].BlockShape);
            }

            if (data.BoardTiles[i].HasCircle)
            {
                CreateCircle(newGrid, data.BoardTiles[i].CircleColor);
            }
        }
    }
    #endregion Save And Load Level

    #region Select Edit Option
    [Header("Edit")]
    [SerializeField] private Toggle TileToggle;
    [SerializeField] private Toggle EmptyToggle;
    [SerializeField] private Toggle CircleToggle;
    [SerializeField] private Toggle DeleteShape;
    [SerializeField] private TMP_Dropdown BlockDropdown;
    private bool CanEdit;
    private bool CanPlaceShape;
    private Shape CurrentShape = Shape.None;

    [Header("Color")]
    [SerializeField] private Toggle[] ColorToggles;
    private ColorIndex CurrentColor = ColorIndex.White;

    private void Update()
    {
        Vector2 mousePos = new Vector2(
            Input.mousePosition.x - Screen.width / 2,
            Input.mousePosition.y - GridOffset - Screen.height / 2);

        if (IsOutOfBound(mousePos))
        {
            CanEdit = false;
        }
        else
        {
            CanEdit = true;
        }

        if (Input.GetMouseButton(0) && CanEdit)
        {
            StartEdit(mousePos);
        }
    }

    private void StartEdit(Vector2 mousePos)
    {
        Vector2Int mouseGrid = WorldToGrid(mousePos);

        if (EmptyToggle.isOn && BoardTileDictionary.TryGetValue(mouseGrid, out var visibleTile))
        {
            PoolingSystem.Despawn(Tile.gameObject, visibleTile.gameObject);
            BoardTileDictionary.Remove(mouseGrid);
            RemoveShape(mouseGrid);
        }

        if (TileToggle.isOn && BoardTileDictionary.ContainsKey(mouseGrid) == false)
        {
            CreateBoardTile(mouseGrid);
        }

        if (DeleteShape.isOn)
        {
            RemoveShape(mouseGrid);
        }

        SelectColor();

        if (BoardTileDictionary.ContainsKey(mouseGrid) &&
            DraggableDictionary.ContainsKey(mouseGrid) == false &&
            ConsumableDictionary.ContainsKey(mouseGrid) == false)
        {
            if (CircleToggle.isOn && CurrentColor != ColorIndex.White)
            {
                CreateCircle(mouseGrid, CurrentColor);
            }
        }

        if (BlockDropdown.value != 0 && CurrentColor != ColorIndex.White && CurrentShape != Shape.None)
        {
            CanPlaceShape = true;

            Vector2Int[] shapeGrid = ShapeData.Cells[CurrentShape];

            for (int i = 0; i < shapeGrid.Length; i++)
            {
                if (BoardTileDictionary.ContainsKey(mouseGrid + shapeGrid[i]) == false ||
                    DraggableDictionary.ContainsKey(mouseGrid + shapeGrid[i]) ||
                    ConsumableDictionary.ContainsKey(mouseGrid + shapeGrid[i]))
                {
                    CanPlaceShape = false;
                    break;
                }
            }

            if (CanPlaceShape)
            {
                CreateShape(mouseGrid, CurrentColor, CurrentShape);
            }
        }
    }
    #endregion Select Edit Option

    #region Edit Level
    private bool IsOutOfBound(Vector2 mousePos)
    {
        float boundWidth = TileContainer.rect.width / 2;
        float boundHeight = TileContainer.rect.height / 2;

        return mousePos.x > boundWidth || mousePos.x < -boundWidth || mousePos.y > boundHeight || mousePos.y < -boundHeight;
    }
    private void SelectShape(int value)
    {
        if (value != 0)
        {
            EmptyToggle.isOn = false;
            TileToggle.isOn = false;
            CircleToggle.isOn = false;
            DeleteShape.isOn = false;
        }

        BlockDropdown.value = value;
        CurrentShape = (Shape)value;
    }

    private void ResetDropdown(bool isToggleOn)
    {
        if (isToggleOn)
        {
            BlockDropdown.value = 0;
            CurrentShape = Shape.None;
        }
    }

    private void CreateShape(Vector2Int newGrid, ColorIndex currentColor, Shape currentShape)
    {
        Draggable newBlock = PoolingSystem.Spawn<Draggable>(
            Block.gameObject,
            BlockContainer.transform,
            Block.transform.localScale,
            GridToWorld(newGrid),
            Quaternion.identity);

        newBlock.name = $"{currentColor} {currentShape}";
        newBlock.SetData(currentColor, newGrid, currentShape);

        for (int i = 0; i < newBlock.ShapeGrid.Length; i++)
        {
            DraggableDictionary.Add(newBlock.StartGrid + newBlock.ShapeGrid[i], newBlock);
        }
    }

    private void CreateCircle(Vector2Int newGrid, ColorIndex currentColor)
    {
        Consumable newCircle = PoolingSystem.Spawn<Consumable>(
            Circle.gameObject,
            CircleContainer.transform,
            Circle.transform.localScale,
            GridToWorld(newGrid),
            Quaternion.identity);

        newCircle.name = $"{currentColor} Circle {newGrid}";
        newCircle.SetData(currentColor);
        ConsumableDictionary.Add(newGrid, newCircle);
    }

    private void SelectColor()
    {
        for (int i = 0; i < ColorToggles.Length; i++)
        {
            if (ColorToggles[i].isOn)
            {
                CurrentColor = (ColorIndex)i;
                break;
            }

            if (ColorToggles[i].isOn == false && i == ColorToggles.Length)
            {
                CurrentColor = ColorIndex.White;
            }
        }
    }

    private void RemoveShape(Vector2Int newGrid)
    {
        if (DraggableDictionary.TryGetValue(newGrid, out var visibleBlock))
        {
            PoolingSystem.Despawn(Block.gameObject, visibleBlock.gameObject);

            for (int i = 0; i < visibleBlock.ShapeGrid.Length; i++)
            {
                DraggableDictionary.Remove(visibleBlock.StartGrid + visibleBlock.ShapeGrid[i]);
            }
        }

        if (ConsumableDictionary.TryGetValue(newGrid, out var visibleCircle))
        {
            PoolingSystem.Despawn(Circle.gameObject, visibleCircle.gameObject);
            ConsumableDictionary.Remove(newGrid);
        }
    }
    #endregion Edit Level

    private Vector2 GridToWorld(Vector2Int targetGrid)
    {
        float newPosX = (targetGrid.x * TileWidth) - (TileWidth * (BoardWidth - 1) / 2);
        float newPosY = (targetGrid.y * TileHeight) - (TileHeight * (BoardHeight - 1) / 2);
        return new Vector2(newPosX, newPosY);
    }

    private Vector2Int WorldToGrid(Vector2 targetPos)
    {
        int newGridX = Mathf.RoundToInt((targetPos.x + (TileWidth * (BoardWidth - 1) / 2)) / TileWidth);
        int newGridY = Mathf.RoundToInt((targetPos.y + (TileHeight * (BoardHeight - 1) / 2)) / TileHeight);
        return new Vector2Int(newGridX, newGridY);
    }
}
