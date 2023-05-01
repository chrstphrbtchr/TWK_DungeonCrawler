using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KeyGet : MonoBehaviour
{
    public Material mat;
    public Image img;

    private void Awake()
    {
        mat.SetInt("_GotItem", 0);
        img = GetComponent<Image>();
    }
    private void OnEnable()
    {
        Key.OnKeyGet += ItemPickup;
        Door.OnDoorLocked += Flash;
    }

    private void OnDisable()
    {
        Key.OnKeyGet -= ItemPickup;
        Door.OnDoorLocked -= Flash;
    }

    void ItemPickup()
    {
        mat.SetInt("_GotItem", 1);
    }

    void Flash()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        Color temp = img.color;
        Color fade = new Color(temp.r, temp.g, temp.b, 0.2f);
        for(int i = 0; i < 6; i++)
        {
            img.color = (i % 2 == 0) ? fade : temp;
            yield return new WaitForSeconds(0.25f);
        }
        yield return null;
    }
}
