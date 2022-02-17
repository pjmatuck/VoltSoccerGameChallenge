using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargingBarView : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient barGradientColor;
    [SerializeField] Image barFill;

    public event Action OnMaxValue;

    public float Value
    {
        get => slider.normalizedValue;
        set
        {
            slider.value = value;
            barFill.color = barGradientColor.Evaluate(Value);
            if (slider.value >= slider.maxValue)
            {
                OnMaxValue.Invoke();
                slider.value = 0;
            }
        }
    }
}
