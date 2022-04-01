using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanitySlider : MonoBehaviour
{
    public Slider slider;
    SanityManager sanity;

    private void Start()
    {
        sanity = FindObjectOfType<SanityManager>();
    }

    void Update()
    {
        slider.value = sanity.GetCurrentSanity();
    }
}
