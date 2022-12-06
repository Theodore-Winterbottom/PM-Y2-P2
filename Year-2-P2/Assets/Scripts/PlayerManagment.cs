using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagment : MonoBehaviour
{
    public static int helth1 = 100;
    public Slider healthBar;
    public static bool gameOvrer;
    // Start is called before the first frame update
    void Start()
    {
        gameOvrer = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = helth1;

        if(helth1 < 0)
        {
            gameOvrer = true;
        }
    }
}
