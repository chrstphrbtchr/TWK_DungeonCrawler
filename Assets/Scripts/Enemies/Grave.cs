using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public GameObject ghostPrefab;
    public bool ghostSpawned;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if(!ghostSpawned)
        {
            ghostSpawned = true;
            Ghost boo = Instantiate(ghostPrefab, this.transform.position, 
                Quaternion.identity).GetComponent<Ghost>();
            boo.home = this.gameObject;
            boo.player = collision.gameObject;
        }
    }
}
