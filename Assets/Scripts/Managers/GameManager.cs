using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Score = 0; // score
    public static int EnemiesKilled = 0; // wieviele gegner wurden gekillt
    private bool GameOver = false;
    private float TimePassed = 0f;

    private void Start() 
    {
        EnemiesKilled = 0;
    }


    void Update() // wenn das spiel vorbei ist score berechnen
    {
        if (!GameOver)
        {
            TimePassed += Time.deltaTime;
        }
        Score = (int)(EnemiesKilled * 5 + TimePassed * 2);
    }
}
