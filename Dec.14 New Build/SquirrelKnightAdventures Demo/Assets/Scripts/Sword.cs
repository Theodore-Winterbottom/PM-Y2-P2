using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Animator anim;
    public EnemyAi enemyAi;
    public Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(other.tag == "Enemy" && anim.GetBool("attacking"))
        {
            enemyAi.TakeDamage(25);
        }

        if (other.tag == "Boss" && anim.GetBool("attacking"))
        {
            boss.TakeDamage(20);
        }
    }

}
