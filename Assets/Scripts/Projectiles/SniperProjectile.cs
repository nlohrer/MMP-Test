using UnityEngine;

public class SniperProjectile : Projectile
{
    private Vector3 TargetLocation; //target location speichern
    
    public override void Start() // on spawn richtung festlegen
    {
        base.Start();
        Vector2 direction = TargetLocation - gameObject.transform.position;
        Rb.velocity = direction.normalized * Speed;
    }

    // wird von Sniper aufgerufen und findet for start statt, deswegen wird target Location gespeichert
    public void Init(Vector3 EstimatedLocation) 
    {
        TargetLocation = EstimatedLocation;
    }
}
