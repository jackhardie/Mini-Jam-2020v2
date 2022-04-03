using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour {


    public bool turnOnOff = false;
    [SerializeField] GameObject spotLightCookieOne;
    [SerializeField] GameObject spotLightCookieTwo;
    [SerializeField] GameObject spotLightCookieThree;
    [SerializeField] float rangeDecrease = 0.1f;
    [SerializeField] float batteryDecrease = 1f;
    [SerializeField] float spotAnglerangeDecrease = 0.2f;
    [SerializeField] float intensityrangeDecrease = 0.01f;

    [SerializeField]
    AudioSource flashlightSFX;
    [SerializeField]
    SceneHandler sceneHandler;
    
    public float batteryLevel = 100f;
    [SerializeField]
    Image flashlightBar;

    private void OnEnable()
    {
        flashlightSFX.enabled = true;
    }
    private void OnDisable()
    {
        flashlightSFX.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            turnOnOff = !turnOnOff;
            spotLightCookieOne.SetActive(turnOnOff);
            spotLightCookieTwo.SetActive(turnOnOff);
            spotLightCookieThree.SetActive(turnOnOff);

        }
        if (turnOnOff) {
            DecreaseLight(spotLightCookieOne);
            DecreaseLight(spotLightCookieTwo);
            DecreaseLight(spotLightCookieThree);
            DrainBattery();
        }

        if (batteryLevel <= 0) ForceOffFlashlight();

        flashlightBar.fillAmount = batteryLevel / 100;
    }

    private void DecreaseLight(GameObject spotLightCookie) {
        spotLightCookie.GetComponent<Light>().range -= rangeDecrease * Time.deltaTime;
        spotLightCookie.GetComponent<Light>().spotAngle -= spotAnglerangeDecrease * Time.deltaTime;
        spotLightCookie.GetComponent<Light>().intensity -= intensityrangeDecrease * Time.deltaTime;
    }

    private void DrainBattery()
    {
        if (batteryLevel > 0 && !sceneHandler.menuOnOff) batteryLevel -= batteryDecrease * Time.deltaTime;

    }


    public void ForceOffFlashlight()
    {
        turnOnOff = false;
        spotLightCookieOne.SetActive(turnOnOff);
        spotLightCookieTwo.SetActive(turnOnOff);
        spotLightCookieThree.SetActive(turnOnOff);
        this.enabled = false;
        
    }
}
