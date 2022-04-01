using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {

    public bool hasTheKey;
    public void ResetKeyOwning() {
        hasTheKey = true;
    }

    public bool GetHasTheKey() {
        return hasTheKey;
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.Q) && other.tag == "Key") {
            hasTheKey = true;
            Destroy(other.gameObject);
            //add sound effect when taking key
        }
    }



}
