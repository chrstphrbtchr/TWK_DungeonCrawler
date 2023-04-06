using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileInfo thisTile;

    public int entropy;
    public bool collapsed = false, recentlyChanged = false;

    public List<short> superpositions = new List<short>() { 
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
        11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

    public Tile[] neighbors = new Tile[4];

    SpriteRenderer sprite;

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
            AssignSprite(TilesMaster.allTiles[superpositions[0]].tileNumber);
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

            int rnd = Random.Range(0, superpositions.Count);
            short choice = superpositions[rnd];

            thisTile = TilesMaster.allTiles[choice];
            superpositions.Clear();
            superpositions.Add(choice);

            AssignSprite(thisTile.tileNumber);
            UpdateNeighbors();
        }
    }

    void AssignSprite(string s)
    {
        this.name = s;
        sprite.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f,1f));
        //sprite.sprite = Resources.Load<Sprite>(
         //       "Tiles/colored-transparent_packed_" + s);
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