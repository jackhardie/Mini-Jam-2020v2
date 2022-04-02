using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DeathHandler : MonoBehaviour {

    SanityManager sanityManager;
    FirstPersonController player;
    Quaternion targetRotation;
    Transform targetPosition;
    public GameObject cameraPlayer;
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
        if (sanityManager.GetCurrentSanity() < 0 && !isDead || Input.GetKeyDown(KeyCode.P)) {
            isDead = true;
            targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
            cameraPlayer.transform.position += new Vector3(0.0f, 0.0f, 2f );
        }
        if (isDead)
            DeathEvent();
    }

    private void DeathEvent() {
        player.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.5f * Time.deltaTime);
        //  cameraPlayer.transform.position = Vector3.Slerp(cameraPlayer.transform.position, targetPosition.position, 0.5f * Time.deltaTime);


    }
}
