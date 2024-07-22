using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int currentScore;
    private int highScore;
    public bool IsNewHighScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadHighScore() => highScore = PlayerPrefs.GetInt("HighScore", 0);
    
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        IsNewHighScore = false;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            IsNewHighScore = true;
            SaveHighScore();
        }
    }

    public int GetCurrentScore() => currentScore;
    public int GetHighScore() => highScore;
    public void ResetCurrentScore() => currentScore = 0;  
}