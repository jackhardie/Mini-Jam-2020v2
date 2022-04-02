using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SanitySlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;
    public float duration;
    public float targetVolume;
   
    SanityManager sanity;

    private void Start()
    {
        sanity = FindObjectOfType<SanityManager>();
    }

    void Update()
    {
        slider.value = sanity.GetCurrentSanity();

        audioSource.volume = (100 - slider.value)/100;

        /*if (slider.value < 95)
        {
            targetVolume = .8f;
            StartCoroutine(AudioFade.StartFade(audioSource, duration, targetVolume));
        }

        if (slider.value > 95)
        {
            targetVolume = 0f;
            StartCoroutine(AudioFade.StartFade(audioSource, duration, targetVolume));
        }*/
    }
}
