using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public float SpawnInterval = 10f;
    public List<PowerUp> PowerUpTypes;
    public GameObject Background;
    public float MinDistanceFromPlayer = 3f;

    private float TimeLastIncreased = 0f;
    private float TimeLastSpawned = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - TimeLastSpawned >= SpawnInterval)
        {
            SpawnRandomPowerUp();
            TimeLastSpawned = Time.time;
        }
    }

    private void SpawnRandomPowerUp()
    {
        int random = Random.Range(0, PowerUpTypes.Count);
        var newPowerUp = PowerUpTypes[random];

        Vector2 randomLocation;
        randomLocation = GetRandomLocationWithMinimumDistanceToPlayer();

        Instantiate(newPowerUp, randomLocation, Quaternion.identity);
    }

    private Vector2 GetRandomLocationWithMinimumDistanceToPlayer()
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
