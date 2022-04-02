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
    public GameObject shadowSelf;
    bool isDead;
    [SerializeField] float speedRotation = 1f;
    [SerializeField] float shadowDistance = 3f;
    [SerializeField] float shadowSpawnTime = 2f;
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
            cameraPlayer.transform.position += new Vector3(0.0f, 0.0f, 2f);
            targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
            StartCoroutine("SpawnShadow");
        }
        if (isDead)
            DeathEvent();
    }

    private void DeathEvent() {
        player.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
    }

    IEnumerator SpawnShadow() {

        yield return new WaitForSeconds(shadowSpawnTime);
        shadowSelf.SetActive(true);
        Instantiate(shadowSelf, (transform.position - new Vector3(0f, 0f, shadowDistance)), Quaternion.identity);
    }

}
