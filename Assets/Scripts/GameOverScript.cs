using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = $"Your score is:\n{GameManager.Score}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
