using UnityEngine;

public class PlayerProjectile : Projectile
{
    private Vector2 Direction; 

    public override void Start() // richtung und speed festlegen on spawn
    {
        base.Start();
        Rb.velocity = Movement * Speed;
    }

    public override void OnTriggerEnter2D(Collider2D other) // hit Funktion
    {
        if (other.GetComponent<Enemy>() != null) // wenn gegner
        {
            var enemy = other.GetComponent<Enemy>(); 
            enemy.GetHit(Damage); // enemy nimmt schaden
            Destroy(gameObject); // projektil zerstören
        }
        if (other.CompareTag("Wall")) // falls wall zerstören
        {
            Destroy(gameObject);
        }

    }
}