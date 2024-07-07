using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public float Duration;
    public float Lifetime = 10;

    private float CreationTime;

    public PowerUpController powerUpController;

    public virtual void Start()
    {
        CreationTime = Time.time;
    }

    public virtual void Update()
    {
        if (Time.time - CreationTime >= Lifetime)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();
            Affect(player);
            Destroy(gameObject);
        }
    }

    public abstract void Affect(Player player);
}