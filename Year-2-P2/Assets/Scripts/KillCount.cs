using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillCount : MonoBehaviour
{
    public static KillCount instance;

    [SerializeField]
    private TextMeshProUGUI killCountText;

    private int killCount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        UpdateKillCountText();
    }

    public void EnemyKilled()
    {
        killCount++;
        UpdateKillCountText();
    }

    private void UpdateKillCountText()
    {
        killCountText.text = "Kills: " + killCount;
    }
}

