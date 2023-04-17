using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MultiStateEnemy
{
    public BoxCollider2D box;

    [SerializeField] Sprite skeleSprite;
    [SerializeField] Sprite bonesSprite;
    SpriteRenderer spRend;
    
    bool doneSpawning;
    float bcOff = 2.5f, bcOn = 9f;

    public override void GetNextLocation()
    {
        if(player == null) nextLocation = this.transform.position;
        timeTilMoveMax = GetRandomMoveTime();
        nextLocation = player.transform.position;
    }

    public override IEnumerator MoveEnemy()
    {
        moving = true;
        GetNextLocation();
        while(timeTilMove < timeTilMoveMax)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                nextLocation, speed * Time.deltaTime);
            timeTilMove += Time.deltaTime;
        }
        timeTilMove = 0;
        moving = false;
        yield return null;
    }

    public override void OnAggroBegin()
    {
        // get out of bones mode
        // get into skeleton mode
        if(!doneSpawning)
        {
            StartCoroutine(IntoAggro());
        }
    }

    public override void OnAggroEnd()
    {
        // become bones again...
        if (doneSpawning)
        {
            StartCoroutine(OutOfAggro());
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spRend = GetComponent<SpriteRenderer>();
        spRend.sprite = bonesSprite;
        timeTilMove = 0;
        moveTimeRange = new Vector2(0.5f, 1.25f);
        timeTilMoveMax = GetRandomMoveTime();
        speed = 0.0175f;
        doneSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(aggro && !moving)
        {
            StartCoroutine(MoveEnemy());
        }
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if(player == null) player = collision.gameObject;

        OnAggroBegin();
    }

    private new void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        OnAggroEnd();
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && aggro && doneSpawning)
        {
            //if (player == null) player = collision.gameObject;
            player.GetComponent<PlayerMove>().KillPlayer();
            aggro = false;
        }
    }

    IEnumerator IntoAggro()
    {
        for (int i = 0; i < 4; i++)
        {
            spRend.sprite = skeleSprite;
            yield return new WaitForSeconds(0.125f);
            spRend.sprite = bonesSprite;
            yield return new WaitForSeconds(0.125f);
        }
        yield return new WaitForSeconds(0.025f);
        spRend.sprite = skeleSprite;

        aggro = true;
        doneSpawning = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        box.size = new Vector2(bcOn, bcOn);
        yield return null;
    }

    IEnumerator OutOfAggro()
    {
        for (int i = 0; i < 4; i++)
        {
            spRend.sprite = bonesSprite;
            yield return new WaitForSeconds(0.025f);
            spRend.sprite = skeleSprite;
            yield return new WaitForSeconds(0.025f);
            
        }
        yield return new WaitForSeconds(0.025f);
        spRend.sprite = bonesSprite;

        aggro = false;
        doneSpawning = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        box.size = new Vector2(bcOff, bcOff);
        yield return null;
    }
}
