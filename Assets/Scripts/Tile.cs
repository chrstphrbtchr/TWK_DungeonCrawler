using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int entropy;
    public bool collapsed = false, recentlyChanged = false;

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
        UpdateNeighbors();
    }

    void RecalculateSuperpositions(List<short> newOnes)
    {
        List<short> newList = new List<short>();

        for(int i = 0; i < newOnes.Count; i++)
        {
            if (superpositions.Contains(newOnes[i]))
            {
                newList.Add(newOnes[i]);
            }
        }
        superpositions.Clear();
        superpositions = newList;
        FindEntropy();
    }

    public void CollapseTile()
    {
        if(!collapsed)
        {
            Debug.Log(this.name + " is Collapsing!");
            collapsed = true;
            short choice = 0;

            if(superpositions.Count > 0)
            {
                int rnd = Random.Range(0, superpositions.Count);
                choice = superpositions[rnd];
                superpositions.Clear();
                superpositions.Add(choice);
            }

            AssignSprite(choice);
            UpdateNeighbors();
        }
    }

    void AssignSprite(short s)
    {
        this.name += " - " + s;
        WFC.ChangeTile(this, s);        // I HATE THIS. BUT, FOR TESTING, I'LL LIVE...
        print("ASSIGNED " + s + " TILE!");
    }

    void UpdateNeighbors()
    {
        recentlyChanged = true;
        List<List<short>> candidates = new List<List<short>>(){ new List<short>(),new 
                List<short>(), new List<short>(), new List<short>()};

        for(int a = 0; a < superpositions.Count; a++)
        {
            TileInfo info = TilesMaster.allTiles[superpositions[a]];

            for(int x = 0; x < 4; x++)
            {
                if (neighbors[x] != null)
                {
                    if (!neighbors[x].collapsed && !neighbors[x].recentlyChanged)
                    {
                        for (int b = 0; b < info.rules[x].Count; b++)
                        {
                            // check north
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
            if (neighbors[y] != null)
            {
                if (!neighbors[y].collapsed && !neighbors[y].recentlyChanged)
                {
                    neighbors[y].RecalculateSuperpositions(candidates[y]);
                }
            }
        }
    }
}
// maybe do it all fr wfc instead of indiv tiles??
// check new list vs old list, stop search if same