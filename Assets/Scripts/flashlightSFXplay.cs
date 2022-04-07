using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightSFXplay : MonoBehaviour
{
    public AudioClip flashlightSFX;
    public AudioSource audioSource;


    // VR CHANGE
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            audioSource.PlayOneShot(flashlightSFX, .8f);
        }
    }
}
