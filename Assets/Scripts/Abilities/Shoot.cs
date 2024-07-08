using UnityEngine;

public class Shoot : Ability
{
    public Projectile Projectile;

    public bool CanShoot = true; // on of wichtig für abilities

    private GunFlipper Flipper; 
    private GameObject Gun;
    private Camera Camera;

    protected override void Start() // setup für variablen
    {
        base.Start();
        Gun = GameObject.FindGameObjectWithTag("Gun");
        Flipper = Gun.GetComponent<GunFlipper>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public override void Use() // same wie in ability
    {
        if (CanUse())
        {
            if (CanShoot) 
            {
                InternalUse();
                TimeAbilityWasLastUsed = Time.time;
                SetReady(false);
            }
        }
    }

    public override bool CheckForCommand() // wird linke maustaste gedrückt?
    {
        return Input.GetMouseButtonDown(0);
    }

    public override void SetReady(bool ready) // ability ready?
    {
        Flipper.SetLoad(ready);
    }

    protected override void InternalUse() // schusslogik
    {
        Vector2 playerPosition = Player.GetComponent<Rigidbody2D>().position; // spieler position
        var gunDistanceVector = (Vector2)Gun.transform.position - playerPosition; // gun abstand

        Vector2 direction = Camera.ScreenToWorldPoint(Input.mousePosition) - Player.transform.position; // richtung bestimmen anhand mauszeiger
        float angle = Vector2.SignedAngle(Vector2.left, direction); // schussrichtung setzen
        Projectile.Movement = direction.normalized; // movement setzen
        Instantiate(Projectile, playerPosition + gunDistanceVector * 1.8f, Quaternion.Euler(0, 0, angle)); // projectile mit richtung erstellen
    }

    public void ShootExternal() { // von außen schießen, für z.b. MaschineGun Powerup
        InternalUse();
    }
}