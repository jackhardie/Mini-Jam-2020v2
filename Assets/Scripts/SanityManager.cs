using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SanityManager : MonoBehaviour
{
    [SerializeField]
    LightCheck lightChecker;

    public float totalSanity = 100f;
    public float currentSanity;
    public float gainSanityOverTime = 1f;
    public float loseSanityOverTime = 3f;
    FirstPersonController player;
    FlashLight flashlight;

    private void Start()
    {
        currentSanity = totalSanity;
        player = FindObjectOfType<FirstPersonController>();
        flashlight = player.GetComponentInChildren<FlashLight>();
    }

    void Update()
    {
        if(lightChecker.realLightLevel <= 0 && !flashlight.turnOnOff) currentSanity -= loseSanityOverTime * Time.deltaTime;
        else if(lightChecker.realLightLevel > 0)
        {
            currentSanity += gainSanityOverTime * Time.deltaTime;
            if (currentSanity > totalSanity)
            {
                currentSanity = totalSanity;
            }
        }
    }

    public float GetCurrentSanity()
    {
        return currentSanity;
    }

}

