using UnityEngine;

public class DeathBubbles : MonoBehaviour
{
    public AudioClip DeathAudio;

    private float TimeAlive;

    private void Start()
    {
        TimeAlive = 0f;
        SoundManager.Instance.PlayClip(DeathAudio, transform, 0.5f);
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