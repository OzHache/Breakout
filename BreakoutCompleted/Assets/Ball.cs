using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    public float speed = 8f;
    public GameObject Paddle;
    public bool inPlay = false;

    private Rigidbody2D rb;
    private Vector2 collisionObject;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.sqrMagnitude > speed && inPlay == true)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.parent = Paddle.transform;
        transform.localPosition = new Vector3(0f, .3f, 0f);
        inPlay = false;
    }

    public void Launch()
    {
        //Rotate to a random direction. 
        gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(-60f, 60f));
        rb.velocity = transform.up * speed;
        inPlay = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            ResetBall();
            GameManager.GetGameManager.Loss();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Brick"))
        {
            collision.gameObject.GetComponent<Brick>()
                .HitBlock();
        }
    }
}
