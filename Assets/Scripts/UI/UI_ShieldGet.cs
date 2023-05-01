using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShieldGet : MonoBehaviour
{
    public Material mat;

    private void Awake()
    {
        if (PlayerMove.shieldGet)
        {
            mat.SetFloat("_Progress", 0);
        }
        else
        {
            mat.SetFloat("_Progress", 1);
        }
        
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
        mat.SetFloat("_Progress", 0);
    }

    void ItemLost()
    {
        StartCoroutine(Dissolver());
    }

    IEnumerator Dissolver()
    {
        float prog = .25f;
        while(prog < 1f)
        {
            mat.SetFloat("_Progress", prog);
            prog += Time.deltaTime * 0.75f;
            yield return new WaitForEndOfFrame();
        }
        mat.SetFloat("_Progress", 1);
        yield return null; 

    }
}
