using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
	public Image fill;

    private void Update()
    {
        
    }
    public void SetMaxHealth(float health)
	{
		//slider.maxValue -= health;
		slider.value -= health;
	}

	/*public void SetHealth(int health)
	{
		slider.value = health;
	}*/
   
}
