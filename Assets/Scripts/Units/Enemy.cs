using UnityEngine;

public abstract class Enemy: MonoBehaviour
{
    public int HP;
    public float Speed;
    public float AttackCooldown;
    public float AttackRange;
    public Vector2 Movement;

    public GameManager gameManager; 

    protected float TimeSinceLastAttack;
    protected Rigidbody2D Rb;
    protected Player Player;
    protected Animator EnemyAnimator;
    protected SpriteRenderer Renderer;

    public virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.velocity = Movement * Speed;
        Player = FindFirstObjectByType<Player>();
        EnemyAnimator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
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
            GameManager.EnemiesKilled++;
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
            return;
        }
        Movement = Vector2.Reflect(Movement, other.GetContact(0).normal);
        Rb.velocity = Movement * Speed;
    }

    protected virtual void AttemptAttack()
    {
        if (Vector2.Distance(Player.GetComponent<Rigidbody2D>().position, Rb.position) < AttackRange) // Range check
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