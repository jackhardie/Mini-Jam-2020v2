using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {
    bool isDead;
    SanityManager sanityManager;
    void Start() {
        sanityManager = GetComponent<SanityManager>();
    }

    // Update is called once per frame
    void Update() {
        if (sanityManager.GetCurrentSanity() < 0 && !isDead) {
            Debug.Log("Player is dead");
            isDead = true;
            DeathEvent();

        }
    }
    private void DeathEvent() {
        throw new NotImplementedException();
    }
}
