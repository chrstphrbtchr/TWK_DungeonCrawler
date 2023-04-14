using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Enemy
{
    Rigidbody2D rb;
    private void Awake()
    {
        timeTilMove = 0;
        timeTilMoveMax = 1.75f;
        speed = 95;
        rb = GetComponent<Rigidbody2D>();
    }
    
    public override void GetNextLocation()
    {
        /*
        Vector2 mvmt = new Vector2(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));
        nextLocation = (Vector2)this.transform.position + mvmt;
        */

        Vector2 temp = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        nextLocation = (temp * 0.5f);
    }

    void Update()
    {
        if (ReadyToMove())
        {
            StartCoroutine(MoveEnemy());
        }

        if(!moving)
        {
            this.timeTilMove += Time.deltaTime;
        }
    }

    public override IEnumerator MoveEnemy()
    {
        this.moving = true;
        GetNextLocation();
        float time = 0, totalTime = 1.25f;
        //Vector2 current = this.transform.position;
        while(time < totalTime)
        {
            rb.velocity = nextLocation * speed * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        
        /*while (Vector2.Distance(transform.position, nextLocation) > 0)
        {
            time += Time.deltaTime;
            this.transform.position = Vector2.Lerp(current, nextLocation, time / totalTime);
            yield return null;
        }*/
        //this.transform.position = nextLocation;
        nextLocation = Vector2.zero;
        timeTilMove = 0;
        this.moving = false;
        yield return null;
    }
}
