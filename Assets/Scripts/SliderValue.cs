using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public Slider slider;
    public TMPro.TextMeshProUGUI txt;

    public void Start()
    {
        slider.onValueChanged.AddListener(ValueChanged);   
    }

    private void ValueChanged(float v)
    {
        txt.text = Mathf.RoundToInt(v*100f)+"%";

    }
}
