using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] private BoardTile Tile;
    [SerializeField] private RectTransform TileContainer;
    [SerializeField] private TMP_InputField WidthInput;
    [SerializeField] private TMP_InputField HeightInput;
    [SerializeField] private Button UpdateGridButton;
    private float TileWidth, TileHeight;
    private int Column, Row;
    private float GridOffset;
    private Dictionary<Vector2Int, BoardTile> BoardTileDictionary = new Dictionary<Vector2Int, BoardTile>();
    private Dictionary<Vector2Int, TileColor> TileColorDictionary = new Dictionary<Vector2Int, TileColor>();

    [Header("Save/Load Level")]
    [SerializeField] private TMP_InputField LevelName;
    [SerializeField] private Button SaveLevel;
    [SerializeField] private Button LoadLevel;

    [Header("Edit")]
    [SerializeField] private Toggle TileToggle;
    [SerializeField] private Toggle EmptyToggle;
    private bool CanEdit;

    private void Start()
    {
        RectTransform tileSize = Tile.GetComponent<RectTransform>();
        TileWidth = tileSize.rect.width;
        TileHeight = tileSize.rect.height;
        GridOffset = TileContainer.transform.localPosition.y;
        UpdateGridButton.onClick.AddListener(UpdateGrid);
        SaveLevel.onClick.AddListener(SaveToJson);
        LoadLevel.onClick.AddListener(LoadFromJson);
    }

    private void OnDisable()
    {
        UpdateGridButton.onClick.RemoveListener(UpdateGrid);
        SaveLevel.onClick.RemoveListener(SaveToJson);
        LoadLevel.onClick.RemoveListener(LoadFromJson);
    }

    private void SaveToJson()
    {
        JsonData data = new JsonData(
            Column,
            Row,
            TileColorDictionary);

        JsonManager.SaveJson(data, LevelName.text);
    }

    private void LoadFromJson()
    {
        JsonData data = JsonManager.LoadJson(LevelName.text);

        if (data != null)
        {
            WidthInput.text = data.Column.ToString();
            HeightInput.text = data.Row.ToString();
            TileColorDictionary = data.TileColorDictionary;
            UpdateGrid();
        }
    }

    private void UpdateGrid()
    {
        if (int.TryParse(WidthInput.text, out int width) && width > 0)
        {
            Column = width;
        }

        if (int.TryParse(HeightInput.text, out int height) && height > 0)
        {
            Row = height;
        }

        ClearGrid();
        CreateGrid();
    }

    private void ClearGrid()
    {
        if (BoardTileDictionary.Count > 0)
        {
            foreach (var boardTile in BoardTileDictionary.Values)
            {
                PoolingSystem.Despawn(Tile.gameObject, boardTile.gameObject);
            }
            BoardTileDictionary.Clear();
            TileColorDictionary.Clear();
        }
    }

    private void CreateGrid()
    {
        for (int i = 0; i < Column; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                Vector2Int newGrid = new Vector2Int(i, j);

                BoardTile newTile = PoolingSystem.Spawn<BoardTile>(
                    Tile.gameObject,
                    TileContainer.transform,
                    Tile.transform.localScale,
                    GridToWorld(newGrid),
                    Quaternion.identity);

                newTile.name = $"Tile {newGrid}";
                newTile.SetData(TileColor.Black);
                BoardTileDictionary.Add(newGrid, newTile);
                TileColorDictionary.Add(newGrid, TileColor.Black);
            }
        }
    }

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

    private bool IsOutOfBound(Vector2 mousePos)
    {
        float boundWidth = TileContainer.rect.width / 2;
        float boundHeight = TileContainer.rect.height / 2;

        return mousePos.x > boundWidth || mousePos.x < -boundWidth || mousePos.y > boundHeight || mousePos.y < -boundHeight;
    }

    private void StartEdit(Vector2 mousePos)
    {
        Vector2Int mouseGrid = WorldToGrid(mousePos);

        if (EmptyToggle.isOn && BoardTileDictionary.TryGetValue(mouseGrid, out var visibleTile))
        {
            PoolingSystem.Despawn(Tile.gameObject, visibleTile.gameObject);
            BoardTileDictionary.Remove(mouseGrid);
            TileColorDictionary.Remove(mouseGrid);
        }

        if (TileToggle.isOn && BoardTileDictionary.ContainsKey(mouseGrid) == false)
        {
            BoardTile newTile = PoolingSystem.Spawn<BoardTile>(
                Tile.gameObject,
                TileContainer.transform,
                Tile.transform.localScale,
                GridToWorld(mouseGrid),
                Quaternion.identity);

            newTile.name = $"Tile {mouseGrid}";
            newTile.SetData(TileColor.Black);
            BoardTileDictionary.Add(mouseGrid, newTile);
            TileColorDictionary.Add(mouseGrid, TileColor.Black);
        }
    }

    private Vector2 GridToWorld(Vector2Int targetGrid)
    {
        float newPosX = (targetGrid.x * TileWidth) - (TileWidth * (Column - 1) / 2);
        float newPosY = (targetGrid.y * TileHeight) - (TileHeight * (Row - 1) / 2);
        return new Vector2(newPosX, newPosY);
    }

    private Vector2Int WorldToGrid(Vector2 targetPos)
    {
        int newGridX = Mathf.RoundToInt((targetPos.x + (TileWidth * (Column - 1) / 2)) / TileWidth);
        int newGridY = Mathf.RoundToInt((targetPos.y + (TileHeight * (Row - 1) / 2)) / TileHeight);
        return new Vector2Int(newGridX, newGridY);
    }
}
