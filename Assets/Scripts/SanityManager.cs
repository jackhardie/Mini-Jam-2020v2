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

    private void Start()
    {
        currentSanity = totalSanity;
        player = FindObjectOfType<FirstPersonController>();
    }

    void Update()
    {
        if(lightChecker.realLightLevel <= 0) currentSanity -= loseSanityOverTime * Time.deltaTime;
        else
        {
            currentSanity += gainSanityOverTime * Time.deltaTime;
            if (currentSanity > totalSanity)
            {
                currentSanity = totalSanity; //can't go above the max stamina
            }
        }
    }

    public float GetCurrentSanity()
    {
        return currentSanity;
    }

}

