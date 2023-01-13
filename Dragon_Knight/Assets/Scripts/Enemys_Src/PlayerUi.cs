using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public Gradient barColor;
    public Slider healthBar, comboBar;
    public Image fill, comboFill;

    const float maxValue = 100f;
    float maxSliderValue = 1f, minSliderValue = 0f;

    public void SetHealthBarMaxValue(float maxHealth)
    {
        healthBar.maxValue = maxHealth / maxValue;
        fill.color = barColor.Evaluate(maxHealth / maxValue);
    }

    public void SetCurrentHealthBarValue(float currentHealth)
    {
        healthBar.value = currentHealth / maxValue;
        fill.color = barColor.Evaluate(currentHealth / maxValue);

    }

    public void SetStartComboBarValue()
    {
        comboBar.minValue = minSliderValue;
        comboBar.maxValue = maxSliderValue;
    }
    public void SetComboBarValue(float comboBarValue)
    {
        comboBar.value = comboBarValue / maxValue;
    }
    
}
