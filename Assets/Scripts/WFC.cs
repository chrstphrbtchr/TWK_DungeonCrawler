using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WFC : MonoBehaviour
{
    const float spawnOffset = 0.5f;
    const int xLen = 32, yLen = 20;

    int times = xLen * yLen;
    [SerializeField] Sprite[] allSprites;
    static Sprite[] staticAllSprites; // THIS SUCKS.
    
    public GameObject tile;
    public Tile[,] tileArray = new Tile[xLen, yLen];

    void Start()
    {
        staticAllSprites = allSprites;  //yuck
        BuildLevel();
        WaveFunctionCollapse();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
#endif
    }

    void BuildLevel()
    {
        for (int y = 0; y < yLen; y++)
        {
            for (int x = 0; x < xLen; x++)
            {
                Tile t = Instantiate(tile, new Vector2(x * spawnOffset, y * spawnOffset),
                    Quaternion.identity).GetComponent<Tile>();
                t.name = "Tile [" + x + ", " + y + "]";
                tileArray[x, y] = t;

                if (y == yLen - 1)
                {
                    if (x == 0 || x == xLen - 1)
                    {
                        t.superpositions = (x == 0 ? new List<short>() { 0, 2, 10 } :
                            new List<short>() { 0, 4, 11 });
                    }
                    else
                    {
                        t.superpositions = new List<short> { 0, 2, 3, 4, 10, 11 };
                    }
                }
                else if (y == 0)
                {
                    if (x == 0 || x == xLen - 1)
                    {
                        t.superpositions = (x == 0 ? new List<short>() { 0, 7, 12 } :
                            new List<short>() { 0, 9, 13 });
                    }
                    else
                    {
                        t.superpositions = new List<short> { 0, 7, 8, 9, 12, 13 };
                    }
                }
                else
                {
                    if (x == 0 || x == xLen - 1)
                    {
                        t.superpositions = (x == 0 ?
                            new List<short>() { 0, 2, 5, 7, 10, 12 } :
                            new List<short>() { 0, 4, 6, 9, 11, 13 });
                    }
                }

                AssignNeighbors(t,x,y);

                for(int i = 0; i<4; i++)
                {
                    if (t.neighbors[i] == null)
                    {
                        t.neighborCandidates[i].Clear();
                    }
                }

                t.CalculatePossibleNeighbors();
            }
        }
        Debug.Log("Level built.");
    }

    void AssignNeighbors(Tile t, int x, int y)
    {
        if(x - 1 >= 0)
        {
            t.neighbors[3] = tileArray[x - 1, y];
            tileArray[x - 1, y].neighbors[1] = t;

            t.neighbors[3].neighborCandidates[1] = t.superpositions;
            t.neighborCandidates[3] = t.neighbors[3].superpositions;
        }

        if(y - 1 >= 0)
        {
            t.neighbors[0] = tileArray[x, y - 1];
            tileArray[x, y - 1].neighbors[2] = t;

            t.neighbors[0].neighborCandidates[2] = t.superpositions;
            t.neighborCandidates[0] = t.neighbors[0].superpositions;
        }
    }

    Tile ChooseNextTile(bool firstTime)
    {
        Tile next = null;

        if (firstTime)
        {
            next = tileArray[Random.Range(1, xLen - 1), Random.Range(1 , yLen - 1)];
        }
        else
        {
            for(int i = 0; i < yLen; i++)
            {
                for(int j = 0; j < xLen; j++)
                {
                    if (!tileArray[j,i].collapsed)
                    {
                        if(next == null)
                        {
                            next = tileArray[j,i];
                        }
                        else
                        {
                            next = FindLowestEntropyTile(next, tileArray[j,i]);
                        }
                    }
                }
            }
        }
        
        return next;
    }

    Tile FindLowestEntropyTile(Tile current, Tile challenger)
    {
        if(current.entropy < challenger.entropy)
        {
            return current;
        }
        else if(current.entropy > challenger.entropy)
        {
            return challenger;
        }
        else
        {
            int rnd = Random.Range(0, 2);
            return (rnd == 0 ? current : challenger);
        }
    }


    void WaveFunctionCollapse()
    {
        for(int temp = 0; temp < times * 3 || TilesMaster.collapsedTiles >= xLen * yLen; temp++)
        {
            Tile t = ChooseNextTile(temp > 0 ? false : true);

            if (t == null) { Debug.Log("OOPS!"); return; }

            t.CollapseTile();
        }
        Debug.Log("YOU'RE TELLING ME A <color=cyan>WAVE</color> <color=magenta>COLLAPSED</color> THIS <color=lime>FUNCTION</color>?!");
    }

    public static void ChangeTile(Tile t, short num)
    {
        t.sprite.sprite = staticAllSprites[num];
    }
}

// On Collapse, before choosing which Tile, check all neighbors to see if they're logical
//      save the old superpositions list as a new list.
//      IF NEW SUPERPOS. LIST == 0, then insert the old list and pick something.
//          (in the future, if tiles have a %, obviously check this for the most logical)
//      THEN CALL RECALCULATENEIGHBORS(), which passes in a list of possible
//          neighbors to the tile you're using as well as it's neighbor index
//          for the given tile (do for all neighbors THAT DO NOT MAKE SENSE!
//                  (ie, that do not have this tile in their possible neighbors lists).
//          THIS SHOULD recalc. the possibilities for the surrounding tiles,
//          which will then double check if their own neighbors make sense.
//          (calling on them if they don't, as above...)
//


