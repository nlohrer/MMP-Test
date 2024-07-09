using UnityEngine;

public class FearfulEnemy : Enemy
{
    public GameObject Projectile;

    // Start is called before the first frame update
    public override void Start()
    {
        //HP = 1;
        //Speed = 3;
        //AttackCooldown = 2f;
        //TimeSinceLastAttack = 1f;
        base.Start();
    }

    private float fastSpeed;
    private float slowSpeed;

    //TODO: updaten
    protected override void MakeAttack()
    {
        EnemyAnimator.SetTrigger("OctopusToDash");
        Movement = (-1f) * ((Vector2)Player.transform.position - (Vector2)transform.position).normalized;
        Instantiate(Projectile, Rb.position, Quaternion.identity);
    }

    public override void Update()
    {
        base.Update();
        float zRotation = Mathf.Rad2Deg * Mathf.Atan2(Movement.y, Movement.x);
        if (Movement != Vector2.zero)
        {
            transform.rotation = Quaternion.Euler(0, 0, zRotation);
        }

        if (zRotation >= 90 || zRotation <= -90)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
        }

        var stateInfo = EnemyAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Octopus_slow"))
        {
            Rb.velocity = Movement * Speed * 0.6f;
        }
        if (stateInfo.IsName("Octopus_fast"))
        {
            Rb.velocity = Movement * Speed;
        }
        if (stateInfo.IsName("Octopus_dash"))
        {
            Rb.velocity = Movement * Speed * 1.5f;
        }

    }

}
