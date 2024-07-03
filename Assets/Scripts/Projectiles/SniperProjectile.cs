using UnityEngine;

public class SniperProjectile : Projectile
{
    private Vector3 TargetLocation;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Vector2 direction = TargetLocation - gameObject.transform.position;
        Rb.velocity = direction.normalized * Speed;
    }

    public void Init(Vector3 EstimatedLocation)
    {
        TargetLocation = EstimatedLocation;
    }
}
