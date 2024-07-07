using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stern : PowerUp
{
    
      public override void Affect(Player player)
    {
        player.MakePowerUp(Duration, 0);

    }

}
