using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 12f;
    public int HP = 10;
    public Vector2 movement;
    public GameObject Projectile;

    private Rigidbody2D Rb2d;
    private SpriteRenderer Renderer;
    private static float InverseSqrt = 1f / Mathf.Sqrt(2f);

    void Start()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (HP <= 0) return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput != 0 && verticalInput != 0)
        {
            horizontalInput *= InverseSqrt;
            verticalInput *= InverseSqrt;
        }
        movement = new Vector2(horizontalInput, verticalInput);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot(Input.mousePosition);
        }
    }

    private void Shoot(Vector2 target)
    {
        var projectile = Instantiate(Projectile, Rb2d.position, Quaternion.identity);
    }

    public void GetHit(int damage)
    {
        // Trigger hit animation
        HP -= damage;
        if (HP <= 0)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            StartCoroutine(HitAnimation());
        }
    }

    private IEnumerator GameOver()
    {
        movement = Vector2.zero;
        // death animation
        yield return new WaitForSeconds(2f);
        // GameOverScreen
    }

    private IEnumerator HitAnimation()
    {
        Renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Renderer.color = Color.black;
    }

    private void FixedUpdate()
    {
        Rb2d.velocity = movement * Speed;
    }
}
