using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {
    bool turnOnOff = false;
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            turnOnOff = !turnOnOff;
            this.gameObject.transform.GetChild(1).gameObject.SetActive(turnOnOff);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(turnOnOff);
            this.gameObject.transform.GetChild(3).gameObject.SetActive(turnOnOff);
        }
    }
}
