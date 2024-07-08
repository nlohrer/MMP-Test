using UnityEngine;

public abstract class Enemy: MonoBehaviour
{
    public int HP; // enemy leben
    public float Speed; // speed
    public float AttackCooldown; // wie oft angreifen
    public float AttackRange; 
    public Vector2 Movement;

    public GameManager gameManager; 

    protected float TimeSinceLastAttack; // tracking von angriffen
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

    public virtual void Update() // angriff versuchen und tracker updaten
    {
        AttemptAttack();
        TimeSinceLastAttack += Time.deltaTime;
    }

    public void GetHit(int damage) // schaden nehmen 
    {
        HP -= damage;
        if (HP <= 0)
        {
            GameManager.EnemiesKilled++;
            Destroy(gameObject);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D other) // collisions verhalten
    {
        if (other.collider.GetComponent<Player>() is not null) // wenn gegner
        {
            Player player = other.collider.GetComponent<Player>();
            if(player.InvulMode == true)  // falls gegner unzerstörbar (Stern) track enemies killed sonst nicht
            {
                GameManager.EnemiesKilled++;
            }
            Destroy(gameObject); // zerstöre enemie (überschrieben von enemies mit mehr HP als 1)
            player.GetHit(1); // spieler nimmt 1 schaden
            return;
        }
        Movement = Vector2.Reflect(Movement, other.GetContact(0).normal); // alle anderen collisionen bounce implementieren
        Rb.velocity = Movement * Speed;
    }

    protected virtual void AttemptAttack() // versuche anzugreifen
    {
        if (Vector2.Distance(Player.GetComponent<Rigidbody2D>().position, Rb.position) < AttackRange) // Range check
        {
            if (TimeSinceLastAttack >= AttackCooldown) // in range aber atatck on cooldown?
            {
                MakeAttack(); // attack

                TimeSinceLastAttack = 0f; // attack tracker neu setzen
            }
        }
    }

    protected abstract void MakeAttack(); // wird von enemies überschrieben
}