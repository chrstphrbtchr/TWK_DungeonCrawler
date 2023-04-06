using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TilesMaster
{
    public static TileInfo[] allTiles =
    {
        // 0: NEGATIVE SPACE!
        new TileInfo(0, 0,
            new List<short>{0,8,12,13},
            new List<short>{0,2,5,7,10,12},
            new List<short>{0,2,3,4,10,11},
            new List<short>{0,4,6,9,11,13}),
        // 1: JUST FLOOR!
        new TileInfo(0, 1,
            new List<short>{1,3,16,17,18,19,20},
            new List<short>{1,6,14,16,18,19,20},
            new List<short>{1,8,14,15,18,19,20},
            new List<short>{1,5,15,17,18,19,20}),
        // 2: NW CORNER
        new TileInfo(18, 2,
            new List<short>{0,7,8,9,12,13},
            new List<short>{3,4,11,17},
            new List<short>{5,7,12,17},
            new List<short>{0,4,6,9,11,13}),
        // 3: NORTH WALL
        new TileInfo(19, 3,
            new List<short>{0,7,8,9,12,13},
            new List<short>{3,4,11},
            new List<short>{1,8,14,15,18,19,20},
            new List<short>{2,3,10}),
        // 4: NE CORNER
        new TileInfo(20, 4,
            new List<short>{0,7,8,9,12,13},
            new List<short>{0,2,5,7,10,12},
            new List<short>{6,9,13,16},
            new List<short>{2,3,10,16}),
        // 5: WEST WALL
        new TileInfo(67, 5,
            new List<short>{2,5,10,15},
            new List<short>{1,14,16,18,19,20},
            new List<short>{5,7,12,17},
            new List<short>{0,4,6,9,11,13}),
        // 6: EAST WALL
        new TileInfo(69, 6,
            new List<short>{4,6,11,14},
            new List<short>{0,2,5,7,10,12},
            new List<short>{6,9,13,16},
            new List<short>{1,15,17,18,19,20}),
        // 7: SW CORNER
        new TileInfo(116, 7,
            new List<short>{2,5,10,15},
            new List<short>{8,9,13,15},
            new List<short>{0,2,3,4,10,11},
            new List<short>{0,4,6,9,11,13}),
        // 8: SOUTH WALL
        new TileInfo(117, 8,
            new List<short>{1,3,16,17,18,19,20},
            new List<short>{8,9,13,15},
            new List<short>{0,2,3,4,10,11},
            new List<short>{7,8,12,14}),
        // 9: SE CORNER
        new TileInfo(118, 9,
            new List<short>{4,6,11,14},
            new List<short>{0,2,5,7,10,12},
            new List<short>{2,3,4,10,11},
            new List<short>{7,8,12,14}),
        // 10: SOUTHEAST SLANT
        new TileInfo(167, 10,
            new List<short>{0,7,8,9,12,13},
            new List<short>{3,4,11,17},
            new List<short>{5,7,12,17},
            new List<short>{0,4,6,9,11,13}),
        // 11: SOUTHWEST SLANT
        new TileInfo(168, 11,
            new List<short>{0,7,8,9,12,13},
            new List<short>{0,2,5,7,10,12},
            new List<short>{6,9,13,16},
            new List<short>{2,3,10,16}),
        // 12: NORTHEAST SLANT
        new TileInfo(216, 12,
            new List<short>{2,5,10,15},
            new List<short>{8,9,13,15},
            new List<short>{0,2,3,4,10,11},
            new List<short>{0,4,6,9,11,13}),
        // 13: NORTHWEST SLANT
        new TileInfo(217, 13,
            new List<short>{4,6,11,14},
            new List<short>{0,2,5,7,10,12},
            new List<short>{0,2,3,4,10,11},
            new List<short>{7,8,12,14}),
        // 14: SOUTHEAST BRICKS
        new TileInfo(165, 14,
            new List<short>{1,3,16,17,18,19,20},
            new List<short>{8,9,13,15},
            new List<short>{6,9,13,16},
            new List<short>{1,5,17,18,19,20}),
        // 15: SOUTHWEST BRICKS
        new TileInfo(166, 15,
            new List<short>{1,3,16,17,18,19,20},
            new List<short>{1,6,16,18,19,20},
            new List<short>{5,7,12,17},
            new List<short>{7,8,12,14}),
        // 16: NORTHEAST BRICKS
        new TileInfo(214, 16,
            new List<short>{4,6,11,14},
            new List<short>{3,4,11,17},
            new List<short>{1,8,14,15,18,19,20},
            new List<short>{1,4,15,18,19,20}),
        // 17: NORTHWEST BRICKS
        new TileInfo(215, 17,
            new List<short>{2,5,10,15},
            new List<short>{1,6,16,18,19,20},
            new List<short>{1,8,14,15,18,19,20},
            new List<short>{2,3,10,16}),
        // 18: BRICK FLOOR
        new TileInfo(68, 18,
            new List<short>{1,3,16,17,19,20},
            new List<short>{1,6,14,16,19,20},
            new List<short>{1,8,14,15,19,20},
            new List<short>{1,5,15,17,19,20}),
        // 19: HUMAN BONES!
        new TileInfo(735, 19,
            new List<short>{1,3,16,17,18,20},
            new List<short>{1,6,14,16,18,20},
            new List<short>{1,8,14,15,18,20},
            new List<short>{1,5,15,17,18,20}),
        // 20: MONSTER BONES!
        new TileInfo(736, 20,
            new List<short>{1,3,16,17,18,19},
            new List<short>{1,6,14,16,18,19},
            new List<short>{1,8,14,15,18,19},
            new List<short>{1,5,15,17,18,19})
    };

    /// <summary>
    /// Given two Tiles, returns 0 if the entropy of both 
    /// Tiles is equal, -1 if the new Tile is less, and 1 
    /// if the currently selected Tile still has the 
    /// least entropy.
    /// </summary>
    /// <param name="current">Currently the Tile with the lowest entropy.</param>
    /// <param name="other">A new challenger!</param>
    /// <returns>-1, 0, or 1 depending on whether the new Tile 
    /// has less, equal, or greater entropy than the current.</returns>
    public static int CompareEntropy(Tile current, Tile other)
    {
        if (current == null)
        {
            return -1;
        }

        if(other.entropy <= current.entropy)
        {
            return (current.entropy == other.entropy) ? 0 : -1;
        }
        else
        {
            return 1;
        }
    }

}

public class TileInfo
{
    public short tileNumber;
    public short tileIndex;
    public List<short>[] rules = new List<short>[4];
    // N : 0
    // E : 1
    // S : 2
    // W : 3

    public TileInfo(short num, short index, List<short> north, List<short> east, List<short> south, List<short> west)
    {
        this.tileNumber = num;
        if (north != null) { rules[0] = north; }
        if (east  != null) { rules[1] = east ; }
        if (south != null) { rules[2] = south; }
        if (west  != null) { rules[3] = west ; }
    }
}
