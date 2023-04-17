using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movementInput;
    [SerializeField] float speed = 100;
    public static bool keyGet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        keyGet = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal == 0 && vertical == 0)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        movementInput = new Vector2 (horizontal, vertical);
        rb.velocity = movementInput * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            //KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Destroy(this.gameObject);
        Debug.Log("Y O U   H A V E   D I E D .");
        ScreenTransition.beginTransition = true;
    }
}
