using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SanityManager : MonoBehaviour
{
    [SerializeField]
    LightCheck lightChecker;
    [SerializeField]
    MeshRenderer vhsEffectRenderer;

    public float totalSanity = 100f;
    public float currentSanity;
    public float gainSanityOverTime = 1f;
    public float loseSanityOverTime = 3f;
    public float vhsEffectIntensity;
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
        ApplyFilter();
    }

    public float GetCurrentSanity()
    {
        return currentSanity;
    }

    void ApplyFilter()
    {
        vhsEffectIntensity = 1 - (currentSanity / 100);
        if (vhsEffectIntensity >= 0.7f) vhsEffectIntensity = 0.7f;

        UnityEngine.Color oldColor = vhsEffectRenderer.material.color;
        vhsEffectRenderer.material.color = new UnityEngine.Color(oldColor.r, oldColor.g, oldColor.b, vhsEffectIntensity);
    }
}

