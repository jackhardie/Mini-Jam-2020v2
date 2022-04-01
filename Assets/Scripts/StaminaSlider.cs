using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSlider : MonoBehaviour
{
    public Slider slider;
    Stamina stamina;

    private void Start() {
        stamina = FindObjectOfType<Stamina>();
    }

    void Update()
    {
        slider.value = stamina.GetCurrentStamina();
    }
}
