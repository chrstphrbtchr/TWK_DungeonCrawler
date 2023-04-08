using System.Collections.Generic;

public static class TilesMaster
{
    public static int collapsedTiles = 0;

    public static TileInfo[] allTiles =
    {
        // 0: NEGATIVE SPACE!
        new TileInfo(0, 10,
            new short[] {-1,2,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1},
            new List<short>{0,8,12,13},     // N = 0
            new List<short>{0,2,5,7,10,12}, // E = 1
            new List<short>{0,2,3,4,10,11}, // S = 2
            new List<short>{0,4,6,9,11,13}),// W = 3
        // 1: JUST FLOOR!
        new TileInfo(1,30,
            new short[] {-1,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1},
            new List<short>{1,3,16,17,18,19,20},
            new List<short>{1,6,14,16,18,19,20},
            new List<short>{1,8,14,15,18,19,20},
            new List<short>{1,5,15,17,18,19,20}),
        // 2: NW CORNER
        new TileInfo(2,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new List<short>{0,7,8,9,12,13},
            new List<short>{3,4,11,17},
            new List<short>{5,7,12,17},
            new List<short>{0,4,6,9,11,13}),
        // 3: NORTH WALL
        new TileInfo(3,20,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new List<short>{0,7,8,9,12,13},
            new List<short>{3,4,11},
            new List<short>{1,8,14,15,18,19,20},
            new List<short>{2,3,10}),
        // 4: NE CORNER
        new TileInfo(4,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new List<short>{0,7,8,9,12,13},
            new List<short>{0,2,5,7,10,12},
            new List<short>{6,9,13,16},
            new List<short>{2,3,10,16}),
        // 5: WEST WALL
        new TileInfo(5,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new List<short>{2,5,10,15},
            new List<short>{1,6,14,16,18,19,20},
            new List<short>{5,7,12,17},
            new List<short>{0,4,6,9,11,13}),
        // 6: EAST WALL
        new TileInfo(6,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new List<short>{4,6,11,14},
            new List<short>{0,2,5,7,10,12},
            new List<short>{6,9,13,16},
            new List<short>{1,5,15,17,18,19,20}),
        // 7: SW CORNER
        new TileInfo(7,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new List<short>{2,5,10,15},
            new List<short>{8,9,13,15},
            new List<short>{0,2,3,4,10,11},
            new List<short>{0,4,6,9,11,13}),
        // 8: SOUTH WALL
        new TileInfo(8,20,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new List<short>{1,3,16,17,18,19,20},
            new List<short>{8,9,13,15},
            new List<short>{0,2,3,4,10,11},
            new List<short>{7,8,12,14}),
        // 9: SE CORNER
        new TileInfo(9,25,
            new short[] {0,2,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,1,1,1,1,2,2,2},
            new List<short>{4,6,11,14},
            new List<short>{0,2,5,7,10,12},
            new List<short>{0,2,3,4,10,11},
            new List<short>{7,8,12,14}),
        // 10: SOUTHEAST SLANT
        new TileInfo(10,15,
            new short[] {1,1,1,1,1,0,0,0,0,0,-1,0,-1,-1,0,1,0,2,1,1,1},
            new List<short>{0,7,8,9,12,13},
            new List<short>{3,4,11,17},
            new List<short>{5,7,12,17},
            new List<short>{0,4,6,9,11,13}),
        // 11: SOUTHWEST SLANT
        new TileInfo(11,15,
            new short[] {1,1,1,1,1,0,0,0,0,0,0,-1,-1,-1,0,0,2,1,1,1,1},
            new List<short>{0,7,8,9,12,13},
            new List<short>{0,2,5,7,10,12},
            new List<short>{6,9,13,16},
            new List<short>{2,3,10,16}),
        // 12: NORTHEAST SLANT
        new TileInfo(12,15,
            new short[] {1,1,1,1,1,0,0,0,0,0,1,0,-1,1,0,1,0,2,1,1,1},
            new List<short>{2,5,10,15},
            new List<short>{8,9,13,15},
            new List<short>{0,2,3,4,10,11},
            new List<short>{0,4,6,9,11,13}),
        // 13: NORTHWEST SLANT
        new TileInfo(13,15,
            new short[] {1,1,1,1,1,0,0,0,0,0,0,-1,-1,-1,0,0,2,1,1,1,1},
            new List<short>{4,6,11,14},
            new List<short>{0,2,5,7,10,12},
            new List<short>{0,2,3,4,10,11},
            new List<short>{7,8,12,14}),
        // 14: SOUTHEAST BRICKS
        new TileInfo(14,10,
            new short[] {1,1,0,0,0,-1,1,1,1,1,2,0,0,2,-1,0,0,0,1,1,1},
            new List<short>{1,3,16,17,18,19,20},
            new List<short>{8,9,13,15},
            new List<short>{6,9,13,16},
            new List<short>{1,5,15,17,18,19,20}),
        // 15: SOUTHWEST BRICKS
        new TileInfo(15,10,
            new short[] {},
            new List<short>{1,3,16,17,18,19,20},
            new List<short>{1,6,16,14,18,19,20},
            new List<short>{5,7,12,17},
            new List<short>{7,8,12,14}),
        // 16: NORTHEAST BRICKS
        new TileInfo(16,10,
            new short[] {},
            new List<short>{4,6,11,14},
            new List<short>{3,4,11,17},
            new List<short>{1,8,14,15,18,19,20},
            new List<short>{1,5,15,17,18,19,20}),
        // 17: NORTHWEST BRICKS
        new TileInfo(17,10,
            new short[] {},
            new List<short>{2,5,10,15},
            new List<short>{1,6,14,16,18,19,20},
            new List<short>{1,8,14,15,18,19,20},
            new List<short>{2,3,10,16}),
        // 18: BRICK FLOOR
        new TileInfo(18,5,
            new short[] {},
            new List<short>{1,3,16,17,19,20},
            new List<short>{1,6,14,16,19,20},
            new List<short>{1,8,14,15,19,20},
            new List<short>{1,5,15,17,19,20}),
        // 19: HUMAN BONES!
        new TileInfo(19,7,
            new short[] {},
            new List<short>{1,3,16,17,18,20},
            new List<short>{1,6,14,16,18,20},
            new List<short>{1,8,14,15,18,20},
            new List<short>{1,5,15,17,18,20}),
        // 20: MONSTER BONES!
        new TileInfo(20,8,
            new short[] {},
            new List<short>{1,3,16,17,18,19},
            new List<short>{1,6,14,16,18,19},
            new List<short>{1,8,14,15,18,19},
            new List<short>{1,5,15,17,18,19})
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
    public List<short>[] rules = new List<short>[4];
    public short[] changes = new short[21];
    public short percentage;

    public TileInfo(short index, short percent, short[] changelist, List<short> north, List<short> east, List<short> south, List<short> west)
    {
        this.tileIndex = index;
        this.percentage = percent;
        this.changes = changelist;

        if (north != null) { rules[0] = north; }
        if (east  != null) { rules[1] = east ; }
        if (south != null) { rules[2] = south; }
        if (west  != null) { rules[3] = west ; }
    }
}


