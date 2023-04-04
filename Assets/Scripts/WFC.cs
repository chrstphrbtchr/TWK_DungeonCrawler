using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFC : MonoBehaviour
{
    public GameObject tile;
    public Tile[,] tileArray = new Tile[32,20];
    const float spawnOffset = 0.5f;

    void Start()
    {
        BuildLevel();
        // Random Tile
        // Assign
        // Update Neighbors' Superpositions.
        //          and so forth, updating entropy along the way
        // Determine next lowest entropy.
        // start again!

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildLevel()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                Tile t = Instantiate(tile, new Vector2(j * spawnOffset, i * spawnOffset),
                    Quaternion.identity).GetComponent<Tile>();
                tileArray[j, i] = t;
                if (i == 0)
                {
                    if (j == 0 || j == 31)
                    {
                        t.superpositions = (j == 0 ? new List<short>() { 0, 2, 10 } :
                            new List<short>() { 0, 4, 11 });
                    }
                    else
                    {
                        t.superpositions = new List<short> { 0, 2, 3, 4, 10, 11 };
                    }
                }
                else if (i == 19)
                {
                    if (j == 0 || j == 31)
                    {
                        t.superpositions = (j == 0 ? new List<short>() { 0, 6, 12 } :
                            new List<short>() { 0, 9, 13 });
                    }
                    else
                    {
                        t.superpositions = new List<short> { 0, 7, 8, 9, 12, 13 };
                    }
                }
                else
                {
                    if (j == 0 || j == 31)
                    {
                        t.superpositions = (j == 0 ?
                            new List<short>() { 0, 2, 5, 7, 10, 12 } :
                            new List<short>() { 0, 4, 6, 9, 11, 13 });
                    }
                }

                AssignNeighbors(t,j,i);

            }
        }
    }

    void AssignNeighbors(Tile t, int x, int y)
    {
        if(x - 1 > 0)
        {
            t.neighbors[3] = tileArray[x - 1, y];
            tileArray[x - 1, y].neighbors[1] = t;
        }

        if(y - 1 > 0)
        {
            t.neighbors[0] = tileArray[x, y - 1];
            tileArray[x, y - 1].neighbors[2] = t;
        }
    }
}
