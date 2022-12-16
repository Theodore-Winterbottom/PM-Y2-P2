using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator anim;
    public EnemyAi enemyAi;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

            anim.SetBool("attacking", true);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            anim.SetBool("attacking", false);

        }

        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemyAi.Death();
        }
    }
}
