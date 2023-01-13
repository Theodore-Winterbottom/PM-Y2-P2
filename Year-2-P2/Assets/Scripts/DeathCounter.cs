using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class DeathCounter : MonoBehaviour
{
    public int deaths = 0;

    //Reference to your Text, dragged in via the inspector
    public TextMeshProUGUI DeathCountText;

    //private Scene scene;

    public void SetText(string text)
    {
        DeathCountText.text = "Death Count: " + text;
    }

    void Update()
    {
        DeathCountText.text = "Deaths: " + deaths.ToString();
    }

    /*void Start()
    {
        scene = SceneManager.GetActiveScene();
    }*/

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            deaths = deaths + 1;
            //Just update the referenced UI text
            DeathCountText.text = "Death Count: " + deaths;
            Debug.Log("You are dead");
            System.Threading.Thread.Sleep(500);
            SceneManager.LoadScene(0);
        }

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
