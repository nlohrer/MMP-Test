using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Score = 0;
    public static int EnemiesKilled = 0;
    private int Coins = 0;
    private bool GameOver = false;
    private float TimePassed = 0f;

    private void Start()
    {
        EnemiesKilled = 0;
    }


    void Update()
    {
        if (!GameOver)
        {
            TimePassed += Time.deltaTime;
        }
        Score = (int)(EnemiesKilled * 5 + TimePassed * 2);
    }
}
