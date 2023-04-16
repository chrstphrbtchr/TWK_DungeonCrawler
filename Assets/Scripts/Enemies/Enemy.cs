using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Vector2 nextLocation, moveTimeRange;
    public GameObject player;
    public Rigidbody2D rb;
    public bool aggro, moving;
    public float speed, timeTilMove, timeTilMoveMax;

    public abstract void GetNextLocation();

    public bool ReadyToMove()
    {
        return (timeTilMove > timeTilMoveMax && !moving);
    }

    public abstract IEnumerator MoveEnemy();

    public float GetRandomMoveTime()
    {
        return Random.Range(moveTimeRange.x, moveTimeRange.y);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (player == null) player = collision.gameObject;
            player.GetComponent<PlayerMove>().KillPlayer();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player == null) player = collision.gameObject;
            aggro = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            aggro = false;
        }
    }
}
