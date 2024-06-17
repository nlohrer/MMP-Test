using UnityEngine;

public class PlayerProjectile : Projectile
{
    public override void Start()
    {
        Damage = 1;
        Speed = 11f;
        base.Start();

        var player = GameObject.FindGameObjectWithTag("Player");
        var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector2 direction = camera.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        Rb.velocity = direction.normalized * Speed;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            // hit Enemy
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