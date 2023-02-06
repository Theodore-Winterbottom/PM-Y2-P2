using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine.ProBuilder.MeshOperations;

public class Stats : MonoBehaviour
{
    [Tooltip("Place the HealthScript game object Here")][SerializeField] public Enemy enemy;

    // Death count text
    [SerializeField]
    private TextMeshProUGUI deathCountText;

    // Enemy kill count text
    [SerializeField]
    private TextMeshProUGUI killCountText;

    // Boss kill count text
    [SerializeField]
    private TextMeshProUGUI bossKillCountText;

    // Score count text
    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Kill count variable
    private int enemyKillCount;

    // Score count variable
    private int scoreCount;

    // Boss Kill count variable
    private int bossKillCount;

    // Death count variable
    private int deathCount;

    // Set Time
    private float timerDuration = 0.0001f * 60f;

    [SerializeField]
    private bool countUp = true;

    // Timer variable
    private float timer;

    // Minutes variable
    public float minCount;

    // Seconds variable
    public float secCount;

    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI separator;
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
        //Adds time, count up
        if (countUp && timer > 0)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
        // Stops time at 60 minutes
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

    // Method to add deaths
    public void Playerkilled(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Add deaths to player death count
            deathCount++;
            deathCountText.text = "Deaths: " + deathCount;
        }
    }

    // Method to add kills
    public void EnemyKilled(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Adds kills to player kill count
            enemyKillCount = enemyKillCount + 1;
            killCountText.text = "Kills: " + enemyKillCount;
            ScoreText(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            // Adds 10 kills to player kill count
            bossKillCount = bossKillCount + 10;
            bossKillCountText.text = "Kills: " + bossKillCount;
        }
    }

    // Method to add score
    public void ScoreText(GameObject other)
    {
        if (other.gameObject.tag == "Boss")
        {
            scoreCount = scoreCount + 10;
            scoreText.text = "Score: " + scoreCount;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            scoreCount = scoreCount + 1;
            scoreText.text = "Score: " + scoreCount;
        }
    }
}
