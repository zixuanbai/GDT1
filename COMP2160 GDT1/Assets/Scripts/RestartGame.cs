using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartGame : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0); // 默认值为0
        highScoreText.text = "HIGHT SCORE: " + highScore.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}



