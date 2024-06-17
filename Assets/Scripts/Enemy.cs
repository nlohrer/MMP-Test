using UnityEngine;

public abstract class Enemy: MonoBehaviour
{
    public int HP;
    public float Speed;
    public float AttackCooldown;
    public Vector2 Movement = new(1f, 1f);

    protected float TimeSinceLastAttack;
    protected Rigidbody2D Rb;

    public virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.velocity = Movement * Speed;
    }

    public virtual void Update()
    {
        AttemptAttack();
        TimeSinceLastAttack += Time.deltaTime;
    }

    public void GetHit(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<Player>() is not null)
        {
            Player player = other.collider.GetComponent<Player>();
            Destroy(gameObject);
            player.GetHit(1);
        }
        Movement = Vector2.Reflect(Movement, other.GetContact(0).normal);
        Rb.velocity = Movement * Speed;
    }

    protected virtual void AttemptAttack()
    {
        if (true) // Range check
        {
            if (TimeSinceLastAttack >= AttackCooldown)
            {
                MakeAttack();

                TimeSinceLastAttack = 0f;
            }
        }
    }

    protected abstract void MakeAttack();
}