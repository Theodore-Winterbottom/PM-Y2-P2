using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public GameObject player;
    public GameObject fullHealth;
    public GameObject health3HP;
    public GameObject health2HP;
    public GameObject lowHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(Collider other)
    {
        if(other.tag == "FullHealth")
        {
            fullHealth.SetActive(true);
            health3HP.SetActive(false);
            
        }
        else if(other.tag == "Health-3")
        {
            health3HP.SetActive(true);
            fullHealth.SetActive(false);
        }
    }
}
