using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Stamina : MonoBehaviour {

    public float totalStamina = 100f;
    public float currentStamina;
    public float staminaRunThreshold = 0.14f;
    public float gainStaminaOverTime = 1f;
    public float loseStaminaOverTime = 3f;
    public bool resting;
    OVRPlayerController player;

    private void Awake() {
        currentStamina = totalStamina;
        player = FindObjectOfType<OVRPlayerController>();
    }

    void Update() {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch)!=0f && !resting) { //running
            currentStamina -= loseStaminaOverTime * Time.deltaTime;
            if (currentStamina < staminaRunThreshold) {
                resting = true;
                player.SetRunSpeed(5f);
            }
        }
        else {
            currentStamina += gainStaminaOverTime * Time.deltaTime;
            if (currentStamina > totalStamina) {
                currentStamina = totalStamina; //can't go above the max stamina
                resting = false;
                player.SetRunSpeed(1f);
            }
        }
    }

    public float GetCurrentStamina() {
        return currentStamina;
    }

}
