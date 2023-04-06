using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tileNum = 0;
    public int entropy;
    public bool collapsed = false;

    public List<short> superpositions = new List<short>() { 
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
        11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

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
            AssignSprite(TilesMaster.allTiles[superpositions[Random.Range(0, superpositions.Count)]].tileIndex);
            CollapseTile();
        }
    }

    void RecalculateSuperpositions(List<short> newOnes)
    {
        bool changed = false;

        for(int i = newOnes.Count - 1; i >= 0 ; i--)
        {
            if (!superpositions.Contains(newOnes[i]))
            {
                superpositions.Remove(newOnes[i]);
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
            collapsed = true;
            short choice = -1;

            for(int z = 0; z < neighbors.Length; z++)
            {
                if (neighbors[z] != null)
                {
                    for (int a = superpositions.Count - 1; a >= 0; a--)
                    {
                        bool removed = false;
                        for (int f = 0; f < neighbors[z].superpositions.Count && !removed; f++)
                        {
                            Debug.LogFormat("<color=magenta>Z: {0}, A: {1}, F: {2}, #{3}</color>!", (z + 2) % 4, a, f, TilesMaster.allTiles[neighbors[z].superpositions[f]].tileNumber);
                            if (!TilesMaster.allTiles[
                                neighbors[z].superpositions[f]].rules[
                                (z + 2) % 4].Contains(superpositions[a]))
                            {
                                superpositions.Remove(superpositions[a]);
                                Debug.Log("<color=yellow> REMOVED!!! </color>");
                                removed = true;
                                if(superpositions.Count == 0)
                                {
                                    Debug.LogError("UH OH, WE GOT AN IMPOSSIBLE TILE HERE!");
                                }
                            }
                        }
                    }
                }
            }

            if(superpositions.Count > 0)
            {
                // CHECK AGAINST NEIGHBORS' POSSIBLE NEIGHBORS.
                int rnd = Random.Range(0, superpositions.Count);
                choice = superpositions[rnd];
                tileNum = choice;
                superpositions.Clear();
                superpositions.Add(choice);
            }
            else
            {

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
        List<List<short>> candidates = new List<List<short>>(){ new List<short>(),new 
                List<short>(), new List<short>(), new List<short>()};

        for(int a = 0; a < superpositions.Count; a++)
        {
            TileInfo info = TilesMaster.allTiles[superpositions[a]];

            for(int x = 0; x < 4; x++)
            {
                if (neighbors[x] != null)
                {
                    if (!neighbors[x].collapsed && x != caller)
                    {
                        for (int b = 0; b < info.rules[x].Count; b++)
                        {
                            if (!candidates[x].Contains(info.rules[x][b]))
                            {
                                candidates[x].Add(info.rules[x][b]);
                            }
                        }
                        Debug.Log("<color=cyan>" + candidates[x].Count + "</color>");
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
// maybe do it all fr wfc instead of indiv tiles??
// check new list vs old list, stop search if same