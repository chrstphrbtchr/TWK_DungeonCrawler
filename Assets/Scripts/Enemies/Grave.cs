using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public GameObject ghostPrefab;
    bool ghostSpawned;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && !ghostSpawned)
        {
            ghostSpawned = true;
            Ghost boo = Instantiate(ghostPrefab, this.transform.position, Quaternion.identity).GetComponent<Ghost>();
            boo.home = this.gameObject;
        }
    }
}
