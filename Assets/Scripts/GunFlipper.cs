using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlipper : MonoBehaviour
{
    public void FlipGun(bool flip)
    {
        gameObject.GetComponent<SpriteRenderer>().flipY = flip;
    }
}
