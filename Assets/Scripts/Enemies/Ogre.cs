using System.Collections;
using UnityEngine;

public class Ogre : Enemy
{
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timeTilMove = 0;
        moveTimeRange = new Vector2(1f, 4f);
        timeTilMoveMax = GetRandomMoveTime();
        speed = 70f;
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

        if (!aggro && CanSeePlayer())
        {
            aggro = true;
        }
    }

    public override void GetNextLocation()
    {
        if (aggro && player != null)
        {
            nextLocation = -(this.transform.position - player.transform.position);
        }
        else
        {
            Vector2 temp = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
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
            nextLocation = transform.position;
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
        timeTilMoveMax = GetRandomMoveTime();
        this.moving = false;
        yield return null;
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
