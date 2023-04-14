using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using UnityEngine;

public class ShadowMaster : MonoBehaviour
{
    [SerializeField] public GameObject[] shadows;

    GameObject GetShadow(int n)
    {
        switch(n)
        {
            case -1:
                return shadows[shadows.Length - 1];
            case 0:
            case 1:
            case 18:
            case 19:
            case 20:
                return null;
            default:
                return shadows[n - 2];
        }
    }

    public void AssignShadows()
    {
        foreach(Tile t in WFC.tileArray)
        {
            if (t.collapsed || t.tileNum != 0)
            {
                GameObject g = GetShadow(t.tileNum);
                if (g != null)
                {
                    Instantiate(g, t.transform);
                }
            }
            else
            {
                Instantiate(GetShadow(-1), t.transform);
            }
        }
    }
}
