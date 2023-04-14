using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Enemy
{
    private void Awake()
    {
        timeTilMove = 0;
        timeTilMoveMax = 4;
        speed = 2;
    }
    
    public override void GetNextLocation()
    {
        nextLocation = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
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
        Vector2 current = this.transform.position;
        while (Vector2.Distance(transform.position, nextLocation) > 0)
        {
            time += Time.deltaTime;
            this.transform.position = Vector2.Lerp(current, nextLocation, time / totalTime);
            yield return null;
        }
        this.transform.position = nextLocation;
        nextLocation = Vector2.zero;
        timeTilMove = 0;
        this.moving = false;
        yield return null;
    }
}
