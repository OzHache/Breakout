using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ScoreText;
    public Text LevelText;
    public Text LivesText;
    public Rigidbody2D Paddle;
    public Ball ball;

    public static GameManager GetGameManager;       //Self Reference

    private int score = 0;
    private int level = 0;
    private int life = 3;
    [Range(1f,10f)]
    [SerializeField] float Speed = 7;

    // Start is called before the first frame update
    void Start()
    {
        GetGameManager = this;
        DontDestroyOnLoad(this);
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (this != GetGameManager)
            Destroy(this);
        if (!ball.inPlay && Input.GetKeyDown(KeyCode.Space))
        {
            ball.Launch();
        }
    }
    private void FixedUpdate()
    {
        MovePaddle();
    }

    void UpdateUI()
    {
        ScoreText.text = score.ToString();
        LevelText.text = level.ToString();
        LivesText.text = life.ToString();
    }

    public void Score(int value)
    {
        score += value;
        UpdateUI();
    }

    //TODO: make a level transition for destroying all the blocks
    void MovePaddle()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Paddle.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Speed, 0);
        } else
        {
            Paddle.velocity = Vector2.zero;
        }        
    }

    public void Loss()
    {
        life--;
        UpdateUI();
    }
}
