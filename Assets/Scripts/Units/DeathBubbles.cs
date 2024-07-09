using UnityEngine;

public class DeathBubbles : MonoBehaviour
{
    private float TimeAlive;

    private void Start()
    {
        TimeAlive = 0f;
    }

    private void Update()
    {
        TimeAlive += Time.deltaTime;
        if (TimeAlive > 1)
        {
            Destroy(gameObject);
        }
    }
}