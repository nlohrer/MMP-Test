using UnityEngine;

public class BounceModeEnemy: MonoBehaviour
{
    public int HP;
    public float Speed;
    public Vector2 Movement;

    protected Rigidbody2D Rb;

    public virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        float randomAngle = Mathf.Deg2Rad * Random.Range(0, 360);
        Vector2 movement = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle));
        Movement = movement;
        Rb.velocity = Movement * Speed;
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<Player>() is not null)
        {
            Player player = other.collider.GetComponent<Player>();
            Destroy(gameObject);
            player.GetHit(1);
            return;
        }
        Movement = Vector2.Reflect(Movement, other.GetContact(0).normal);
        Rb.velocity = Movement * Speed;
    }
}