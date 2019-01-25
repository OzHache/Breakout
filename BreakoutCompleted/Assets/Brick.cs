using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum BrickColor {White, DarkBlue, Red, LightBlue, Yellow }

public class Brick : MonoBehaviour
{
    public static List<GameObject> GetBricks = new List<GameObject>();
    public BrickColor CurrentColor;
    public Sprite[] Colors;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetBricks.Add(this.gameObject);
    }

    public void HitBlock()
    {
        if (spriteRenderer.sprite == Colors[0])
        {
            GetBricks.Remove(this.gameObject);
            Destroy(this.gameObject, .05f);
        }
        else
        {
            spriteRenderer.sprite = Colors[Array.IndexOf(Colors, spriteRenderer.sprite) - 1]; // need using System;
        }
        GameManager.GetGameManager.Score(50);

    }


}
