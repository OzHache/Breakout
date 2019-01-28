using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    public float speed = 8f;
    public GameObject Paddle;
    public bool inPlay = false;

    private bool colllisionUpdateTwice = false;
    private Rigidbody2D rb;
    private Vector2 collisionObject;
    private float velX = 0;
    private float velY = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.sqrMagnitude != speed && inPlay == true)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
    private void FixedUpdate()
    {
        VelocityUpdateMethod();
        colllisionUpdateTwice = false;
    }

    private void VelocityUpdateMethod()
    {
        if (rb.velocity.x == 0 || rb.velocity.y == 0)
        {
            if (velX == 0 && velY == 0)
            {
                return;
            }

            if (rb.velocity.x != 0)
            {
                velX = rb.velocity.x;
            }
            if (rb.velocity.y != 0)
            {
                velY = rb.velocity.y;
            }

        }
        velX = rb.velocity.x;
        velY = rb.velocity.y;
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
        // todo: average out the collisions on multiple collisions so that the ball never goes flat. 

        if (colllisionUpdateTwice)
        {
            Debug.Log("Updated Collisions twice in one frame");
            return;
        } else
        {
            colllisionUpdateTwice = true;
        }
        switch (collision.collider.tag)
        {
            case "Brick":
                collision.gameObject.GetComponent<Brick>()
                .HitBlock();
                NormalReturn(collision);
                break;
            case "Paddle":
                PaddleReturn(collision);
                break;
            default:
                NormalReturn(collision);
                break;
        }
        VelocityUpdateMethod();
    }
    
    void NormalReturn(Collision2D collision)
    {
        Vector3 collisionCenter = collision.collider.bounds.center;
        Vector3 collisionExtends = collision.collider.bounds.extents;
        Vector3 myPos = transform.position;
        //determine which side has been hit

        //Top or bottom
        if (collisionCenter.x + collisionExtends.x > myPos.x && myPos.x > collisionCenter.x - collisionExtends.x)
        {
            rb.velocity = new Vector2(velX, -velY);
        }
        else
        //Sides
        if (collisionCenter.y + collisionExtends.y > myPos.y && myPos.y > collisionCenter.y - collisionExtends.y)
        {
            rb.velocity = new Vector2(-velX, velY);
        }
        //Corners
        else
        {
            rb.velocity = new Vector2(-velX, -velY);
        }
    }
    void PaddleReturn(Collision2D collision)
    {
        //todo: if I hit the paddle, take the complemetary angle from the center of the ball to the paddle if in the middle, 
        //if on the edge send out on the same angle from the ball to the center of the paddle
        Vector2 ballPosition = transform.position;
        Vector2 paddlePosition = collision.transform.position;

        if(velX > 0) // going to the right
        {
            rb.velocity = new Vector2(rb.velocity.x + Mathf.Abs(ballPosition.x - paddlePosition.x), -velY);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x - Mathf.Abs(ballPosition.x - paddlePosition.x), -velY);
        }
    }
}
