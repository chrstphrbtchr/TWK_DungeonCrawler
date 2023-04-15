using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject door;
    public GameObject key;
    public GameObject shield;

    public GameObject[] monsters;
    

    public void PlacePieces(List<Tile> places)
    {
        bool placed = false;
        Tile t = null;
        Vector2 temp = Vector2.zero, baseOffset = new Vector2(0.5f, 0.5f);
        PlacementHelper(player);
        PlacementHelper(key);
        PlacementHelper(door);
        PlacementHelper(monsters[0]);   // FIX LATER, JUST FOR TESTING NOW....
        PlacementHelper(monsters[1]);   // SEE ABOVE..........................

        void PlacementHelper(GameObject g)
        {
            placed = false;
            while (!placed)
            {
                t = places[Random.Range(0, places.Count)];
                places.Remove(t);
                if (CheckValidity(t))
                {
                    temp = ((baseOffset * t.tileLoc) + GetOffset(t));
                    Instantiate(g, temp, Quaternion.identity);
                    placed = true;
                }
            }
        }

        bool CheckValidity(Tile t)
        {
            switch (t.tileNum)
            {
                case -1:
                case  0: return false;
                case 10:
                    if (places.Contains(t.neighbors[1]) &&
                        places.Contains(t.neighbors[2]) &&
                        places.Contains(t.neighbors[1].neighbors[2]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 11:
                    if (places.Contains(t.neighbors[2]) &&
                        places.Contains(t.neighbors[3]) &&
                        places.Contains(t.neighbors[3].neighbors[2]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 12:
                    if (places.Contains(t.neighbors[0]) &&
                        places.Contains(t.neighbors[1]) &&
                        places.Contains(t.neighbors[1].neighbors[0]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 13:
                    if (places.Contains(t.neighbors[0]) &&
                        places.Contains(t.neighbors[3]) &&
                        places.Contains(t.neighbors[3].neighbors[0]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 19:
                case 20: return false;
                default:
                    return true;
            }
        }
    }

    Vector2 GetOffset(Tile t)
    {
        switch (t.tileNum)
        {
            case 10: return new Vector2( 0.5f, -0.5f);
            case 11: return new Vector2(-0.5f, -0.5f);
            case 12: return new Vector2( 0.5f,  0.5f);
            case 13: return new Vector2(-0.5f,  0.5f);
            default: return Vector2.zero;
        }
    }
}
