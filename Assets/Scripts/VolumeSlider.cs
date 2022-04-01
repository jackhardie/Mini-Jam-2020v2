using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    AudioSource gameBackground;
    Slider slider;
    void Start() {
        slider = GetComponent<Slider>();
        gameBackground = FindObjectOfType<AudioSource>();
        slider.value = gameBackground.volume;
    }

    void Update() {
        gameBackground.volume = slider.value;
    }
}
