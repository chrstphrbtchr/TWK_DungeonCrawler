using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator shieldAnimator;

    Rigidbody2D rb;
    SpriteRenderer spriteRend;

    Vector2 movementInput;
    float speed = 90f;

    public static bool keyGet, shieldGet, iFrames;

    public delegate void ShieldDestroyer();
    public static event ShieldDestroyer OnShieldDestroyed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
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
        if (iFrames) return;
        if(shieldGet)
        {
            shieldGet = false;
            StartCoroutine(Invincibility());
            OnShieldDestroyed();
            shieldAnimator.SetTrigger("Broken");
            return;
        }
        Destroy(this.gameObject);
        Debug.Log("Y O U   H A V E   D I E D .");
        ScreenTransition.beginTransition = true;
    }

    IEnumerator Invincibility()
    {
        iFrames = true;
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.125f);
            spriteRend.enabled = false;
            yield return new WaitForSeconds(0.125f);
            spriteRend.enabled = true;
        }
        iFrames = false;
        shieldAnimator.ResetTrigger("Broken");
        yield return null;
    }
}
