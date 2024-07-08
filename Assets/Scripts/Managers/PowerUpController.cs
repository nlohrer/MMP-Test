using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public float SpawnInterval = 10f; // wie oft powerups?
    public List<PowerUp> PowerUpTypes; // welche powerups?
    public GameObject Background; 
    public float MinDistanceFromPlayer = 3f; // nicht auf player spawnen

    private float TimeLastSpawned = 0f; // tracken von spawns

    void Update() // alle SpawnInterval sekunden powerup spawnen
    {
        if (Time.time - TimeLastSpawned >= SpawnInterval)
        {
            SpawnRandomPowerUp();
            TimeLastSpawned = Time.time;
        }
    }

    private void SpawnRandomPowerUp() // random power up aus der liste spawnen
    {
        int random = Random.Range(0, PowerUpTypes.Count); // random powerup auswählen
        var newPowerUp = PowerUpTypes[random];

        Vector2 randomLocation; // random location berechnen
        randomLocation = GetRandomLocationWithMinimumDistanceToPlayer();

        Instantiate(newPowerUp, randomLocation, Quaternion.identity); // powerup spawnen
    }

    private Vector2 GetRandomLocationWithMinimumDistanceToPlayer() // solange position berechnen, bis 3 units vom player entfernt
    {
        float xSpawn = Background.transform.localScale.x / 2 - 1;     // half the horizontal length of the background
        float ySpawn = Background.transform.localScale.y / 2 - 1;     // half the vertical width of the background

        float randomX = Random.Range(-xSpawn, xSpawn + 1);
        float randomY = Random.Range(-ySpawn, ySpawn + 1);

        var position = new Vector2(randomX, randomY);
        var playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().position;
        if (Vector2.Distance(position, playerPosition) < MinDistanceFromPlayer)
        {
            return GetRandomLocationWithMinimumDistanceToPlayer();
        }
        else
        {
            return position;
        }
    }
    
}
