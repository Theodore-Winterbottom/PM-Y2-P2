/*using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Respawn : MonoBehaviour
{

    public int deaths;

    //Reference to your Text, dragged in via the inspector
    public TextMeshProUGUI deathCount;

    private Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            deaths = deaths + 1;
            //Just update the referenced UI text
            deathCount.text = "Death Count: " + deaths;
            Debug.Log("You are dead");
            System.Threading.Thread.Sleep(500);
            SceneManager.LoadScene(0);
        }

    }

}*/