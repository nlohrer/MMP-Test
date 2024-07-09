using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float SpawnInterval = 5f; // wie oft werden enemies gespawned?
    public List<Enemy> EnemyTypes;
    public GameObject Background;
    public float MinDistanceFromPlayer = 5f;


    private float TimeLastIncreased = 0f; // tracker für spawnrate reduzierung
    private float TimeLastSpawned = 0f; // tracker für spawns

    private void Start() // am anfang 5 gegner spawnen
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnRandomEnemy();
        }
    }

    // Update is called once per frame
    void Update() // nach SpawnInterval gegner spawnen und alle 20 sekunden spawn intervall runtersetzen
    {
        if (Time.time - TimeLastSpawned >= SpawnInterval)
        {
            SpawnRandomEnemy();
            TimeLastSpawned = Time.time;
        }
        if (SpawnInterval >= 1f && Time.time - TimeLastIncreased >= 20f)
        {
            SpawnInterval -= 0.5f;
            TimeLastIncreased = Time.time;
        }
    }

    private void SpawnRandomEnemy() // random enemy spawnen
    {
        int random = Random.Range(0, EnemyTypes.Count); //random enemy auswählen
        var newEnemy = EnemyTypes[random];

        float randomAngle = Mathf.Deg2Rad * Random.Range(0, 360); // random schwimm richtung berechnen
        Vector2 movement = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle));
        newEnemy.Movement = movement;

        Vector2 randomLocation; // random location (5 units vom spieler weg berechnen)
        randomLocation = GetRandomLocationOutsideOfCamera();

        Instantiate(newEnemy, randomLocation, Quaternion.identity); // enemy spawnen 
    }

    private Vector2 GetRandomLocationOutsideOfCamera() // random location solange berechnen bis 5 units entfernt vom spieler
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
        }
        else
        {
            return position;
        }
    }
}
