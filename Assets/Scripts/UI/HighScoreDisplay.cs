using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start() => 
        highScoreText.text = "High Score: " + ScoreManager.Instance.GetHighScore();
}
