using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static Dictionary<Shape, Vector2Int[]> Cells = new Dictionary<Shape, Vector2Int[]>()
    {
        { Shape.Block,                   new Vector2Int[] { new( 0, 0) } },
        { Shape.Cross,                   new Vector2Int[] { new( 0, 1), new(-1, 0), new( 0, 0), new( 1, 0), new( 0,-1) } },
        { Shape.I,                       new Vector2Int[] { new( 0, 1), new( 0, 0), new( 0,-1)                         } },
        { Shape.I_Rotate_90,             new Vector2Int[] { new(-1, 0), new( 0, 0), new( 1, 0)                         } },
        { Shape.J,                       new Vector2Int[] { new( 0, 2), new( 0, 1), new(-1, 0), new( 0, 0)             } },
        { Shape.J_Rotate_180,            new Vector2Int[] { new( 0, 0), new( 1, 0), new( 0,-1), new( 0,-2)             } },
        { Shape.J_Rotate_90_Left,        new Vector2Int[] { new(-2, 0), new(-1, 0), new( 0, 0), new( 0,-1)             } },
        { Shape.J_Rotate_90_Right,       new Vector2Int[] { new( 0, 1), new( 0, 0), new( 1, 0), new( 2, 0)             } },
        { Shape.L,                       new Vector2Int[] { new( 0, 2), new( 0, 1), new( 0, 0), new( 1, 0)             } },
        { Shape.L_Rotate_180,            new Vector2Int[] { new(-1, 0), new( 0, 0), new( 0,-1), new( 0,-2)             } },
        { Shape.L_Rotate_90_Left,        new Vector2Int[] { new( 0, 1), new(-2, 0), new(-1, 0), new( 0, 0)             } },
        { Shape.L_Rotate_90_Right,       new Vector2Int[] { new( 0, 0), new( 1, 0), new( 2, 0), new( 0,-1)             } },
        { Shape.O,                       new Vector2Int[] { new( 0, 1), new( 1, 1), new( 0, 0), new( 1, 0)             } },
        { Shape.Short_I,                 new Vector2Int[] { new( 0, 0), new( 0, 1)                                     } },
        { Shape.Short_I_Rotate_90,       new Vector2Int[] { new( 0, 0), new( 1, 0)                                     } },
        { Shape.Short_L,                 new Vector2Int[] { new( 0, 1), new( 0, 0), new( 1, 0)                         } },
        { Shape.Short_L_Rotate_180,      new Vector2Int[] { new(-1, 0), new( 0, 0), new( 0,-1)                         } },
        { Shape.Short_L_Rotate_90_Left,  new Vector2Int[] { new( 0, 1), new(-1, 0), new( 0, 0)                         } },
        { Shape.Short_L_Rotate_90_Right, new Vector2Int[] { new( 0, 0), new( 1, 0), new( 0,-1)                         } },
        { Shape.T,                       new Vector2Int[] { new(-1, 0), new( 0, 0), new( 1, 0), new( 0,-1)             } },
        { Shape.T_Rotate_180,            new Vector2Int[] { new( 0, 1), new(-1, 0), new( 0, 0), new( 1, 0)             } },
        { Shape.T_Rotate_90_Left,        new Vector2Int[] { new( 0, 1), new( 0, 0), new( 1, 0), new( 0,-1)             } },
        { Shape.T_Rotate_90_Right,       new Vector2Int[] { new( 0, 1), new(-1, 0), new( 0, 0), new( 0,-1)             } },
    };

    public static Dictionary<Shape, Vector2Int> TextOffset = new Dictionary<Shape, Vector2Int>()
    {
        { Shape.Block,                   new Vector2Int( 0, 0) },
        { Shape.Cross,                   new Vector2Int( 1, 0) },
        { Shape.I,                       new Vector2Int( 0,-1) },
        { Shape.I_Rotate_90,             new Vector2Int( 1, 0) },
        { Shape.J,                       new Vector2Int( 0, 0) },
        { Shape.J_Rotate_180,            new Vector2Int( 1, 0) },
        { Shape.J_Rotate_90_Left,        new Vector2Int( 0,-1) },
        { Shape.J_Rotate_90_Right,       new Vector2Int( 2, 0) },
        { Shape.L,                       new Vector2Int( 1, 0) },
        { Shape.L_Rotate_180,            new Vector2Int( 0,-2) },
        { Shape.L_Rotate_90_Left,        new Vector2Int( 0, 0) },
        { Shape.L_Rotate_90_Right,       new Vector2Int( 2, 0) },
        { Shape.O,                       new Vector2Int( 1, 0) },
        { Shape.Short_I,                 new Vector2Int( 0, 0) },
        { Shape.Short_I_Rotate_90,       new Vector2Int( 1, 0) },
        { Shape.Short_L,                 new Vector2Int( 1, 0) },
        { Shape.Short_L_Rotate_180,      new Vector2Int( 0,-1) },
        { Shape.Short_L_Rotate_90_Left,  new Vector2Int( 0, 0) },
        { Shape.Short_L_Rotate_90_Right, new Vector2Int( 1, 0) },
        { Shape.T,                       new Vector2Int( 1, 0) },
        { Shape.T_Rotate_180,            new Vector2Int( 1, 0) },
        { Shape.T_Rotate_90_Left,        new Vector2Int( 1, 0) },
        { Shape.T_Rotate_90_Right,       new Vector2Int( 0,-1) }
    };
}

public enum Shape
{
    None,
    Block,
    I,
    J,
    L,
    O,
    T,
    Cross,
    
    I_Rotate_90,
    J_Rotate_180,
    J_Rotate_90_Left,
    J_Rotate_90_Right,
    L_Rotate_180,
    L_Rotate_90_Left,
    L_Rotate_90_Right,
    Short_I,
    Short_I_Rotate_90,
    Short_L,
    Short_L_Rotate_180,
    Short_L_Rotate_90_Left,
    Short_L_Rotate_90_Right,
    T_Rotate_180,
    T_Rotate_90_Left,
    T_Rotate_90_Right,
}
