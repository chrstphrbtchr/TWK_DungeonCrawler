using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public short tileNum = 0;
    public int entropy;
    public bool collapsed = false;

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
                Debug.LogWarningFormat("{0} has no choices! {1}", this.name, oldSuperpositions.Count);
                collapsed = false;

                if (oldSuperpositions.Count == 0)
                {
                    UpdateNeighborsSuperpositions(false, new List<short> {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20}, -1);
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
        this.name += " - " + s;
        WFC.ChangeTile(this, s);        // I HATE THIS. BUT, FOR A GAME JAM, I'LL LIVE...
        print("ASSIGNED " + s + " TILE!");
    }

    void UpdateNeighborsSuperpositions(bool onCollapse, List<short> newSuperpositions, short neighborIndexOfCaller)
    {
        if (!onCollapse)
        {
            Debug.LogFormat("SUPERPOSITIONS: {0} / {1}!", superpositions.Count, newSuperpositions.Count);
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
                }
            }
            else
            {
                CalculatePossibleNeighbors(-1);
                Debug.LogFormat("ONCOLLAPSE:{0} has these possible neighbors: {1},{2},{3},{4}.",this.name, possibleNeighboringSuperpositions[0].Count,
                    possibleNeighboringSuperpositions[1].Count, possibleNeighboringSuperpositions[2].Count, 
                    possibleNeighboringSuperpositions[3].Count);
                return;
            }
        }

        CalculatePossibleNeighbors(neighborIndexOfCaller);
        Debug.LogFormat("REG:{0} has these possible neighbors: {1},{2},{3},{4}.",this.name, possibleNeighboringSuperpositions[0].Count,
                    possibleNeighboringSuperpositions[1].Count, possibleNeighboringSuperpositions[2].Count, 
                    possibleNeighboringSuperpositions[3].Count);

        for (int j = 0; j < 4; j++)
        {
            if (j != neighborIndexOfCaller)
            {
                if (neighbors[j] != null)
                {
                    short s = (short)((j + 2) % 4);
                    UpdateNeighborsSuperpositions(false, possibleNeighboringSuperpositions[j], s);
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
                                Debug.LogFormat("REMOVED! {0} INDEX {1}: {2}. IgnoreIndex = {3}.", this.name, (c - 1), possibleNeighboringSuperpositions[n][c - 1], ignoreIndex);
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
            return (rnd == 0 ? current : challenger);
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
                        return false;
                    }
                }
            }
        }
        return true;
    }
}