using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DeathHandler : MonoBehaviour {

    SanityManager sanityManager;
    FirstPersonController player;
    Quaternion targetRotation;
 //   Transform targetRotation;
    public Camera cameraPlayer;
    bool isDead;

    public bool IsDead() {
        return isDead;
    }

    void Start() {
        isDead = false;
        sanityManager = GetComponent<SanityManager>();
        player = FindObjectOfType<FirstPersonController>();
    }

    void Update() {
        if (sanityManager.GetCurrentSanity() < 0 && !isDead|| Input.GetKey(KeyCode.P)) {
            isDead = true;
            targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
        }
        if (isDead)
            DeathEvent();
    }

    private void DeathEvent() {
        player.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
    }
}
