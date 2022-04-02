using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightSFXplay : MonoBehaviour
{
    public AudioClip flashlightSFX;
    public AudioSource audioSource;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(flashlightSFX, .8f);
        }
    }
}
