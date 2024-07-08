using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public float Duration; // wie lange hält das Powerup
    public float Lifetime = 10; // wie lange bleibt das Powerup auf der Map

    private float CreationTime; // wann wurde das Powerup erstellt

    public PowerUpController powerUpController;

    public virtual void Start() // setze Creation time
    {
        CreationTime = Time.time;
    }

    public virtual void Update() // wenn Lifetime überschritten zerstöre Powerup
    {
        if (Time.time - CreationTime >= Lifetime)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other) // wenn player berührt zerstöre Powerup und aktiviere effekt
    {
        if (other.GetComponent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();
            Affect(player);
            Destroy(gameObject);
        }
    }

    public abstract void Affect(Player player); // aktiviere effekt
}