using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screamAudio : MonoBehaviour
{
    public AudioSource animationSoundPlayer;
    public AudioClip screamSFX;

    // Start is called before the first frame update
    void Start()
    {
        //animationSoundPlayer = GetComponent<AudioSource>();
    }

    private void scream()
    {
        animationSoundPlayer.PlayOneShot(screamSFX, .86f);
    }
}
