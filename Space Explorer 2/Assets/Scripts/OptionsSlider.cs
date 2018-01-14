using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsSlider : MonoBehaviour
{
    public Slider slider;
    public AudioMixer master;
    float sliderValue;

    void OnEnable()
    {
        master.GetFloat("musicVolume", out sliderValue);
        slider.value = sliderValue;

    }
}

