using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaschineGun : PowerUp // implementation MaschineGun powerup (1)
{

    public override void Affect(Player player) // Maschinegun powerup (übergeben an palyer für Coroutine)
    {
        player.MakePowerUp(Duration, 1); // 1 for mashineGun      
    }    
}
