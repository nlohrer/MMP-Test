using UnityEngine;

public class Shoot : Ability
{
    public Projectile Projectile;

    private GunFlipper Flipper;
    private GameObject Gun;
    private Camera Camera;

    protected override void Start()
    {
        base.Start();
        Gun = GameObject.FindGameObjectWithTag("Gun");
        Flipper = Gun.GetComponent<GunFlipper>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public override bool CheckForCommand()
    {
        return Input.GetMouseButtonDown(0);
    }

    public override void SetReady(bool ready)
    {
        Flipper.SetLoad(ready);
    }

    protected override void InternalUse()
    {
        Vector2 playerPosition = Player.GetComponent<Rigidbody2D>().position;
        var gunDistanceVector = (Vector2)Gun.transform.position - playerPosition;

        Vector2 direction = Camera.ScreenToWorldPoint(Input.mousePosition) - Player.transform.position;
        float angle = Vector2.SignedAngle(Vector2.left, direction);
        Projectile.Movement = direction.normalized;
        Instantiate(Projectile, playerPosition + gunDistanceVector * 1.8f, Quaternion.Euler(0, 0, angle));
    }

    public void ShootExternal() {
        InternalUse();
    }
}