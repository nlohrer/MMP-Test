using UnityEngine;

public class SniperEnemy : Enemy
{

    public GameObject Projectile;

    public override void Update() // sprite setzen, dass es in richtung spieler schaut
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

    protected override void MakeAttack() // angriffs implementierung
    {
        EnemyAnimator.SetTrigger("attack"); // attack animation starten
        // berechnung der geschätzten zeit des Projectils vom enemy bis zum Spieler
        var FlightDuration = (Player.transform.position - gameObject.transform.position).magnitude / Projectile.GetComponent<SniperProjectile>().Speed; 
        // speichern der spieler richtung und geschwindigkeit
        var PlayerVelocity = Player.GetComponent<Rigidbody2D>().velocity;
        // berechnung der geschätzten Position des spielers zur ankunftszeit
        var EstimatedLocation = Player.transform.position + (Vector3)PlayerVelocity * FlightDuration;
        //bullet spawnen
        SniperProjectile Bullet = Instantiate(Projectile, Rb.position, Quaternion.identity).GetComponent<SniperProjectile>();
        Bullet.Init(EstimatedLocation);
    }
}
