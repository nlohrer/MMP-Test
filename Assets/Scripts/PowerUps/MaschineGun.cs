using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaschineGun : PowerUp
{

    public override void Affect(Player player)
    {
        player.MakeFire(Duration);         
    }    
}
