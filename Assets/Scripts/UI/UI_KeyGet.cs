using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_KeyGet : MonoBehaviour
{
    public Material mat;

    private void Awake()
    {
        mat.SetInt("_GotItem", 0);
    }
    private void OnEnable()
    {
        Key.OnKeyGet += ItemPickup;
    }

    private void OnDisable()
    {
        Key.OnKeyGet -= ItemPickup;
    }

    void ItemPickup()
    {
        mat.SetInt("_GotItem", 1);
    }
}
