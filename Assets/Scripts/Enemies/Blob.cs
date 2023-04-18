using System.Collections;
using UnityEngine;

public class Blob : Enemy
{
    private void Awake()
    {
        timeTilMove = 0;
        moveTimeRange = new Vector2(0.25f, 1f);
        timeTilMoveMax = GetRandomMoveTime();
        speed = 83f;
        rb = GetComponent<Rigidbody2D>();
    }
    
    public override void GetNextLocation()
    {
        if (aggro && player != null)
        {
            nextLocation = (this.transform.position - player.transform.position) * -0.5f;
        }
        else
        {
            Vector2 temp = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            nextLocation = (temp * 0.5f);
        }
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
        nextLocation = transform.position;
        timeTilMove = 0;
        timeTilMoveMax = GetRandomMoveTime();
        this.moving = false;
        yield return null;
    }
}
