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
    bool isDead;
    bool shadowSpawned;
    [SerializeField] float speedRotation = 1f;
    [SerializeField] float shadowDistance = 3f;
    [SerializeField] float shadowSpawnTime = 2f;
    [SerializeField] Animator animator;
    Transform startDeathPosition;
    public bool IsDead() {
        return isDead;
    }

    void Start() {
        isDead = false;
        sanityManager = GetComponent<SanityManager>();
        player = FindObjectOfType<FirstPersonController>();
    }

    void Update() {
        if (sanityManager.GetCurrentSanity() < 0 && !isDead || Input.GetKeyDown(KeyCode.P)) {
            isDead = true;
            startDeathPosition = cameraPlayer.transform;
            DeathEvent();
            DeathAnimation();
        }
    }

    private void DeathAnimation() {
        animator.enabled = true;
    }

    private void DeathEvent() {
        player.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public void SpawnShadowEvent() {
        shadowSelf.SetActive(true);
        Instantiate(shadowSelf, (transform.position - transform.forward * shadowDistance), transform.rotation);
    }

}
