using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] Hearts;
    //public Player Player;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void RemoveHeart(int HP)
    {
        if (HP < 3)
        {
            Hearts[2].enabled = false;
        }
        if (HP < 2)
        {
            Hearts[1].enabled = false;
        }
        if (HP < 1)
        {
            Hearts[0].enabled = false;
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Player.HP < 3)
    //    {
    //        Hearts[2].enabled = false;
    //    }
    //    if (Player.HP < 2)
    //    {
    //        Hearts[1].enabled = false;
    //    }
    //    if (Player.HP < 1) {
    //        Hearts[0].enabled = false;
    //    }
    //}
}
