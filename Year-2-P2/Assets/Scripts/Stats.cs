using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using Unity.VisualScripting;

public class Stats : MonoBehaviour
{
    // Set Time
    private float timerDuration = 0.0001f * 59f * 60f;

    [SerializeField]
    private bool countUp = true;

    private float timer;

    [SerializeField]
    private TextMeshProUGUI firstHour;
    [SerializeField]
    private TextMeshProUGUI secondHour;
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
        float hours = Mathf.FloorToInt(time / 60);
        float minutes = Mathf.FloorToInt(time % 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{00:00}{1:00}", hours, minutes, seconds);
        firstHour.text = currentTime[0].ToString();
        secondHour.text = currentTime[1].ToString();
        firstMinute.text = currentTime[2].ToString();
        secondMinute.text = currentTime[3].ToString();
        firstSecond.text = currentTime[4].ToString();
        secondSecond.text = currentTime[5].ToString();
    }
}
