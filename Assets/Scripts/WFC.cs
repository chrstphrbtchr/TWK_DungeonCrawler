using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFC : MonoBehaviour
{
    const float spawnOffset = 0.5f;
    const int xLen = 32, yLen = 20;

    int times = xLen * yLen;
    
    public GameObject tile;
    public Tile[,] tileArray = new Tile[xLen, yLen];

    void Start()
    {
        BuildLevel();
        WaveFunctionCollapse();
    }

    void BuildLevel()
    {
        for (int i = 0; i < yLen; i++)
        {
            for (int j = 0; j < xLen; j++)
            {
                Tile t = Instantiate(tile, new Vector2(j * spawnOffset, i * spawnOffset),
                    Quaternion.identity).GetComponent<Tile>();
                t.name = "Tile " + j + "-" + i;
                tileArray[j, i] = t;

                if (i == 0)
                {
                    if (j == 0 || j == xLen - 1)
                    {
                        t.superpositions = (j == 0 ? new List<short>() { 0, 2, 10 } :
                            new List<short>() { 0, 4, 11 });
                    }
                    else
                    {
                        t.superpositions = new List<short> { 0, 2, 3, 4, 10, 11 };
                    }
                }
                else if (i == yLen - 1)
                {
                    if (j == 0 || j == xLen - 1)
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
                    if (j == 0 || j == xLen - 1)
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
        if(x - 1 >= 0)
        {
            t.neighbors[3] = tileArray[x - 1, y];
            tileArray[x - 1, y].neighbors[1] = t;
        }

        if(y - 1 >= 0)
        {
            t.neighbors[0] = tileArray[x, y - 1];
            tileArray[x, y - 1].neighbors[2] = t;

        }
    }

    Tile ChooseNextTile(bool firstTime)
    {
        List<Tile> candidates = new List<Tile>();
        Tile next = null;

        if (firstTime)
        {
            candidates.Add(tileArray[Random.Range(0, xLen), Random.Range(0,yLen)]);
        }
        else
        {
            for(int i = 0; i < yLen; i++)
            {
                for(int j = 0; j < xLen; j++)
                {
                    if (!tileArray[j,i].collapsed)
                    {
                        Debug.Log("NOT TRUE");
                        if (candidates.Count == 0)
                        {
                            candidates.Add(tileArray[j, i]);
                            Debug.Log("<color=purple>CANDIDATE</color>: " + j + " " + i);
                        }
                        else
                        {
                            Debug.Log("<color=orange>PRE_CANDIDATES.COUNT: </color>" + candidates.Count);
                            if (candidates[0].GetEntropy() >= tileArray[j, i].GetEntropy())
                            {
                                if (candidates[0].GetEntropy() > tileArray[j, i].GetEntropy())
                                {
                                    candidates.Clear();
                                }
                                candidates.Add(tileArray[j, i]);
                                Debug.Log("<color=green>MID_CANDIDATES.COUNT: </color>" + candidates.Count);
                            }
                        }
                    }
                }
            }
        }

        if(candidates.Count > 0)
        {
            Debug.Log("<color=purple>CANDIDATES.COUNT: </color>" + candidates.Count);
            next = candidates[Random.Range(0, candidates.Count)];
        }
        
        return next;
    }

    void WaveFunctionCollapse()
    {
        for(int temp = 0; temp < times; temp++)
        {
            Tile t = ChooseNextTile(temp > 0 ? false : true);

            if (t == null) return;

            t.CollapseTile();
            Debug.Log("<color=yellow>TEMP: " + temp + "!</color>");
        }
    }
}
