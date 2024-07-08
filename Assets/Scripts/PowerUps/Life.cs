using UnityEngine;

public class Life : PowerUp // implementation Herz powerup
{
    public override void Affect(Player player) // heil player um 1 
    {
        player.GetHealed(1);
    }
}