using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SanitySlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;
    float audioValue = 0f;
    //public float duration;
    //public float targetVolume;
   
    SanityManager sanity;

    private void Start()
    {
        sanity = FindObjectOfType<SanityManager>();
        
    }

    void Update()
    {
        slider.value = sanity.GetCurrentSanity();

        audioValue = ((100 - slider.value) / 100);

       
        audioSource.volume = audioValue;

        SetSanitySound(audioValue);
        Debug.Log(audioSource);
        Debug.Log(audioSource.volume);
      
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

    public void SetSanitySound(float sanityLevel)
    {

        

        audioSource.volume = sanityLevel;
    }

}
