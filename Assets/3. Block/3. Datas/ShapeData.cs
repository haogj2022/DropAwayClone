using System.Collections.Generic;
using UnityEngine;

public static class ShapeData
{
    public static Dictionary<Shape, Vector2Int[]> Cells = new Dictionary<Shape, Vector2Int[]>()
    {
        { Shape.Block,    new Vector2Int[] { new( 0, 0) } },
        { Shape.Cross,    new Vector2Int[] { new( 0, 1), new(-1, 0), new( 0, 0), new( 1, 0), new( 0,-1) } },
        { Shape.I,        new Vector2Int[] { new( 0, 1), new( 0, 0), new( 0,-1)                         } },
        { Shape.I_90,     new Vector2Int[] { new(-1, 0), new( 0, 0), new( 1, 0)                         } },
        { Shape.J,        new Vector2Int[] { new( 0, 2), new( 0, 1), new(-1, 0), new( 0, 0)             } },
        { Shape.J_180,    new Vector2Int[] { new( 0, 0), new( 1, 0), new( 0,-1), new( 0,-2)             } },
        { Shape.J_90_CCW, new Vector2Int[] { new(-2, 0), new(-1, 0), new( 0, 0), new( 0,-1)             } },
        { Shape.J_90_CW,  new Vector2Int[] { new( 0, 1), new( 0, 0), new( 1, 0), new( 2, 0)             } },
        { Shape.L,        new Vector2Int[] { new( 0, 2), new( 0, 1), new( 0, 0), new( 1, 0)             } },
        { Shape.L_180,    new Vector2Int[] { new(-1, 0), new( 0, 0), new( 0,-1), new( 0,-2)             } },
        { Shape.L_90_CCW, new Vector2Int[] { new( 0, 1), new(-2, 0), new(-1, 0), new( 0, 0)             } },
        { Shape.L_90_CW,  new Vector2Int[] { new( 0, 0), new( 1, 0), new( 2, 0), new( 0,-1)             } },
        { Shape.O,        new Vector2Int[] { new( 0, 1), new( 1, 1), new( 0, 0), new( 1, 0)             } },
        { Shape.T,        new Vector2Int[] { new(-1, 0), new( 0, 0), new( 1, 0), new( 0,-1)             } },
        { Shape.T_180,    new Vector2Int[] { new( 0, 1), new(-1, 0), new( 0, 0), new( 1, 0)             } },
        { Shape.T_90_CCW, new Vector2Int[] { new( 0, 1), new( 0, 0), new( 1, 0), new( 0,-1)             } },
        { Shape.T_90_CW,  new Vector2Int[] { new( 0, 1), new(-1, 0), new( 0, 0), new( 0,-1)             } },
    };

    public static Dictionary<Shape, Vector2Int> TextOffset = new Dictionary<Shape, Vector2Int>()
    {
        { Shape.Block,    new Vector2Int( 0, 0) },
        { Shape.Cross,    new Vector2Int( 1, 0) },
        { Shape.I,        new Vector2Int( 0,-1) },
        { Shape.I_90,     new Vector2Int( 1, 0) },
        { Shape.J,        new Vector2Int( 0, 0) },
        { Shape.J_180,    new Vector2Int( 1, 0) },
        { Shape.J_90_CCW, new Vector2Int( 0,-1) },
        { Shape.J_90_CW,  new Vector2Int( 2, 0) },
        { Shape.L,        new Vector2Int( 1, 0) },
        { Shape.L_180,    new Vector2Int( 0,-2) },
        { Shape.L_90_CCW, new Vector2Int( 0, 0) },
        { Shape.L_90_CW,  new Vector2Int( 2, 0) },
        { Shape.O,        new Vector2Int( 1, 0) },
        { Shape.T,        new Vector2Int( 1, 0) },
        { Shape.T_180,    new Vector2Int( 1, 0) },
        { Shape.T_90_CCW, new Vector2Int( 1, 0) },
        { Shape.T_90_CW,  new Vector2Int( 0,-1) },
    };
}

public enum Shape
{
    None,
    Block,
    Cross,
    I,
    I_90,
    J,
    J_180,
    J_90_CCW,
    J_90_CW,
    L,
    L_180,
    L_90_CCW,
    L_90_CW,
    O,
    T,
    T_180,
    T_90_CCW,
    T_90_CW,
}
