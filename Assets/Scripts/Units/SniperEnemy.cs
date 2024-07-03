using UnityEngine;

public class SniperEnemy : Enemy
{

    public GameObject Projectile;

    public override void Update()
    {
        base.Update();
        if (Player.transform.position.x < transform.position.x)
        {
            Renderer.flipX = false;
        } else
        {
            Renderer.flipX = true;
        }
    }

    protected override void MakeAttack()
    {
        EnemyAnimator.SetTrigger("attack");
        //EnemyAnimator.ResetTrigger("attack");
        var FlightDuration = (Player.transform.position - gameObject.transform.position).magnitude / Projectile.GetComponent<SniperProjectile>().Speed; //estimated time from enemy to player
        var PlayerVelocity = Player.GetComponent<Rigidbody2D>().velocity;
        var EstimatedLocation = Player.transform.position + (Vector3)PlayerVelocity * FlightDuration; // estimated position at arrival of bullet
        SniperProjectile Bullet = Instantiate(Projectile, Rb.position, Quaternion.identity).GetComponent<SniperProjectile>();
        Bullet.Init(EstimatedLocation);
    }
}
