using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Enemy
{
    public override void GetNextLocation()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator MoveEnemy()
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
