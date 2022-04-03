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

    // Update is called once per frame
    void Update()
    {

    }

    private void scream()
    {
        animationSoundPlayer.PlayOneShot(screamSFX, .86f);
    }
}
