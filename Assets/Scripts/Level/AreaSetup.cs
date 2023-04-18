using System.Collections.Generic;
using UnityEngine;

public class AreaSetup
{
    public List<Tile> FindWalkableTiles()
    {
        int badTiles = 0;
        List<Tile> result = new List<Tile>();

        for(int x = 0; x < WFC.xLen; x++)
        {
            for(int y = 0; y < WFC.yLen; y++)
            {
                Tile t = WFC.tileArray[x, y];
                if (t.collapsed && t.tileNum != 0)
                {
                    result.Add(t);
                }

                if (!t.collapsed)
                {
                    badTiles++;
                }
            }
        }

        // Just in case too many un-collapsed tiles exist.
        if( badTiles > (WFC.xLen >= WFC.yLen ? WFC.yLen : WFC.xLen) / 4)
        {
            Debug.LogWarningFormat("Reset due to <color=#261B23>{0}</color> uncollapsed tiles.", badTiles);
            WFC.ResetEverything();
        }

        return result;
    }

    public List<Tile> FindPlayableArea(List<Tile> walkable)
    {
        List<Tile> finalResult = new List<Tile> ();
        

        for(int i = 0; i < walkable.Count; i++)
        {
            List<Tile> potentialResult = new List<Tile>();
            if (!finalResult.Contains(walkable[i]) && !potentialResult.Contains(walkable[i]))
            {
                potentialResult = CheckPathConnectivity(potentialResult, walkable[i]);

                if (potentialResult.Count > finalResult.Count)
                {
                    finalResult = potentialResult;
                }
            }
        }

        //------------------------------------------------------------------------------
        // LOCAL METHOD ///
        List<Tile> CheckPathConnectivity(List<Tile> tiles, Tile t)
        {
            tiles.Add(t);
            for(int i = 0; i < t.neighbors.Length; i++)
            {
                if (t.neighbors[i] != null)
                {
                    if (walkable.Contains(t.neighbors[i]) &&
                        !tiles.Contains(t.neighbors[i]))
                    {
                        // if one of the sockets is false (meaning walkable),
                        if (TilesMaster.allTiles[t.tileNum].sockets[i] == false ||
                            TilesMaster.allTiles[t.tileNum].sockets[(i + 1) % 4] == false)
                        {
                            tiles = CheckPathConnectivity(tiles, t.neighbors[i]);
                        }
                    }
                }
            }
            return tiles;
        }
        //------------------------------------------------------------------------------

        return finalResult;
    }

    
}
