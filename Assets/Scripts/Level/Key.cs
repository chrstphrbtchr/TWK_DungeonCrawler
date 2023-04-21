using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public delegate void GotKey();
    public static event GotKey OnKeyGet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerMove.keyGet = true;
            OnKeyGet();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMove.keyGet = true;
            OnKeyGet();
            Destroy(this.gameObject);
        }
    }
}
