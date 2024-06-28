using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed = 8f;
    public int HP = 3;
    public float AttackCooldown = 1f;
    public float InvulOnHitDuration = 0.5f;
    public Vector2 movement;
    public GameObject Projectile;
    public GameObject Gun;
    public bool InvulMode = false;
    public Ability[] Abilities;
    public bool CanMoveManually = true;

    private Rigidbody2D Rb2d;
    private SpriteRenderer Renderer;
    private static readonly float InverseSqrt = 1f / Mathf.Sqrt(2f);
    private float LastTimeGotHit = 0f;

    void Start()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (HP <= 0) return;

        if (CanMoveManually)
        {
            SetMovement(); 
        }

        foreach (var ability in Abilities)
        {
            if (ability.CanUse())
            {
                ability.SetReady(true);
                if (ability.CheckForCommand())
                {
                    ability.Use();
                }
            }

        }
    }

    private void SetMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == 1)
        {
            Renderer.flipX = true;

        }
        else if (horizontalInput == -1)
        {
            Renderer.flipX = false;
        }
        if (horizontalInput != 0 && verticalInput != 0)
        {
            horizontalInput *= InverseSqrt;
            verticalInput *= InverseSqrt;
        }


        movement = new Vector2(horizontalInput, verticalInput);
    }
    public void GetHit(int damage)
    {
        if (Time.time - LastTimeGotHit < InvulOnHitDuration || InvulMode)
        {
            return;
        }


        // Trigger hit animation
        HP -= damage;
        var uiManager = FindFirstObjectByType<UIManager>();
        uiManager.RemoveHeart(HP);
        if (HP <= 0)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            StartCoroutine(HitAnimation());
        }
        LastTimeGotHit = Time.time;
    }

    private IEnumerator GameOver()
    {
        movement = Vector2.zero;
        Renderer.color = Color.red;
        // death animation
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
        // GameOverScreen
    }

    private IEnumerator HitAnimation()
    {
        Renderer.color = Color.red;
        yield return new WaitForSeconds(InvulOnHitDuration);
        Renderer.color = Color.white;
    }

    private void FixedUpdate()
    {
        Rb2d.velocity = movement * Speed;
    }
}
