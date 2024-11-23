using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;      
    public TextMeshProUGUI highScoreText;  

    private float score;
    private float highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        UpdateHighScoreUI();
        ResetScore(); 
    }

    private void Update()
    {
        score += GameManager.Instance.gameSpeed * Time.deltaTime;
        UpdateScoreUI();
        
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore); // Save new high score
            UpdateHighScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    private void UpdateHighScoreUI()
    {
        highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore).ToString();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
}
