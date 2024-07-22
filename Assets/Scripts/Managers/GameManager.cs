using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ScoreManager.Instance.ResetCurrentScore();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        UpdateScoreUI();
    }

    public void AddPoints(int points)
    {
        ScoreManager.Instance.AddPoints(points);
        UpdateScoreUI();
    }

    public void GameOver()
    {
        if (ScoreManager.Instance.IsNewHighScore)
            scoreText.text = "New high score: " + ScoreManager.Instance.GetCurrentScore();

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateScoreUI() => scoreText.text = "Score: " + ScoreManager.Instance.GetCurrentScore();
}
