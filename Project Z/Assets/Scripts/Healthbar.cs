using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setMaxHealth(int maxhealth){
        slider.maxValue = maxhealth;
        slider.value = maxhealth;

        fill.color = gradient.Evaluate(1f);
    }

    public void setHealth(int health){
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
