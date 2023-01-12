using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DeathCounter : MonoBehaviour
{


    public Text DeathCount;
    public void SetText(int text)
    {
        //string deathsS = deaths.ToString();
        //DeathCount.text = deathsS;
    }


























    /*[Header("Player K/D")]

    [Space()]

    // Gives the number of deaths
    public int deathCount = 0;
    // Gives the number of kills
    public int killCount = 0;

    [Space()]

    [Tooltip("Place DeathCountText here")] [SerializeField]
    private TextMeshProUGUI deathCountText;
    [Tooltip("Place KillCountText here")] [SerializeField]
    private TextMeshProUGUI killCountText;

    void Start()
    {
        UpdateDeathCountText();
        UpdateKillCountText();
    }
    // Adds player kills and deaths
    public void AddKD()
    {
        // Adds for every kill
        killCount++;
        // Adds for every death
        deathCount++;
        UpdateDeathCountText();
        UpdateKillCountText();
    }

    void UpdateDeathCountText()
    {
        deathCountText.text = "Deaths: " + deathCount;
    }
    
    void UpdateKillCountText()
    {
        killCountText.text = "Kills: " + killCount;
    }*/
}
