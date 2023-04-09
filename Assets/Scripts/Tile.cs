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
    public void FindEntropy()
    {
        entropy = superpositions.Count;
        /*
        if(entropy <= 1)
        {
            CollapseTile();
        }
        */
    }

    /*
    void RecalculateSuperpositions(List<short> newOnes)
    {
        bool changed = false;

        for(int i = superpositions.Count; i > 0 ; i--)
        {
            
            if (!newOnes.Contains(superpositions[i - 1]))
            {
                superpositions.RemoveAt(i - 1);
                changed = true;
            }
        }

        if (changed)
        {
            CalculatePossibleNeighbors();

            for (int q = 0; q < neighbors.Length; q++)
            {
                if (neighbors[q] != null)
                {
                    neighbors[q].UpdateNeighborsSuperpositions((q + 2) % 4);
                }
            }
        }
    }
    */

    public void CollapseTile()
    {
        if(!collapsed)
        {
            collapsed = true;
            TilesMaster.collapsedTiles++;

            List<short> oldSuperpositions = new List<short>();
            oldSuperpositions.Concat(superpositions);
            short choice = -1;

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
                    possibleNeighboringSuperpositions[i] = TilesMaster.allTiles[choice].sockets[i];
                }
            }

            if(choice >= 0)
            {
                AssignSprite(choice);
                UpdateNeighborsSuperpositions(true, null, -1);
                TilesMaster.EvaluatePercentages(tileNum);
            }
            else
            {
                Debug.LogWarningFormat("{0} has no choices!", this.name);
                UpdateNeighborsSuperpositions(false, oldSuperpositions, -1);
                collapsed = false;
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
                CalculatePossibleNeighbors(neighborIndexOfCaller);
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
                    for (int c = possibleNeighboringSuperpositions[n].Count; c > 0; c--)
                    {
                        bool possible = false;

                        for (int s = 0; s < superpositions.Count && !possible; s++)
                        {
                            short index = superpositions[s];
                            short possibility = possibleNeighboringSuperpositions[n][c - 1];

                            if (TilesMaster.allTiles[index].sockets[n].Contains(possibility))
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

    void DoubleCheckSuperpositions()
    {

    }
}