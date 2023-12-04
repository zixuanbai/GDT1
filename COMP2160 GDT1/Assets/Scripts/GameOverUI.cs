using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;




    private void Start()
    {
        gameOverPanel.SetActive(false); 
    }

    public  void ShowGameOver(int score, int highScore)
    {
        scoreText.text = "CURRENT SCORE: " + score.ToString();
        highScoreText.text = "HIGHT SCORE: " + highScore.ToString();
        gameOverPanel.SetActive(true); 
    }

    
}

