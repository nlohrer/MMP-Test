using UnityEngine;

public class StandardProjectile : Projectile
{

    public override void Start() // on spawn velocity festlegen 
    {
        base.Start();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - transform.position;
        Rb.velocity = direction.normalized * Speed;
    }

}
