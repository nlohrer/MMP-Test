using UnityEngine;

public class StandardEnemy : Enemy
{
    public GameObject Projectile;

    public override void Start()
    {
        base.Start();
    }

    protected override void MakeAttack() // bullet spawnen
    {
        Instantiate(Projectile, Rb.position, Quaternion.identity);
    }
}
