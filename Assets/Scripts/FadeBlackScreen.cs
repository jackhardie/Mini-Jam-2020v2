using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlackScreen : MonoBehaviour
{
    DeathHandler deathHandler;
    public Animator animator;
    void Start()
    {
        deathHandler = FindObjectOfType<DeathHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathHandler.IsDead()) {
            animator.enabled = true;
        }
    }
}
