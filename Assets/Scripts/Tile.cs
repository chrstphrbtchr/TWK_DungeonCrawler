using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.tvOS;

public class Tile : MonoBehaviour
{
    public int tileNum = 0;
    public int entropy;
    public bool collapsed = false;

    public List<short> superpositions = new List<short>() { 
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
    public List<short>[] neighborCandidates =
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

    public void CollapseTile()
    {
        if(!collapsed)
        {
            List<short> oldSuperpositions = new List<short>();
            oldSuperpositions.Concat(superpositions);
            collapsed = true;
            short choice = -1;

            /*
            for (int z = 0; z < neighbors.Length; z++)
            {
                if (neighbors[z] != null)
                {
                    for (int a = superpositions.Count - 1; a >= 0; a--)
                    {
                        bool possible = false;

                        for (int f = 0; f < neighbors[z].superpositions.Count && !possible; f++)
                        {
                            if (TilesMaster.allTiles[
                                neighbors[z].superpositions[f]].rules[
                                (z + 2) % 4].Contains(superpositions[a]))
                            {
                                possible = true;
                            }
                        }

                        if (!possible)
                        {
                            superpositions.RemoveAt(a);

                            if(superpositions.Count <= 0)
                            {

                            }
                        }
                    }
                }
            }
            */

            if (superpositions.Count > 0)
            {
                // CHECK AGAINST NEIGHBORS' POSSIBLE NEIGHBORS.
                int rnd = Random.Range(0, superpositions.Count);
                choice = superpositions[rnd];
                tileNum = choice;
                superpositions.Clear();
                superpositions.Add(choice);
                for(int i = 0; i < neighborCandidates.Length; i++)
                {
                    neighborCandidates[i] = TilesMaster.allTiles[choice].rules[i];
                }
            }

            if(choice >= 0)
            {
                AssignSprite(choice);
                CalculatePossibleNeighbors();

            }
            else
            {
                Debug.LogWarningFormat("{0} has no choices!", this.name);
            }
            UpdateNeighborsSuperpositions(-1);
        }
    }

    void AssignSprite(short s)
    {
        this.name += " - " + s;
        WFC.ChangeTile(this, s);        // I HATE THIS. BUT, FOR A GAME JAM, I'LL LIVE...
        print("ASSIGNED " + s + " TILE!");
    }

    void UpdateNeighborsSuperpositions(int caller)
    {
        for(int a = 0; a < superpositions.Count; a++)
        {
            TileInfo info = TilesMaster.allTiles[superpositions[a]];

            for(int x = 0; x < 4; x++)
            {
                if (neighbors[x] != null)
                {
                    if (!neighbors[x].collapsed && x != caller)
                    {
                        for(int y = neighbors[x].superpositions.Count; y > 0; y--)
                        {
                            if (!neighborCandidates[(x + 2) % 4].Contains(neighbors[x].superpositions[y - 1]))
                            {
                                neighbors[x].superpositions.RemoveAt(y - 1);
                            }
                        }
                    }
                }
            }
        }

        for(int y = 0; y < 4; y++)
        {
            if (neighbors[y] != null && y != caller)
            {
                if (!neighbors[y].collapsed)
                {
                    neighbors[y].RecalculateSuperpositions(neighborCandidates[y]);
                }
            }
        }
    }

    void ReEvaluateNeighbors(int myTileIndex)
    {

    }

    public void CalculatePossibleNeighbors(short ignoreIndex=-1)
    {
        for(int n = 0; n < 4; n++)
        {
            if (neighbors[n] != null)
            {
                for (int c = neighborCandidates[n].Count; c > 0; c--)
                {
                    bool possible = false;

                    for(int s = 0; s < superpositions.Count && !possible; s++)
                    {
                        short index = superpositions[s];
                        short possibility = neighborCandidates[n][c - 1];

                        if (TilesMaster.allTiles[index].rules[n].Contains(possibility))
                        {
                            possible = true;
                        }
                    }

                    if (!possible)
                    {
                        neighborCandidates[n].RemoveAt(c - 1);
                    }
                }
            }
        }

        FindEntropy();
    }

    void RecalculatePossibilitiesPostCollapse(List<short> newPossibleSPs, short ignoreIndex=-1)
    {

    }
}