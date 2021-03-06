using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DeathHandler : MonoBehaviour {

    SanityManager sanityManager;
    FirstPersonController player;
    public GameObject cameraPlayer;
    public GameObject shadowSelf;
    public AudioSource audiosource;
    public AudioClip attackSFX;

    bool isDead;
    bool finallyDead;
    bool shadowSpawned;
    bool isKilledByAttack;
    [SerializeField] float speedRotation = 1f;
    [SerializeField] float shadowDistance = 3f;
    [SerializeField] float shadowSpawnTime = 2f;
    [SerializeField] Animator animatorDeathBySanity;
    Transform startDeathPosition;
    public bool IsDead() {
        return isDead;
    }
    public bool GetIsKilledByAttack() {
        return isKilledByAttack;
    }
    public void SetIsKilledByAttack(bool isKilled) {
         isKilledByAttack = isKilled;
    }

    void Start() {
        isDead = false;
        sanityManager = GetComponent<SanityManager>();
        player = FindObjectOfType<FirstPersonController>();

    }

    void Update() {
        if (sanityManager.GetCurrentSanity() < 0 && !isDead || Input.GetKeyDown(KeyCode.P)) {
            GetComponent<Animator>().SetBool("deadBySanity", true);
            isDead = true;
            startDeathPosition = cameraPlayer.transform;
            DeathEvent();
            DeathAnimation();
        }

        if (isKilledByAttack && !finallyDead || Input.GetKeyDown(KeyCode.K)) {
            finallyDead = true;
            isDead = true;
            
            DeathAnimation();
            DeathEvent();
            GetComponent<Animator>().SetBool("deadByFrontal", true);
        }

    }

    private void DeathAnimation() {
        animatorDeathBySanity.enabled = true;
        //audiosource.PlayOneShot(attackSFX, .02f);
    }

    private void DeathEvent() {
     
        player.enabled = false;
        //audiosource.PlayOneShot(attackSFX, .2f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public void SpawnShadowEvent() {
        shadowSelf.SetActive(true);
        Instantiate(shadowSelf, (transform.position - transform.forward * shadowDistance), transform.rotation);
        audiosource.PlayOneShot(attackSFX, .8f);

    }

}
