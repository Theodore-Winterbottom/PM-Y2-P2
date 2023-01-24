using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using Unity.VisualScripting;

public class Stats : MonoBehaviour
{
    public HealthScript healthScript;

    // Set Time
    private float timerDuration = 0.0001f * 60f;

    [SerializeField]
    private bool countUp = true;

    private float timer;

    public float hourCount;
    public float minCount;
    public float secCount;

    [SerializeField]
    private TextMeshProUGUI separator1;
    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI separator2;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }
    // Update is called once per frame
    void Update()
    {
        //Adds time
        if (countUp && timer > 0)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        }

        if (minCount >= 60)
        {

            countUp = false;
        }
    }
    private void ResetTimer()
    {
        // If timer is equal to 0, count up
        if(countUp)
        {
            timer = timerDuration;
        }
        else
        {
            timer = 0;
        }
    }
    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        secCount = seconds;
        minCount = minutes;
        string currentTime = string.Format("{00:00}{01:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    
    // Kill count text
    [SerializeField]
    private TextMeshProUGUI killCountText;

    private int killCount;

    // Method to add kills
    public void EnemyKilled()
    {
        
        // Adds kills to player kill count
        killCount++;
        killCountText.text = "Kills: " + killCount;
    }



    // Death count text
    [SerializeField]
    private TextMeshProUGUI deathCountText;

    private int deathCount;

    // Method to add deaths
    public void PlayerKilled()
    {
        // Add deaths to player death count
        deathCount++;
        deathCountText.text = "Deaths: " + deathCount;
    }


    /*using UnityEngine.UI;
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
}*/




}
