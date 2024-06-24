using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float SpawnInterval = 5f;
    public List<Enemy> EnemyTypes;
    public GameObject Background;
    public float MinDistanceFromPlayer = 5f;


    private float TimeLastIncreased = 0f;
    private float TimeLastSpawned = 0f;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnRandomEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - TimeLastSpawned >= SpawnInterval)
        {
            SpawnRandomEnemy();
            TimeLastSpawned = Time.time;
        }
        if (SpawnInterval >= 1 && Time.time - TimeLastIncreased >= 20f)
        {
            SpawnInterval -= 0.5f;
            TimeLastIncreased = Time.time;
        }
    }

    private void SpawnRandomEnemy()
    {
        int random = Random.Range(0, EnemyTypes.Count);
        var newEnemy = EnemyTypes[random];

        float randomAngle = Mathf.Deg2Rad * Random.Range(0, 360);
        Vector2 movement = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle));
        newEnemy.Movement = movement;

        Vector2 randomLocation;
        randomLocation = GetRandomLocationOutsideOfCamera();

        Instantiate(newEnemy, randomLocation, Quaternion.identity);
    }

    private Vector2 GetRandomLocationOutsideOfCamera()
    {
        float xSpawn = Background.transform.localScale.x / 2 - 1;     // half the horizontal length of the background
        float ySpawn = Background.transform.localScale.y / 2 - 1;     // half the vertical width of the background

        float randomX = Random.Range(-xSpawn, xSpawn + 1);
        float randomY = Random.Range(-ySpawn, ySpawn + 1);

        var position = new Vector2(randomX, randomY);
        var playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().position;
        if (Vector2.Distance(position, playerPosition) < MinDistanceFromPlayer)
        {
            return GetRandomLocationOutsideOfCamera();
        } else
        {
            return position;
        }
    }
}
