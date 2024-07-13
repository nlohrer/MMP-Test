using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public GameObject Gun;
    bool Enabled = true;

    void Update()
    {
        if (!Enabled)
        {
            return;
        }
        var mousePosition = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        var gunVector = mousePosition - transform.position;
        float zRotation = Mathf.Rad2Deg * Mathf.Atan2(gunVector.y, gunVector.x);
        transform.rotation = Quaternion.Euler(0, 0, zRotation);

        GunFlipper flipper = Gun.GetComponent<GunFlipper>();
        if (zRotation >= 90 || zRotation <= -90)
        {
            flipper.FlipGun(true);
        } else
        {
            flipper.FlipGun(false);
        }
    }
}
