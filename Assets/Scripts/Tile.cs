using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool collapsed;
    public TileInfo thisTile;
    public int entropy;
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
    }

    void RecalculateSuperpositions(List<short> newOnes)
    {
        List<short> newList = new List<short>();
        for(int i = 0; i < newOnes.Count; i++)
        {
            for(int j = 0; j < superpositions.Count; j++)
            {
                if (newOnes[i] == superpositions[j])
                {
                    newList.Add(newOnes[i]);
                }
            }
        }
        superpositions = newList;
        FindEntropy();
    }

    public void CollapseTile()
    {
        if(!collapsed)
        {
            int choice = Random.Range(0, superpositions.Count);
            collapsed = true;
            superpositions.Clear();
            thisTile = TilesMaster.allTiles[superpositions[choice]];
            AssignSprite(thisTile.tileNumber);
        }
    }

    void AssignSprite(string s)
    {
        sprite.sprite = Resources.Load<Sprite>(
                "Tiles/BaseTiles/colored-transparent_packed_" + s + ".asset");
    }
}
