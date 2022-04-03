using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterHitAudio : MonoBehaviour
{
    public AudioSource animationSoundPlayer;
    public AudioClip attackSFX;

    // Start is called before the first frame update
    void Start()
    {
        //animationSoundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void attack()
    {
        animationSoundPlayer.PlayOneShot(attackSFX, .96f);
    }
}
