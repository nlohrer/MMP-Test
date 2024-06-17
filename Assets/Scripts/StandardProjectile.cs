using UnityEngine;

public class StandardProjectile : Projectile
{

    public override void Start()
    {
        Damage = 1;
        Speed = 5f;
        base.Start();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - transform.position;
        Rb.velocity = direction.normalized * Speed;
    }

}
