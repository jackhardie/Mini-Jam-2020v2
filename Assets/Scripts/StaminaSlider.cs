using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSlider : MonoBehaviour {
    public Slider slider;
    Stamina stamina;
    public GameObject backGroundColor;
    public GameObject fillAreaColor;
    private void Start() {
        stamina = FindObjectOfType<Stamina>();
        slider.value = stamina.GetCurrentStamina();
    }

    void Update() {
        slider.value = stamina.GetCurrentStamina();
        if (slider.value < 4f) {
            backGroundColor.GetComponent<Image>().color = Color.red;
            fillAreaColor.GetComponent<Image>().color = Color.red;
        }
        else if (slider.value < 6f) {
            backGroundColor.GetComponent<Image>().color = Color.yellow;
            fillAreaColor.GetComponent<Image>().color = Color.yellow;
        }
        else if (slider.value < 8f) {
            backGroundColor.GetComponent<Image>().color = Color.green;
            fillAreaColor.GetComponent<Image>().color = Color.green;
        }
    }
}
