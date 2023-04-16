using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MultiStateEnemy
{
    [SerializeField] Sprite skeleSprite;
    [SerializeField] Sprite bonesSprite;
    SpriteRenderer spRend;
    public override void GetNextLocation()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator MoveEnemy()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAggroBegin()
    {
        // get out of bones mode
        // get into skeleton mode
        aggro = true;
        
    }

    public override void OnAggroEnd()
    {
        // become bones again...
        aggro = false;
    }

    void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        spRend.sprite = bonesSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
