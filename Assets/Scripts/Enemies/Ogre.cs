using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Enemy
{
    public override void GetNextLocation()
    {
        if (aggro && player != null)
        {
            nextLocation = -(this.transform.position - player.transform.position);
        }
        else
        {
            Vector2 temp = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            nextLocation = (temp * 0.5f);
        }
    }

    public override IEnumerator MoveEnemy()
    {
        this.moving = true;
        if (!aggro)
        {
            GetNextLocation();
            float time = 0, totalTime = 2f;
            while (time < totalTime)
            {
                rb.velocity = nextLocation * speed * Time.deltaTime;
                time += Time.deltaTime;
                yield return null;
            }
            nextLocation = Vector2.zero;
            timeTilMove = 0;
        }
        else
        {
            while (aggro && CanSeePlayer())
            {
                GetNextLocation();
                rb.velocity = nextLocation * speed * Time.deltaTime;
                yield return null;
            }
        }
        this.moving = false;
        yield return null;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timeTilMove = 0;
        timeTilMoveMax = 3f;
        speed = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ReadyToMove() || (aggro && !moving))
        {
            StartCoroutine(MoveEnemy());
        }

        if (!moving && !aggro)
        {
            this.timeTilMove += Time.deltaTime;
        }

        if(!aggro && CanSeePlayer())
        {
            aggro = true;
        }
    }

    public new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player == null) player = collision.gameObject;
            if(CanSeePlayer())
            {
                aggro = true;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (player == null) player = collision.gameObject;

        if (CanSeePlayer())
        {
            aggro = true;
        }
    }

    public bool CanSeePlayer()
    {
        if(player == null) return false;
        Vector2 dir = -(this.transform.position - player.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, 5);
        if(hit.collider != null)
        {
            return (hit.collider.tag == "Player" ? true : false);
        }
        return false;
    }
}
