using UnityEngine;

public class PlayerProjectile : Projectile
{
    private Vector2 Direction;

    public override void Start()
    {
        base.Start();
        var player = GameObject.FindGameObjectWithTag("Player");
        var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Direction = camera.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        Rb.velocity = Direction.normalized * Speed;
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

    //private void FixedUpdate()
    //{
    //    Rb.AddForce(Direction.normalized * 1.3f, ForceMode2D.Impulse);
    //}
}