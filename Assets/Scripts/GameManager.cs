using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int Score = 0;
    private int Coins = 0;
    private int EnemiesKilled = 0;
    private bool GameOver = false;
    private float TimePassed = 0f;


    void Update()
    {
        if (!GameOver)
        {
            TimePassed += Time.deltaTime;
        }
        Score = (int)(EnemiesKilled * 5 + TimePassed * 2);
    }
}
