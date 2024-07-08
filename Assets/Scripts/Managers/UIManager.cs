using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] Hearts; // anzeige der Herzen

    public void UpdateHearts(int HP) // bei HP verlust anzeige updaten
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            Hearts[i].enabled = i < HP;
        }
    }
}
