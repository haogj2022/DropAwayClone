using DG.Tweening;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    private Dictionary<Vector2Int, Draggable> DraggableDictionary = new Dictionary<Vector2Int, Draggable>();
    private Dictionary<Vector2Int, BoardTile> BoardTileDictionary = new Dictionary<Vector2Int, BoardTile>();
    private Dictionary<Vector2Int, Consumable> ConsumableDictionary = new Dictionary<Vector2Int, Consumable>();

    #region Handle Event
    private float TileWidth, TileHeight;

    private void Start()
    {
        RectTransform tileSize = Tile.GetComponent<RectTransform>();
        TileWidth = tileSize.rect.width;
        TileHeight = tileSize.rect.height;
        LoadLevel();
        TouchController.Instance.OnStartTouching += OnStartTouching;
        TouchController.Instance.OnTouching += OnTouching;
        TouchController.Instance.OnStopTouching += OnStopTouching;
    }

    private void OnDestroy()
    {
        TouchController.Instance.OnStartTouching -= OnStartTouching;
        TouchController.Instance.OnTouching -= OnTouching;
        TouchController.Instance.OnStopTouching -= OnStopTouching;
    }
    #endregion Handle Event

    #region Generate Level
    [SerializeField] private string LevelName;
    [SerializeField] private BoardTile Tile;
    [SerializeField] private GameObject TileContainer;
    [SerializeField] private Draggable Block;
    [SerializeField] private GameObject BlockContainer;
    [SerializeField] private Consumable Circle;
    [SerializeField] private GameObject CircleContainer;
    private int BoardWidth, BoardHeight;

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

    private void LoadLevel()
    {
        if (LevelLoader.Instance != null)
        {
            LevelName = LevelLoader.Instance.GetCurrentLevel();
        }

        TextAsset level = Resources.Load<TextAsset>(LevelName);

        if (level == null)
        {
            Debug.LogWarning($"Level {LevelName} did not exist");
            return;
        }

        string content = level.text;
        JsonData data = JsonConvert.DeserializeObject<JsonData>(content);

        ClearGrid();
        BoardWidth = data.BoardWidth;
        BoardHeight = data.BoardHeight;

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

    private void CreateBoardTile(Vector2Int newGrid)
    {
        BoardTile newTile = PoolingSystem.Spawn<BoardTile>(
            Tile.gameObject,
            TileContainer.transform,
            Tile.transform.localScale,
            GridToWorld(newGrid),
            Quaternion.identity);

        newTile.name = $"Tile {newGrid}";
        newTile.SetData(ColorIndex.Light_Gray);
        BoardTileDictionary.Add(newGrid, newTile);
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

    #endregion Generate Level

    #region Handle Drag Logic
    [SerializeField] private GameObject EffectContainer;

    private Draggable ShapeToDrag;
    private Draggable Effect;
    private Image[] EffectImages;
    private Vector2Int OffsetGrid;
    private Vector2 Offset;
    private bool CanDrag = true;

    private void OnStartTouching(Vector2 mousePos)
    {
        Vector2Int mouseGrid = WorldToGrid(mousePos);

        if (DraggableDictionary.TryGetValue(mouseGrid, out var draggable) && CanDrag)
        {
            Timer.Instance.ContinueTimer();
            ShapeToDrag = draggable;
            ShapeToDrag.transform.SetAsLastSibling();
            Offset = GridToWorld(mouseGrid) - GridToWorld(ShapeToDrag.StartGrid);
            OffsetGrid = mouseGrid - ShapeToDrag.StartGrid;

            CreateShapeEffect();

            for (int i = 0; i < ShapeToDrag.ShapeGrid.Length; i++)
            {
                DraggableDictionary.Remove(ShapeToDrag.StartGrid + ShapeToDrag.ShapeGrid[i]);
            }
        }
    }

    private void CreateShapeEffect()
    {
        Effect = PoolingSystem.Spawn<Draggable>(
            ShapeToDrag.gameObject,
            EffectContainer.transform,
            ShapeToDrag.transform.localScale,
            ShapeToDrag.transform.localPosition,
            Quaternion.identity);

        EffectImages = Effect.GetComponentsInChildren<Image>();

        for (int i = 0; i < EffectImages.Length; i++)
        {
            EffectImages[i].color = new Color(
                ShapeToDrag.ShapeImages[i].color.r,
                ShapeToDrag.ShapeImages[i].color.g,
                ShapeToDrag.ShapeImages[i].color.b, 0.5f);
        }
    }

    private void OnTouching(Vector2 mousePos)
    {
        if (ShapeToDrag != null && CanDrag)
        {
            Vector2Int mouseGrid = WorldToGrid(mousePos);
            Vector2Int validGrid = GetValidGrid(ShapeToDrag.StartGrid, mouseGrid - OffsetGrid);

            Effect.transform.localPosition = GridToWorld(validGrid);
            ShapeToDrag.transform.localPosition = mousePos - Offset;
            ShapeToDrag.UpdateStartGrid(validGrid);

            for (int i = 0; i < ShapeToDrag.ShapeImages.Count; i++)
            {
                ShapeToDrag.ShapeImages[i].name = $"Block {validGrid + ShapeToDrag.ShapeGrid[i]}";
                EffectImages[i].name = ShapeToDrag.ShapeImages[i].name;
            }
        }

        if (ConsumableDictionary.Count <= 0)
        {
            CanDrag = false;
            Timer.Instance.StopTimer();
        }
    }

    private void OnStopTouching(Vector2 mousePos)
    {
        if (ShapeToDrag != null && CanDrag)
        {
            ShapeToDrag.transform.localPosition = GridToWorld(ShapeToDrag.StartGrid);
            PoolingSystem.Despawn(ShapeToDrag.gameObject, Effect.gameObject);

            if (ShapeToDrag.GridCount > 0)
            {
                for (int i = 0; i < ShapeToDrag.ShapeGrid.Length; i++)
                {
                    DraggableDictionary.Add(ShapeToDrag.StartGrid + ShapeToDrag.ShapeGrid[i], ShapeToDrag);
                }
            }
            ShapeToDrag = null;
        }
    }

    private Vector2Int GetValidGrid(Vector2Int previousGrid, Vector2Int targetGrid)
    {
        if (targetGrid == previousGrid)
        {
            return previousGrid;
        }

        for (int i = 0; i < ShapeToDrag.ShapeGrid.Length; i++)
        {
            Vector2Int newGrid = targetGrid + ShapeToDrag.ShapeGrid[i];

            if (BoardTileDictionary.ContainsKey(newGrid) == false ||
                DraggableDictionary.ContainsKey(newGrid))
            {
                return previousGrid;
            }

            if (ConsumableDictionary.TryGetValue(newGrid, out var consumable))
            {
                if (ShapeToDrag.GetColorIndex() != consumable.GetColorIndex())
                {
                    return previousGrid;
                }

                ConsumableDictionary.Remove(newGrid);
                consumable.transform.SetParent(ShapeToDrag.ShapeImages[i].transform);
                consumable.transform.localPosition = Vector3.zero;
                consumable.transform.DOScale(Vector3.zero, 0.5f);
                ShapeToDrag.DecreaseGridCount(1);
            }
        }

        if (ShapeToDrag.GridCount <= 0)
        {
            ShapeToDrag.transform.DOScale(Vector3.zero, 0.5f);
            Effect.transform.DOScale(Vector3.zero, 0.5f);
        }

        return targetGrid;
    }
    #endregion Handle Drag Logic

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
