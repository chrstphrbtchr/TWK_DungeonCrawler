using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MultiStateEnemy
{
    public GameObject home;
    bool doneSpawning, returningHome;
    SpriteRenderer sprite;
    float transparentThreshold = 0.5f;
    public override void GetNextLocation()
    {
        if (aggro)
        {
            if (player == null) nextLocation = transform.position;
            nextLocation = player.transform.position;
        }
        else
        {
            nextLocation = home.transform.position;
        }
    }

    public override IEnumerator MoveEnemy()
    {
        moving = true;
        if (!doneSpawning) yield return null;
        transform.position = Vector2.MoveTowards(this.transform.position, 
            nextLocation, speed * Time.deltaTime);
        moving = false;
        yield return null;
    }

    public override void OnAggroBegin()
    {
        // Fade in!
        aggro = true;
        returningHome = false;
        StopCoroutine(Fade());
        StartCoroutine(Fade());
    }

    public override void OnAggroEnd()
    {
        // Fade out!
        aggro = false;
        returningHome = true;
        StopCoroutine(Fade());
        StartCoroutine(Fade(true));
    }

    private void Awake()
    {
        doneSpawning = false;
        sprite = GetComponent<SpriteRenderer>();
        speed = 0.6f;
        timeTilMove = 0;
        timeTilMoveMax = 0;
        aggro = true;
        StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {
        if (doneSpawning)
        {
            if(!moving)
            {
                GetNextLocation();
                StartCoroutine(MoveEnemy());
            }

            if(!aggro)
            {
                if (returningHome && Vector2.Distance(
                    transform.position, home.transform.position) <= 0.02f)
                {
                    home.GetComponent<Grave>().ghostSpawned = false;
                    StartCoroutine(Fade(true, true));
                    
                }
            }
            
        }
    }

    IEnumerator Fade(bool fadeOut=false, bool lastTime=false)
    {
        float alpha = sprite.color.a;

        while ((fadeOut ? (!aggro && alpha > 0) : (aggro && alpha < transparentThreshold)))
        {
            alpha += (fadeOut ? -Time.deltaTime : Time.deltaTime) * 6.66f;
            alpha = Mathf.Clamp01(alpha);
            Color fixedAlpha = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            sprite.color = fixedAlpha;
            yield return new WaitForSeconds(0.175f);
        }

        alpha = (fadeOut ? 0 : transparentThreshold);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);

        if (!doneSpawning) doneSpawning = true;

        if(lastTime)
        {
            Destroy(this.gameObject);
        }
        yield return null;
    }

    public new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        OnAggroBegin();
    }

    public new void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        OnAggroEnd();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player" || !doneSpawning || !aggro) return;

        if(Vector2.Distance(this.transform.position, player.transform.position) < 0.25f)
        {
            player.GetComponent<PlayerMove>().KillPlayer();
        }
    }
}
