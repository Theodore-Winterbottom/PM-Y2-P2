using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score insatance;
    
    public int score = 0;
    public int highScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        insatance = this;
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "HighScore: " + highScore.ToString();
    }

    public void AddPoints(int points)
    {
        score += 1;
        scoreText.text = "Score: " + score.ToString();
    }
}
