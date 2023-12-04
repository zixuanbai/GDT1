using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI highScoreText;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0); // 从PlayerPrefs中获取最高分，默认值为0
        highScoreText.text = "HIGHT SCORE: " + highScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetHighScoreText(TextMeshProUGUI text)
    {
        highScoreText = text;
    }
}
