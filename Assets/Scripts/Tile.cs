using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

            List<short> oldSuperpositions = new List<short>();
            oldSuperpositions.Concat(superpositions);
            short choice = -1;

            if (superpositions.Count > 0)
            {
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
                UpdateNeighborsSuperpositions(true, null, -1);
            }
            else
            {
                Debug.LogWarningFormat("{0} has no choices!", this.name);
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
            List<short> oldSuperpositions = new List<short>();
            oldSuperpositions.Concat(this.superpositions);

            for(int i = superpositions.Count - 1; i >= 0; i--)
            {
                if (!newSuperpositions.Contains(superpositions[i]))
                {
                    superpositions.RemoveAt(i);
                }
            }

            if (superpositions.Count != oldSuperpositions.Count)
            {
                // There has been a change, so update our neighboring superpositions and call this method on our neighbors EXCEPT nIofC
                if (superpositions.Count == 0)
                {
                    collapsed = false;
                    superpositions.Concat(oldSuperpositions);
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
                    UpdateNeighborsSuperpositions(false, neighborCandidates[j], s);
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
                    for (int c = neighborCandidates[n].Count; c > 0; c--)
                    {
                        bool possible = false;

                        for (int s = 0; s < superpositions.Count && !possible; s++)
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
        }

        FindEntropy();
    }
}