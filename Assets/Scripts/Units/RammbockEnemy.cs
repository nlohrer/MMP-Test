using System.Collections;
using UnityEngine;

public class RammbockEnemy : Enemy
{
    public AudioClip DashAudio;

    private AudioSource DashAudioSource;

    public override void Update()
    {
        base.Update();

        var StateInfo = EnemyAnimator.GetCurrentAnimatorStateInfo(0);
        if (StateInfo.IsName("Kugelfisch_Idle") || StateInfo.IsName("Kugelfisch_Abblasen"))
        {
            Rb.velocity = Movement * Speed;
            if (Player.transform.position.x < transform.position.x)
            {
                Renderer.flipX = false;
            }
            else
            {
                Renderer.flipX = true;
            }
        }
        if (StateInfo.IsName("Kugelfisch_Aufblasen"))
        {
            Rb.velocity = Vector2.zero;
            if (Player.transform.position.x < transform.position.x)
            {
                Renderer.flipX = false;
            }
            else
            {
                Renderer.flipX = true;
            }
        }
        if (StateInfo.IsName("Kugelfisch_Angriff"))
        {

            Rb.velocity = Movement * (Speed * 2f);
            if (Movement.x < 0)
            {
                Renderer.flipX = false;
            }
            else
            {
                Renderer.flipX = true;
            }
        }
    }

    protected override void MakeAttack()
    {
        EnemyAnimator.SetTrigger("InRange");

        StartCoroutine(BoostEnemySpeed());
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<Player>() is not null)
        {
            Player player = other.collider.GetComponent<Player>();
            if (player.InvulMode)
            {
                GetHit(HP);
            } else
            {
                player.GetHit(1);
            }
        }
        Movement = Vector2.Reflect(Movement, other.GetContact(0).normal);
        Rb.velocity = Movement * Speed;
    }

    public override void GetHit(int damage) // schaden nehmen 
    {
        HP -= damage;
        if (HP <= 0)
        {
            GameManager.EnemiesKilled++;
            Instantiate(DeathBubbles, Rb.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(RenderRed());
        }
    }


    private IEnumerator RenderRed()
    {
        Renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Renderer.color = Color.white;
    }

    private IEnumerator BoostEnemySpeed()
    {
        yield return new WaitForSeconds(1.15f);
        DashAudioSource = SoundManager.Instance.PlayLoopingClip(DashAudio, transform, 2.5f);
        Movement = (Vector2)(Player.transform.position - gameObject.transform.position).normalized;
        yield return new WaitForSeconds(3f);
        EnemyAnimator.SetTrigger("AbblasenStarten");
        yield return new WaitForSeconds(1f);
        Destroy(DashAudioSource.gameObject);
    }
}

