using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Stamina : MonoBehaviour {

    public float totalStamina = 100f;
    public float currentStamina;
    public float thresholdStaminaRun = 3f;
    public float gainStaminaOverTime = 1f;
    public float loseStaminaOverTime = 3f;
    public bool resting;
    FirstPersonController player;

    private void Awake() {
        currentStamina = totalStamina;
        player = FindObjectOfType<FirstPersonController>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && !resting) { //running
            currentStamina -= loseStaminaOverTime * Time.deltaTime;
            if (currentStamina < thresholdStaminaRun) {
                resting = true;
                player.SetRunSpeed(4f);
            }
        }
        else {
            currentStamina += gainStaminaOverTime * Time.deltaTime;
            if (currentStamina > totalStamina) {
                currentStamina = totalStamina; //can't go above the max stamina
                resting = false;
                player.SetRunSpeed(10f);
            }
        }
    }

    public float GetCurrentStamina() {
        return currentStamina;
    }

}
