using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private GameManager GameManager;
    [SerializeField]
    private TextMeshProUGUI Text;

    void Update()
    {
        Text.text = GameManager.Score.ToString();
    }
}
