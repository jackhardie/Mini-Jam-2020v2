using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CutsceneFootsteps : MonoBehaviour
{
    AudioSource animationSoundPlayer;
    public AudioClip footstepsSFX;

    // Start is called before the first frame update
    void Start()
    {
        animationSoundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayerFootstepSound()
    {
        animationSoundPlayer.PlayOneShot(footstepsSFX, .65f);
    }
}
