using System.Collections;
using UnityEngine;

public class Blob : Enemy
{
    private void Awake()
    {
        timeTilMove = 0;
        timeTilMoveMax = 1.75f;
        speed = 95;
        rb = GetComponent<Rigidbody2D>();
    }
    
    public override void GetNextLocation()
    {
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
        while(time < totalTime)
        {
            rb.velocity = nextLocation * speed * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        nextLocation = Vector2.zero;
        timeTilMove = 0;
        this.moving = false;
        yield return null;
    }
}
