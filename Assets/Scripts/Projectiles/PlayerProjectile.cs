using UnityEngine;

public class PlayerProjectile : Projectile
{
    private Vector2 Direction;

    public override void Start()
    {
        base.Start();
        Rb.velocity = Movement * Speed;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.GetHit(Damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }
}