using UnityEngine;

public class PlayerProjectile : Projectile
{
    private Vector2 Direction;

    public override void Start()
    {
        base.Start();
        //var player = GameObject.FindGameObjectWithTag("Player");
        //var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //Direction = camera.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        //float angle = Vector2.SignedAngle(Vector2.left, Direction);
        //Rb.rotation = angle;
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

    //private void FixedUpdate()
    //{
    //    Rb.AddForce(Direction.normalized * 1.3f, ForceMode2D.Impulse);
    //}
}