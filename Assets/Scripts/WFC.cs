using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WFC : MonoBehaviour
{
    const float spawnOffset = 0.5f;
    public static short xLen = 32, yLen = 20;

    int times = xLen * yLen;
    int maxIterations = 3;
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

                t.AssignStartingSuperpositions(x, y);

                AssignNeighbors(t,x,y);

                for(int i = 0; i<4; i++)
                {
                    if (t.neighbors[i] == null)
                    {
                        t.possibleNeighboringSuperpositions[i] = new List<short> { 0 };
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

            t.neighbors[3].possibleNeighboringSuperpositions[1] = t.superpositions;
            t.possibleNeighboringSuperpositions[3] = t.neighbors[3].superpositions;
        }

        if(y - 1 >= 0)
        {
            t.neighbors[2] = tileArray[x, y - 1];
            tileArray[x, y - 1].neighbors[0] = t;

            t.neighbors[2].possibleNeighboringSuperpositions[0] = t.superpositions;
            t.possibleNeighboringSuperpositions[2] = t.neighbors[2].superpositions;
        }

        
    }

    Tile ChooseNextTile(bool firstTime)
    {
        Tile next = null;

        if (firstTime)
        {
            next = tileArray[Random.Range(0, xLen), Random.Range(0 , yLen)];
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

    public static void ChangeTile(Tile t, short num)
    {
        t.sprite.sprite = staticAllSprites[num];
    }

    public static bool IsNeighborValid(bool[] current, bool[] neighbor, int index)
    {
        return (current[index] == neighbor[(index + 3) % 4] && current[(index + 1) % 4] == neighbor[(index + 2) % 4]);
    }

    void WaveFunctionCollapse()
    {
        TilesMaster.collapsedTiles = 0;
        for (int temp = 0; temp < times * maxIterations && TilesMaster.collapsedTiles < xLen * yLen; temp++)
        {
            Tile t = ChooseNextTile(temp > 0 ? false : true);

            if (t == null) return; 

            t.CollapseTile();
        }

        Fun();
    }

    void Fun()
    {
        List<string> strings1 = new List<string>() { "WAVE", "FUNCTION", "COLLAPSE" };
        string[] strings2 = new string[3];
        for (int i = 0; i < strings2.Length; i++)
        {
            int r = Random.Range(0, strings1.Count);
            strings2[i] = strings1[r];
            strings1.RemoveAt(r);
        }
        if (strings2[1].CompareTo("FUNCTION") == 0)
        {
            strings2[1] += "E";
        }
        Debug.LogFormat("YOU'RE TELLING ME A <color=cyan>{0}</color> " +
            "<color=magenta>{1}D</color> THIS <color=lime>{2}</color>?!", strings2[0], strings2[1], strings2[2]);
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


