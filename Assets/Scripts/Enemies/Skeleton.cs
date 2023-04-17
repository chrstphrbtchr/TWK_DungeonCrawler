using System.Collections;
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
        nextLocation = -(this.transform.position - player.transform.position);
    }

    public override IEnumerator MoveEnemy()
    {
        moving = true;
        float timer = 0;
        GetNextLocation();
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(0.125f);
        while(timer < timeTilMoveMax)
        {
            rb.velocity = nextLocation * speed * Time.deltaTime;
            timer += Time.deltaTime;
            /*
            transform.position = Vector2.Lerp(transform.position, 
                nextLocation, timer/timeTilMoveMax);
            
            yield return new WaitForSeconds(0.01f);*/
        }
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
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
        moveTimeRange = new Vector2(0.25f, 1f);
        timeTilMoveMax = GetRandomMoveTime();
        speed = 130f;
        doneSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(aggro && !moving)
        {
            if (ReadyToMove())
            {
                StartCoroutine(MoveEnemy());
            }
            else
            {
                timeTilMove += Time.deltaTime;
            }
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
            yield return new WaitForSeconds(0.075f);
            spRend.sprite = skeleSprite;
            yield return new WaitForSeconds(0.075f);
            
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
