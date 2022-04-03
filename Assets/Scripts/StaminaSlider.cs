using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSlider : MonoBehaviour {
    [SerializeField] Image staminaBar;
    Stamina stamina;
    private void Start() {
        stamina = FindObjectOfType<Stamina>();
        staminaBar.fillAmount = stamina.GetCurrentStamina() / 100f;
    }

    void Update() {
        staminaBar.fillAmount = stamina.GetCurrentStamina() / 100f;
    }
}
