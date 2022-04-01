using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {
    public bool turnOnOff = false;
    [SerializeField] GameObject spotLightCookieOne;
    [SerializeField] GameObject spotLightCookieTwo;
    [SerializeField] GameObject spotLightCookieThree;
    [SerializeField] float rangeDecrease = 0.1f;
    [SerializeField] float spotAnglerangeDecrease = 1f;
    [SerializeField] float intensityrangeDecrease = 0.01f;

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
        }
    }

    private void DecreaseLight(GameObject spotLightCookie) {
        spotLightCookie.GetComponent<Light>().range -= rangeDecrease * Time.deltaTime;
        spotLightCookie.GetComponent<Light>().spotAngle -= spotAnglerangeDecrease * Time.deltaTime;
        spotLightCookie.GetComponent<Light>().intensity -= intensityrangeDecrease * Time.deltaTime;
    }

    public void ForceOffFlashlight()
    {
        turnOnOff = !turnOnOff;
        spotLightCookieOne.SetActive(turnOnOff);
        spotLightCookieTwo.SetActive(turnOnOff);
        spotLightCookieThree.SetActive(turnOnOff);
    }
}
