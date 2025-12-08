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
    [SerializeField] private Button UpdateButton;
    private float TileWidth, TileHeight;
    private int BoardWidth, BoardHeight;
    private float GridOffset;
    private Dictionary<Vector2Int, BoardTile> BoardTileDictionary = new Dictionary<Vector2Int, BoardTile>();

    [Header("Save/Load Level")]
    [SerializeField] private TMP_InputField LevelName;
    [SerializeField] private Button SaveButton;
    [SerializeField] private Button LoadButton;

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

        UpdateButton.onClick.AddListener(UpdateGrid);
        SaveButton.onClick.AddListener(SaveLevel);
        LoadButton.onClick.AddListener(LoadLevel);
    }

    private void OnDisable()
    {
        UpdateButton.onClick.RemoveListener(UpdateGrid);
        SaveButton.onClick.RemoveListener(SaveLevel);
        LoadButton.onClick.RemoveListener(LoadLevel);
    }

    private void SaveLevel()
    {
        JsonData data = new JsonData();
        data.BoardWidth = BoardWidth;
        data.BoardHeight = BoardHeight;

        JsonManager.SaveJson(data, LevelName.text);
    }

    private void LoadLevel()
    {
        JsonData data = JsonManager.LoadJson(LevelName.text);

        if (data != null)
        {
            WidthInput.text = data.BoardWidth.ToString();
            HeightInput.text = data.BoardHeight.ToString();
            UpdateGrid();
        }
    }

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
        }
    }

    private void CreateGrid()
    {
        for (int i = 0; i < BoardWidth; i++)
        {
            for (int j = 0; j < BoardHeight; j++)
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
        }
    }

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
