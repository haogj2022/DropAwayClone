using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;

    private Dictionary<Vector2Int, Draggable> DraggableDictionary = new Dictionary<Vector2Int, Draggable>();
    private Dictionary<Vector2Int, BoardTile> BoardTileDictionary = new Dictionary<Vector2Int, BoardTile>();
    private Dictionary<Vector2Int, Consumable> ConsumableDictionary = new Dictionary<Vector2Int, Consumable>();

    private void Awake()
    {
        Instance = this;
    }

    #region Generate Board
    [SerializeField] private int Column, Row;
    [SerializeField] private BoardTile Tile;
    [SerializeField] private GameObject TileContainer;

    private void CreateBoard()
    {
        for (int i = 0; i < Column; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                Vector2Int newGrid = new Vector2Int(i, j);

                GameObject boardTile = PoolingSystem.Spawn(
                    Tile.gameObject,
                    TileContainer.transform,
                    Tile.transform.localScale,
                    GridToWorld(newGrid),
                    Quaternion.identity);

                boardTile.name = $"Tile {newGrid}";

                BoardTile newTile = boardTile.GetComponent<BoardTile>();
                newTile.SetData(new Color(0, 0, 0, 0.5f));
                BoardTileDictionary.Add(newGrid, newTile);
            }
        }
    }
    #endregion Generate Board

    #region Generate Block
    [SerializeField] private Draggable Block;
    [SerializeField] private GameObject BlockContainer;

    private void CreateDraggableBlock()
    {
        Vector2Int redGrid = new Vector2Int(0, 0);
        GameObject redShape = PoolingSystem.Spawn(
            Block.gameObject,
            BlockContainer.transform,
            Block.transform.localScale,
            GridToWorld(redGrid),
            Quaternion.identity);

        redShape.name = $"Red Shape";
        Draggable redDraggable = redShape.GetComponent<Draggable>();
        redDraggable.SetData(new Color(1, 0.25f, 0.25f, 1), redGrid, Shape.L);
        UpdateDraggableDictionary(redGrid, redDraggable);

        Vector2Int yellowGrid = new Vector2Int(2, 2);
        GameObject yellowShape = PoolingSystem.Spawn(
            Block.gameObject,
            BlockContainer.transform,
            Block.transform.localScale,
            GridToWorld(yellowGrid),
            Quaternion.identity);

        yellowShape.name = $"Yellow Shape";
        Draggable yellowDraggable = yellowShape.GetComponent<Draggable>();
        yellowDraggable.SetData(new Color(1, 0.75f, 0, 1), yellowGrid, Shape.Cross);
        UpdateDraggableDictionary(yellowGrid, yellowDraggable);

        Vector2Int blueGrid = new Vector2Int(0, 3);
        GameObject blueShape = PoolingSystem.Spawn(
            Block.gameObject,
            BlockContainer.transform,
            Block.transform.localScale,
            GridToWorld(blueGrid),
            Quaternion.identity);

        blueShape.name = $"Blue Shape";
        Draggable blueDraggable = blueShape.GetComponent<Draggable>();
        blueDraggable.SetData(new Color(0, 0.5f, 1, 1), blueGrid, Shape.O);
        UpdateDraggableDictionary(blueGrid, blueDraggable);

        Vector2Int orangeGrid = new Vector2Int(2, 5);
        GameObject orangeShape = PoolingSystem.Spawn(
            Block.gameObject,
            BlockContainer.transform,
            Block.transform.localScale,
            GridToWorld(orangeGrid),
            Quaternion.identity);

        orangeShape.name = $"Orange Shape";
        Draggable orangeDraggable = orangeShape.GetComponent<Draggable>();
        orangeDraggable.SetData(new Color(1, 0.5f, 0, 1), orangeGrid, Shape.T);
        UpdateDraggableDictionary(orangeGrid, orangeDraggable);
    }

    public void UpdateDraggableDictionary(Vector2Int targetGrid, Draggable draggable)
    {
        if (DraggableDictionary.ContainsKey(targetGrid))
        {
            DraggableDictionary[targetGrid] = draggable;
        }
        else
        {
            DraggableDictionary.Add(targetGrid, draggable);
        }
    }
    #endregion Generate Block

    #region Generate Circle
    [SerializeField] private Consumable Circle;
    [SerializeField] private GameObject CircleContainer;

    private Vector2Int[] RedGrids = new Vector2Int[]
    {
        new(5, 5), new(5, 6), new(2, 9), new(7, 0),
    };

    private Vector2Int[] YellowGrids = new Vector2Int[]
    {
        new(3, 8), new(4, 8), new(5, 8), new(5, 2), new(7, 4)
    };

    private Vector2Int[] BlueGrids = new Vector2Int[]
    {
        new(6, 0), new(7, 1), new(0, 8), new(7, 7)
    };

    private Vector2Int[] OrangeGrids = new Vector2Int[]
    {
        new(4, 0), new(1, 7), new(6, 9), new(5, 4)
    };

    private void CreateConsumableCircle()
    {
        for (int i = 0; i < RedGrids.Length; i++)
        {
            GameObject redCircle = PoolingSystem.Spawn(
            Circle.gameObject,
            CircleContainer.transform,
            Circle.transform.localScale,
            GridToWorld(RedGrids[i]),
            Quaternion.identity);

            redCircle.name = $"Red Circle {RedGrids[i]}";
            Consumable redItem = redCircle.GetComponent<Consumable>();
            redItem.SetData(new Color(1, 0.25f, 0.25f, 1));
            ConsumableDictionary.Add(RedGrids[i], redItem);
        }

        for (int i = 0; i < YellowGrids.Length; i++)
        {
            GameObject yellowCircle = PoolingSystem.Spawn(
            Circle.gameObject,
            CircleContainer.transform,
            Circle.transform.localScale,
            GridToWorld(YellowGrids[i]),
            Quaternion.identity);

            yellowCircle.name = $"Yellow Circle {YellowGrids[i]}";
            Consumable yellowItem = yellowCircle.GetComponent<Consumable>();
            yellowItem.SetData(new Color(1, 0.75f, 0, 1));
            ConsumableDictionary.Add(YellowGrids[i], yellowItem);
        }

        for (int i = 0; i < BlueGrids.Length; i++)
        {
            GameObject blueCircle = PoolingSystem.Spawn(
            Circle.gameObject,
            CircleContainer.transform,
            Circle.transform.localScale,
            GridToWorld(BlueGrids[i]),
            Quaternion.identity);

            blueCircle.name = $"Blue Circle {BlueGrids[i]}";
            Consumable blueItem = blueCircle.GetComponent<Consumable>();
            blueItem.SetData(new Color(0, 0.5f, 1, 1));
            ConsumableDictionary.Add(BlueGrids[i], blueItem);
        }

        for (int i = 0; i < OrangeGrids.Length; i++)
        {
            GameObject orangeCircle = PoolingSystem.Spawn(
            Circle.gameObject,
            CircleContainer.transform,
            Circle.transform.localScale,
            GridToWorld(OrangeGrids[i]),
            Quaternion.identity);

            orangeCircle.name = $"Orange Circle {OrangeGrids[i]}";
            Consumable orangeItem = orangeCircle.GetComponent<Consumable>();
            orangeItem.SetData(new Color(1, 0.5f, 0, 1));
            ConsumableDictionary.Add(OrangeGrids[i], orangeItem);
        }
    }
    #endregion Generate Circle

    #region Handle Event

    private float TileWidth, TileHeight;

    private void Start()
    {
        RectTransform tileSize = Tile.GetComponent<RectTransform>();
        TileWidth = tileSize.rect.width;
        TileHeight = tileSize.rect.height;
        CreateBoard();
        CreateDraggableBlock();
        CreateConsumableCircle();
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

    #region Handle Drag Logic
    [SerializeField] private GameObject EffectContainer;

    private Draggable ShapeToDrag;
    private Draggable Effect;
    private Image[] EffectImages;
    private Vector2Int OffsetGrid;
    private Vector2 Offset;

    private void OnStartTouching(Vector2 mousePos)
    {
        Vector2Int mouseGrid = WorldToGrid(mousePos);

        if (DraggableDictionary.TryGetValue(mouseGrid, out var draggable))
        {
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
        GameObject newEffect = PoolingSystem.Spawn(
            ShapeToDrag.gameObject,
            EffectContainer.transform,
            ShapeToDrag.transform.localScale,
            ShapeToDrag.transform.localPosition,
            Quaternion.identity);

        Effect = newEffect.GetComponent<Draggable>();
        EffectImages = newEffect.GetComponentsInChildren<Image>();

        for (int i = 0; i < EffectImages.Length; i++)
        {
            EffectImages[i].color = new Color(
                ShapeToDrag.ShapeImages[i].color.r,
                ShapeToDrag.ShapeImages[i].color.g,
                ShapeToDrag.ShapeImages[i].color.b, 0.1f);
        }
    }

    private void OnTouching(Vector2 mousePos)
    {
        if (ShapeToDrag != null)
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
    }

    private void OnStopTouching(Vector2 mousePos)
    {
        if (ShapeToDrag != null)
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
                if (ShapeToDrag.GetColor() != consumable.GetColor())
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
