using UnityEngine;

public class InkProjectile : Projectile
{

    public override void Start()
    {
        base.Start();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - transform.position;
        Rb.velocity = direction.normalized * Speed;
    }

    private void Update()
    {
        if (Time.time - CreationTime >= 2)
        {
            Destroy(gameObject);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    // treffer funktion für enemie projectiles playerprojectile überschreibt
    {
        if (other.GetComponent<Player>() != null) // wenn mit enemy collided
        {
            Player player = other.GetComponent<Player>();
            Destroy(gameObject); // projectil kaputt machen
            player.SpawnInk();
        }

        float TimeSinceSpawned = Time.time - CreationTime;
        /* destroy projectil wenn es mit einem enemy oder wall colided 
        (erst nach einer sekunde, da projectile in enemies spawnen damit sie nicht direkt kaputt gehen)*/
        if (TimeSinceSpawned >= 1 && other.GetComponent<Enemy>() != null || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }

}
