using System.Collections.Generic;

public static class TilesMaster
{
    public static int collapsedTiles = 0;

    public static TileInfo[] allTiles =
    {
        // 0: NEGATIVE SPACE!
        new TileInfo(0, 10,
            new short[] {-1,2,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1},
            new bool[] {true, true, true, true}),
        // 1: JUST FLOOR!
        new TileInfo(1,30,
            new short[] {-1,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1},
            new bool[] {false, false, false, false}),
        // 2: NW CORNER
        new TileInfo(2,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new bool[] {true, true, false, true}),
        // 3: NORTH WALL
        new TileInfo(3,20,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new bool[] {true, true, false, false}),
        // 4: NE CORNER
        new TileInfo(4,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new bool[] {true, true, true, false}),
        // 5: WEST WALL
        new TileInfo(5,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new bool[] {true, false, false, true}),
        // 6: EAST WALL
        new TileInfo(6,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new bool[] {false, true, true, false}),
        // 7: SW CORNER
        new TileInfo(7,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new bool[] {true, false, true, true}),
        // 8: SOUTH WALL
        new TileInfo(8,20,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new bool[] {false, false, true, true}),
        // 9: SE CORNER
        new TileInfo(9,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new bool[] {false, true, true, true}),
        // 10: SOUTHEAST SLANT
        new TileInfo(10,15,
            new short[] {1,1,1,1,1,0,0,0,0,0,-1,0,-1,-1,0,1,0,2,1,1,1},
            new bool[] {false, true, true, true}),
        // 11: SOUTHWEST SLANT
        new TileInfo(11,15,
            new short[] {1,1,1,1,1,0,0,0,0,0,0,-1,-1,-1,0,0,2,1,1,1,1},
            new bool[] {true, false, true, true}),
        // 12: NORTHEAST SLANT
        new TileInfo(12,15,
            new short[] {1,1,1,1,1,0,0,0,0,0,1,0,-1,1,0,1,0,2,1,1,1},
            new bool[] {true, true, true, false}),
        // 13: NORTHWEST SLANT
        new TileInfo(13,15,
            new short[] {1,1,1,1,1,0,0,0,0,0,0,-1,-1,-1,0,0,2,1,1,1,1},
            new bool[] {true, true, false, true}),
        // 14: SOUTHEAST BRICKS
        new TileInfo(14,10,
            new short[] {1,1,0,0,0,-1,1,1,1,1,2,0,0,2,-1,0,0,0,1,1,1},
            new bool[] {false, false, true, false}),
        // 15: SOUTHWEST BRICKS
        new TileInfo(15,10,
            new short[] {},
            new bool[] {false, false, false, true}),
        // 16: NORTHEAST BRICKS
        new TileInfo(16,10,
            new short[] {},
            new bool[] {false, true, false, false}),
        // 17: NORTHWEST BRICKS
        new TileInfo(17,10,
            new short[] {},
            new bool[] {true, false, false, false}),
        // 18: BRICK FLOOR
        new TileInfo(18,5,
            new short[] {},
            new bool[] {false, false, false, false}),
        // 19: HUMAN BONES!
        new TileInfo(19,7,
            new short[] {},
            new bool[] {false, false, false, false}),
        // 20: MONSTER BONES!
        new TileInfo(20,8,
            new short[] {},
            new bool[] {false, false, false, false})
    };

    public static void EvaluatePercentages(short num, bool reverse=false)
    {
        int a = reverse ? -1 : 1;
        for(int i = 0; i < TilesMaster.allTiles.Length; i++)
        {
            int b = (i == num) ? -1 : 1;
            TilesMaster.allTiles[i].percentage += (short)(a * b);
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


