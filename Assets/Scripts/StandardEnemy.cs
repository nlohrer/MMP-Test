using UnityEngine;

public class StandardEnemy : Enemy
{
    public GameObject Projectile;

    // Start is called before the first frame update
    public override void Start()
    {
        HP = 1;
        Speed = 3;
        AttackCooldown = 2f;
        TimeSinceLastAttack = 1f;
        base.Start();
    }

    protected override void MakeAttack()
    {
        Instantiate(Projectile, Rb.position, Quaternion.identity);
    }
}
