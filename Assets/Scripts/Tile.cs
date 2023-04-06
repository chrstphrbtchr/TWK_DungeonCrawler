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
    public List<short>[] candidates =
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
        if(entropy == 1)
        {
            CollapseTile();
        }
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
            FindEntropy();
            for (int q = 0; q < neighbors.Length; q++)
            {
                if (neighbors[q] != null)
                {
                    neighbors[q].UpdateNeighbors((q + 2) % 4);
                }
            }
        }
    }

    public void CollapseTile()
    {
        if(!collapsed)
        {
            Debug.Log(this.name + " is Collapsing!");
            Debug.Log("SUPERPOS: " + superpositions.Count);
            collapsed = true;
            short choice = -1;

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
                        }
                    }
                }
            }

            if (superpositions.Count > 0)
            {
                // CHECK AGAINST NEIGHBORS' POSSIBLE NEIGHBORS.
                Debug.Log("YAY! SUPERPOSITIONS > 0! :: " + superpositions.Count);
                int rnd = Random.Range(0, superpositions.Count);
                choice = superpositions[rnd];
                tileNum = choice;
                superpositions.Clear();
                superpositions.Add(choice);
                for(int i = 0; i < candidates.Length; i++)
                {
                    candidates[i] = TilesMaster.allTiles[choice].rules[i];
                }
            }
            else
            {
                Debug.LogErrorFormat("SUPERPOSITIONS FOR {0} ARE ZERO!", this.name);
            }

            if(choice > 0)
            {
                AssignSprite(choice);
            }
            UpdateNeighbors(-1);
        }
    }

    void AssignSprite(short s)
    {
        this.name += " - " + s;
        WFC.ChangeTile(this, s);        // I HATE THIS. BUT, FOR A GAME JAM, I'LL LIVE...
        print("ASSIGNED " + s + " TILE!");
    }

    void UpdateNeighbors(int caller)
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
                            if (!candidates[(x + 2) % 4].Contains(neighbors[x].superpositions[y - 1]))
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
                    neighbors[y].RecalculateSuperpositions(candidates[y]);
                }
            }
        }
    }
}