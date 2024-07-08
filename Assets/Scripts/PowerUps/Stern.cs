using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stern : PowerUp // Stern PowerUp
{
    
      public override void Affect(Player player) // an Player übergeben für Coroutine
    {
        player.MakePowerUp(Duration, 0); // 0 for stern

    }

}
