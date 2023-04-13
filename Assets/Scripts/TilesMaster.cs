using System.Collections.Generic;

public static class TilesMaster
{
    public static int collapsedTiles = 0;

    public static TileInfo[] allTiles =
    {
        // 0: NEGATIVE SPACE!
        new TileInfo(0, 20,
            new short[] {-5,1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,2,1,1},
            new bool[] {true, true, true, true}),
        // 1: JUST FLOOR!
        new TileInfo(1,100,
            new short[] {0,-6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,-3,-2,-2},
            new bool[] {false, false, false, false}),
        // 2: NW CORNER
        new TileInfo(2,100,
            new short[] {-1,1,-3,-4,-4,-4,-2,-5,-4,-4,-3,-4,-3,-3,-2,-2,-2,-4,-1,-2,2},
            new bool[] {true, true, false, true}),
        // 3: NORTH WALL
        new TileInfo(3,100,
            new short[] {0,-2,-4,-2,-4,-3,-3,-4,-2,-4,-3,-3,-3,-3,-2,-2,-4,-4,-2,-1,-2},
            new bool[] {true, true, false, false}),
        // 4: NE CORNER
        new TileInfo(4,100,
            new short[] {-1,1,-4,-4,-3,-2,-4,-5,-4,-5,-3,-1,-3,-3,-2,-2,-4,-2,1,2,-2},
            new bool[] {true, true, true, false}),
        // 5: WEST WALL
        new TileInfo(5,100,
            new short[] {0,-2,-4,-3,-3,-4,-2,-4,-3,-4,-3,-3,-3,-3,-2,-3,-3,-3,-2,-1,-2},
            new bool[] {true, false, false, true}),
        // 6: EAST WALL
        new TileInfo(6,100,
            new short[] {0,-2,-4,-3,-3,-2,-4,-4,-3,-4,-3,-3,-3,-3,-3,-2,-3,-3,-2,-2,-1},
            new bool[] {false, true, true, false}),
        // 7: SW CORNER
        new TileInfo(7,100,
            new short[] {-1,1,-4,-3,-5,-4,-4,-2,-4,-5,-3,-3,-1,-3,-2,-4,-2,-2,-1,2,-2},
            new bool[] {true, false, true, true}),
        // 8: SOUTH WALL
        new TileInfo(8,100,
            new short[] {0,-2,-4,-2,-4,-3,-3,-4,-3,-4,-3,-3,-3,-3,-4,-4,-2,-2,-2,-2,-1},
            new bool[] {false, false, true, true}),
        // 9: SE CORNER
        new TileInfo(9,100,
            new short[] {-1,1,-5,-3,-4,-4,-4,-5,-4,-2,-3,-3,-3,-1,-4,-2,-2,-2,1,-2,2},
            new bool[] {false, true, true, true}),
        // 10: SOUTHEAST SLANT
        new TileInfo(10,100,
            new short[] {-2,1,-2,-3,-4,-3,-3,-4,-3,-4,-5,-5,-3,-4,-3,-3,-3,-5,0,-1,0},
            new bool[] {true, true, false, true}),
        // 11: SOUTHWEST SLANT
        new TileInfo(11,100,
            new short[] {-2,1,-4,-3,-2,-3,-3,-4,-3,-4,-5,-5,-4,-3,-3,-3,-5,-3,0,-1,0},
            new bool[] {true, true, true, false}),
        // 12: NORTHEAST SLANT
        new TileInfo(12,100,
            new short[] {-2,1,-4,-3,-4,-3,-3,-2,-3,-4,-3,-4,-5,-5,-3,-5,-3,-3,0,0,-1},
            new bool[] {true, false, true, true}),
        // 13: NORTHWEST SLANT
        new TileInfo(13,100,
            new short[] {-2,1,-4,-3,-4,-3,-3,-4,-3,-2,-4,-3,-5,-5,-5,-3,-3,-3,0,0,-1},
            new bool[] {false, true, true, true}),
        // 14: SOUTHEAST BRICKS
        new TileInfo(14,100,
            new short[] {0,-2,1,1,1,1,0,1,-1,-1,0,0,0,0,-5,-2,-2,-2,-2,-2,-2},
            new bool[] {false, false, true, false}),
        // 15: SOUTHWEST BRICKS
        new TileInfo(15,100,
            new short[] {0,-2,1,1,1,0,1,-1,-1,1,0,0,0,0,-2,-5,-2,-2,-2,-2,-2},
            new bool[] {false, false, false, true}),
        // 16: NORTHEAST BRICKS
        new TileInfo(16,100,
            new short[] {0,-2,1,-1,-1,1,0,1,1,1,0,0,0,0,-2,-2,-5,-2,-2,-2,-2},
            new bool[] {false, true, false, false}),
        // 17: NORTHWEST BRICKS
        new TileInfo(17,100,
            new short[] {0,-2,-1,-1,1,0,1,1,1,1,0,0,0,0,-2,-2,-2,-5,-2,-2,-2},
            new bool[] {true, false, false, false}),
        // 18: BRICK FLOOR
        new TileInfo(18,100,
            new short[] { 2, 0, 1, 2, 1, 2, 2, 1, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, -30,-15,-15},
            new bool[] {false, false, false, false}),
        // 19: HUMAN BONES!
        new TileInfo(19,100,
            new short[] { 2, 0, 3, 1, 3, 0, 0, 3, 1, 3, 2, 2, 2, 2, 2, 2, 2, 2, -15,-30,-15},
            new bool[] {false, false, false, false}),
        // 20: MONSTER BONES!
        new TileInfo(20,100,
            new short[] { 2, 0, 1, 3, 1, 0, 0, 1, 3, 1, 2, 2, 2, 2, 1, 1, 1, 1, -15,-15,-30},
            new bool[] {false, false, false, false})
    };

    public static void EvaluatePercentages(short tIndex, bool reverse=false)
    {
        int a = reverse ? -1 : 1;
        for(int i = 0; i < allTiles.Length; i++)
        {
            allTiles[i].percentage += (short)(allTiles[tIndex].changes[i] * a);
        }
    }
    public static void ResetValues()
    {
        collapsedTiles = 0;
        for(int i = 0; i < allTiles.Length; i++)
        {
            allTiles[i].percentage = 100;
        }
    }
}

public class TileInfo
{
    public short tileIndex;
    public bool[] sockets = new bool[4];
    public short[] changes = new short[21];
    public short percentage;

    public TileInfo(short index, short percent, short[] changelist, bool[] rules)
    {
        this.tileIndex = index;
        this.percentage = percent;
        this.changes = changelist;
        this.sockets = rules;
    }
}


