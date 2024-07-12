using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int Damage; // damage des projectils
    public float Speed; // geschwindigkeit des Projectils
    public Vector2 Movement = new(1f, 1f); // wird überschrieben

    public float Volume = 0.5f;
    public AudioClip OnShootAudio;

    protected float CreationTime; // wann wurde es gespawned
    protected Rigidbody2D Rb; // rigidbody für movement

    public virtual void Start() // initialisierung
    {
        Rb = GetComponent<Rigidbody2D>();
        CreationTime = Time.time;
        SoundManager.Instance.PlayClip(OnShootAudio, transform, Volume);
    }

    public virtual void OnTriggerEnter2D(Collider2D other) 
    // treffer funktion für enemie projectiles playerprojectile überschreibt
    {
        if (other.GetComponent<Player>() != null) // wenn mit enemy collided
        {
            Player player = other.GetComponent<Player>();
            Destroy(gameObject); // projectil kaputt machen
            player.GetHit(Damage); // player nimmt Damage schaden
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
