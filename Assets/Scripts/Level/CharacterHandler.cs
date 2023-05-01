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

    public static int monstersToMake;
    Vector2 playerLocation;

    public void PlacePieces(List<Tile> places)
    {
        int rnd = Random.Range(0, 5);
        bool placed = false;
        Tile t = null;
        monstersToMake = places.Count / 80;
        Vector2 temp = Vector2.zero, baseOffset = new Vector2(0.5f, 0.5f);
        PlacementHelper(player);
        PlacementHelper(key);
        PlacementHelper(door);
        if(rnd == 0) PlacementHelper(shield);

        for(int i = 0; i < monstersToMake; i++)
        {
            rnd = Random.Range(0, monsters.Length);
            PlacementHelper(monsters[rnd], (rnd >= 2 ? true : false));
        }

        void PlacementHelper(GameObject g, bool floorsOnly=false)
        {
            placed = false;
            while (!placed)
            {
                t = places[Random.Range(0, places.Count)];
                places.Remove(t);
                if (CheckValidity(t, floorsOnly))
                {
                    temp = ((baseOffset * t.tileLoc) + GetOffset(t));
                    Instantiate(g, temp, Quaternion.identity);
                    placed = true;
                }
            }
            if(g == player) { playerLocation = g.transform.position; }
        }

        bool CheckValidity(Tile t, bool floorsOnly=false)
        {
            if (floorsOnly && t.tileNum != 1) return false;

            if(player != null && Vector2.Distance(playerLocation, t.tileLoc) < 5)
            {
                return false;
            }

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
            case  2:
            case 10: return new Vector2( 0.25f, -0.25f);
            case  3: return new Vector2(   0f,  -0.25f);
            case  4:
            case 11: return new Vector2(-0.25f, -0.25f);
            case  5: return new Vector2( 0.25f,     0f);
            case  6: return new Vector2(-0.25f,     0f);
            case  7:
            case 12: return new Vector2( 0.25f,  0.25f);
            case  8: return new Vector2(    0f,  0.25f);
            case  9:
            case 13: return new Vector2(-0.25f,  0.25f);
            default: return Vector2.zero;
        }
    }
}
