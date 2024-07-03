using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] Hearts;

    public void UpdateHearts(int HP)
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            Hearts[i].enabled = i < HP;
        }
    }
}
