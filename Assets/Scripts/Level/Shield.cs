using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public delegate void GotShield();
    public static event GotShield OnShieldGet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!PlayerMove.shieldGet)
            {
                PlayerMove.shieldGet = true;
                OnShieldGet();
            }
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!PlayerMove.shieldGet)
            {
                PlayerMove.shieldGet = true;
                OnShieldGet();
            }
            Destroy(this.gameObject);
        }
    }
}
