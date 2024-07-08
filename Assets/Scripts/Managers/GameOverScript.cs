using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI ScoreText; // text einfügen

    void Start() // beim scenen start den berechneten score als Text einfügen
    {
        ScoreText.text = $"Your score is:\n{GameManager.Score}";
    }
}
