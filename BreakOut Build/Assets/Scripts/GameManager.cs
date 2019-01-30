using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           //Added to access Text

public class GameManager : MonoBehaviour
{
    public Text ScoreText;      //add references to UI Objects
    public Text LevelText;
    public Text LivesText;
    public Rigidbody2D Paddle;  //Add Ref to Paddle
    //public Ball ball;         //todo: Add Ball class
    public static GameManager GetGameManager;
    [Range(1f, 10f)]
    public float Speed = 8;

    private int score = 0;      //values to track ui Values 
    private int level = 0;
    private int lives = 3;

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
        if(this != GetGameManager)
        {
            Destroy(this);
        }
    }
    private void FixedUpdate()
    {
        MovePaddle();
    }
    void MovePaddle()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            Paddle.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Speed, 0f);
        }
        else
        {
            Paddle.velocity = Vector2.zero;
        }
    }

    void UpdateUI()
    {
        ScoreText.text = score.ToString();
        LevelText.text = level.ToString();
        LivesText.text = lives.ToString();
    }
}
