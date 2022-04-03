using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip keySFX;
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
            audioSource.PlayOneShot(keySFX, .55f);
        }
    }



}
