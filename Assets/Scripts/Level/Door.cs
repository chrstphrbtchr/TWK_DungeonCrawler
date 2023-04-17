using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        if (PlayerMove.keyGet)
        {
            ScreenTransition.beginTransition = true;
        }
        else
        {
            Debug.Log("<color=#BF7959>It's locked...</color>");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        if (PlayerMove.keyGet)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("<color=#BF7959>It's locked...</color>");
        }
    }
}
