using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine.ProBuilder.MeshOperations;

public class Stats : MonoBehaviour
{
    // Death count text
    [SerializeField]
    private TextMeshProUGUI deathCountText;

    // Kill count text
    [SerializeField]
    private TextMeshProUGUI killCountText;
    private TextMeshProUGUI bossKillCountText;

    // Death count variable
    private int deathCount;

    // Kill count variable
    [Range(0f, 100f)]
    [Tooltip("The current health of the object")][SerializeField] private int enemyKillCount;

    // Set Time
    private float timerDuration = 0.0001f * 60f;

    [SerializeField]
    private bool countUp = true;

    private float timer;

    public float hourCount;
    public float minCount;
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
            enemyKillCount++;
            killCountText.text = "Kills: " + enemyKillCount;
        }
    }



    
}
