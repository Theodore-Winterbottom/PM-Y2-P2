using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Range(0f, 100f)]
    [Tooltip("The current health of the object")][SerializeField] public int bossKillCount = 10;

    // Boss kill count text
    [SerializeField]
    public TextMeshProUGUI bossKillCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossKilled(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Adds 10 kills to player kill count
            bossKillCount++;
            bossKillCountText.text = "Kills: " + bossKillCount;
        }

    }
}
