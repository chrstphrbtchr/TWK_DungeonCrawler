using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public short tileNum = 0;
    public int entropy;
    public bool collapsed = false;
    public Vector2Int tileLoc;

    public List<short> superpositions = new List<short>() { 
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
    public List<short>[] possibleNeighboringSuperpositions =
    {
        new List<short>(){ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20},
        new List<short>(){ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20},
        new List<short>(){ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20},
        new List<short>(){ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20}
    };

    public Tile[] neighbors = new Tile[4];

    public SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public int GetEntropy() => entropy;
    public void FindEntropy() => entropy = superpositions.Count;

    public void CollapseTile()
    {
        if(!collapsed)
        {
            collapsed = true;
            TilesMaster.collapsedTiles++;
            List<short> oldSuperpositions = new List<short>();
            oldSuperpositions.Concat(superpositions);

            short choice = -1;

            for (int a = superpositions.Count - 1; a >= 0; a--)
            {
                if (!DoubleCheckSuperposition(superpositions[a]))
                {
                    superpositions.RemoveAt(a);
                }
            }

            if (superpositions.Count > 0)
            {
                /*
                int rnd = Random.Range(0, superpositions.Count);
                choice = superpositions[rnd];
                */
                TileInfo tInfo = TilesMaster.allTiles[superpositions[0]];
                for(int i = 1; i < superpositions.Count; i++)
                {
                    tInfo = GetHighestPercentageTileInfo(tInfo, TilesMaster.allTiles[superpositions[i]]);
                }
                tileNum = tInfo.tileIndex;
                choice = (short)tileNum;
                /*
                superpositions.Clear();
                superpositions.Add(choice);

                for(int i = 0; i < possibleNeighboringSuperpositions.Length; i++)
                {
                    possibleNeighboringSuperpositions[i].Clear();
                    TileInfo thisTilesInfo = TilesMaster.allTiles[tileNum];

                    for(int g = 0; g < TilesMaster.allTiles.Length; g++)
                    {
                        TileInfo thatTilesInfo = TilesMaster.allTiles[g];
                        if(WFC.IsNeighborValid(thisTilesInfo.sockets, thatTilesInfo.sockets, i))
                        {
                            possibleNeighboringSuperpositions[i].Add((short)g);
                            Debug.LogFormat("Added {0} to {1}'s NL#{2}", g, this.name, i);
                        }
                    }
                }
                */
            }

            if(choice >= 0)
            {
                AssignSprite(choice);
                List<short> failsafe = new List<short> { choice };
                UpdateNeighborsSuperpositions(true, failsafe, -1);
                TilesMaster.EvaluatePercentages(tileNum);
            }
            else
            {
                if (collapsed)
                {
                    collapsed = false;
                    TilesMaster.collapsedTiles--;
                }
                
                if (oldSuperpositions.Count == 0)
                {
                    AssignStartingSuperpositions(tileLoc.x, tileLoc.y);
                    UpdateNeighborsSuperpositions(false, superpositions, -1);
                }
                else
                {
                    UpdateNeighborsSuperpositions(false, oldSuperpositions, -1);
                }
            }
        }
    }

    void AssignSprite(short s)
    {
        this.name = "Tile " + tileLoc.ToString() + " - " + s.ToString();
        WFC.ChangeTile(this, s);        // I HATE THIS. BUT, FOR A GAME JAM, I'LL LIVE...
    }

    void UpdateNeighborsSuperpositions(bool onCollapse, List<short> newSuperpositions, short neighborIndexOfCaller)
    {
        if (!onCollapse)
        {
            int oldSuperpositionsCount = superpositions.Count;

            for(int i = superpositions.Count - 1; i >= 0; i--)
            {
                if (!newSuperpositions.Contains(superpositions[i]))
                {
                    superpositions.RemoveAt(i);
                }
            }

            if (superpositions.Count != oldSuperpositionsCount)
            {
                // There has been a change, so update our neighboring superpositions and call this method on our neighbors EXCEPT nIofC
                if (superpositions.Count == 0)
                {
                    Debug.LogWarning("No superpositions for " + this.name);
                    if (collapsed)
                    {
                        collapsed = false;
                        TilesMaster.collapsedTiles--;
                        TilesMaster.EvaluatePercentages(tileNum, true);
                    }

                    superpositions.Concat(newSuperpositions);
                    AssignStartingSuperpositions(tileLoc.x, tileLoc.y);
                    UpdateNeighborsSuperpositions(false, superpositions, -1);
                }
            }
            else
            {
                CalculatePossibleNeighbors(-1);
                return;
            }
        }

        CalculatePossibleNeighbors(neighborIndexOfCaller);

        for (int j = 0; j < 4; j++)
        {
            if (j != neighborIndexOfCaller)
            {
                if (neighbors[j] != null)
                {
                    short s = (short)((j + 2) % 4);
                    neighbors[j].UpdateNeighborsSuperpositions(false, possibleNeighboringSuperpositions[j], s);
                }
            }
        }
    }

    public void CalculatePossibleNeighbors(short ignoreIndex=-1)
    {
        for(int n = 0; n < 4; n++)
        {
            if (neighbors[n] != null)
            {
                if(n != ignoreIndex)
                {
                    if (!neighbors[n].collapsed)
                    {
                        for (int c = possibleNeighboringSuperpositions[n].Count; c > 0; c--)
                        {
                            bool possible = false;

                            for (int s = 0; s < superpositions.Count && !possible; s++)
                            {
                                TileInfo current = TilesMaster.allTiles[superpositions[s]];
                                TileInfo neighbor = TilesMaster.allTiles[possibleNeighboringSuperpositions[n][c - 1]];

                                if (WFC.IsNeighborValid(current.sockets, neighbor.sockets, n))
                                {
                                    possible = true;
                                }
                            }

                            if (!possible)
                            {
                                possibleNeighboringSuperpositions[n].RemoveAt(c - 1);
                            }
                        }
                    }
                    
                }
            }
        }

        FindEntropy();
    }

    TileInfo GetHighestPercentageTileInfo(TileInfo current, TileInfo challenger)
    {
        if (current.percentage < challenger.percentage)
        {
            return challenger;
        }
        else if (current.percentage > challenger.percentage)
        {
            return current;
        }
        else
        {
            int rnd = Random.Range(0, 2);
            return (rnd == 0 ? challenger : current);
        }
    }

    bool DoubleCheckSuperposition(short index)
    {
        TileInfo t = TilesMaster.allTiles[index];
        for (int i = 0; i < neighbors.Length; i++)
        {
            if (neighbors[i] != null)
            {
                if (neighbors[i].collapsed)
                {
                    TileInfo n = TilesMaster.allTiles[neighbors[i].tileNum];

                    if (!WFC.IsNeighborValid(t.sockets, n.sockets, i))
                    {
                        this.collapsed = false;
                        neighbors[i].collapsed = false;
                        TilesMaster.collapsedTiles -= 2;
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void AssignStartingSuperpositions(int x, int y)
    {
        tileLoc = new Vector2Int(x, y);

        if (y == WFC.yLen - 1)
        {
            if (x == 0 || x == WFC.xLen - 1)
            {
                superpositions = (x == 0 ? new List<short>() { 0, 2, 10 } :
                    new List<short>() { 0, 4, 11 });
            }
            else
            {
                superpositions = new List<short> { 0, 2, 3, 4, 10, 11 };
            }
        }
        else if (y == 0)
        {
            if (x == 0 || x == WFC.xLen - 1)
            {
                superpositions = (x == 0 ? new List<short>() { 0, 7, 12 } :
                    new List<short>() { 0, 9, 13 });
            }
            else
            {
                superpositions = new List<short> { 0, 7, 8, 9, 12, 13 };
            }
        }
        else
        {
            if (x == 0 || x == WFC.xLen - 1)
            {
                superpositions = (x == 0 ?
                    new List<short>() { 0, 2, 5, 7, 10, 12 } :
                    new List<short>() { 0, 4, 6, 9, 11, 13 });
            }
        }
    }
}