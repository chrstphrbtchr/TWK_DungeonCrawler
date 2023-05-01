using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShieldGet : MonoBehaviour
{
    public Material mat;

    private void Awake()
    {
        mat.SetInt("_GotItem", 0);
    }
    private void OnEnable()
    {
        Shield.OnShieldGet += ItemPickup;
        PlayerMove.OnShieldDestroyed += ItemLost;
    }

    private void OnDisable()
    {
        Shield.OnShieldGet -= ItemPickup;
        PlayerMove.OnShieldDestroyed -= ItemLost;
    }

    void ItemPickup()
    {
        mat.SetInt("_GotItem", 1);
    }

    void ItemLost()
    {
        mat.SetInt("_GotItem", 0);
    }
}
