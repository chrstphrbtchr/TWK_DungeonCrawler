using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Vector2 nextLocation;
    public GameObject player;
    public bool aggro, moving;
    public float speed, timeTilMove, timeTilMoveMax;

    public abstract void GetNextLocation();

    public bool ReadyToMove()
    {
        return (timeTilMove > timeTilMoveMax && !moving);
    }

    public abstract IEnumerator MoveEnemy();
}
