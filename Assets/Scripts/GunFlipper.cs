using UnityEngine;

public class GunFlipper : MonoBehaviour
{
    public Sprite LoadedGunSprite;
    public Sprite UnloadedGunSprite;

    private SpriteRenderer SR;

    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    public void FlipGun(bool flip)
    {
        gameObject.GetComponent<SpriteRenderer>().flipY = flip;
    }

    public void SetLoad(bool loaded)
    {
        if (loaded)
        {
            SR.sprite = LoadedGunSprite;
        }
        else
        {
            SR.sprite = UnloadedGunSprite;
        }
    }
}
