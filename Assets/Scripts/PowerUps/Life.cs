using UnityEngine;

public class Life : PowerUp
{
    public override void Affect(Player player)
    {
        player.GetHealed(1);
    }
}