using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int Damage;
    public float Speed;
    public Vector2 Movement = new(1f, 1f);

    private float TimeSinceSpawned = 0f;
    protected Rigidbody2D Rb;

    public virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.velocity = Movement * Speed;
    }

    private void Update()
    {
        TimeSinceSpawned += Time.deltaTime;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();
            Destroy(gameObject);
            player.GetHit(Damage);
        }

        if (TimeSinceSpawned >= 1 && other.GetComponent<Enemy>() != null || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        
    }
}
