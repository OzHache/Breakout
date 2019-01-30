using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 8f;
    public GameObject Paddle;
    public bool inPlay = false;

    Rigidbody2D rb;         //assign in start
    private float velX = 0;
    private float velY = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //assigned once
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        //Rotate to a random direction
        transform.rotation = Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(-60f, 60f));
        //Send the ball flying

        //set inplay to true
    }
}
