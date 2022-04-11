using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class ShadowAI : MonoBehaviour {

    [SerializeField] float chaseRange = 10f;
    [SerializeField] float aggroRange = 5f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float turnSpeed = 5f;
    NavMeshAgent navMeshAgent;
    Transform target;
    DeathHandler deathHandler;
    public float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    public bool hasBeenProvokedOnce = false;

    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<OVRPlayerController>().transform;
        deathHandler = FindObjectOfType<DeathHandler>();
    }

    void Update() {

        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (isProvoked) {
            EngageTarget();
        }

        if (distanceToTarget < aggroRange)
        {
            isProvoked = true;
            hasBeenProvokedOnce = true;
            GetComponent<Animator>().SetBool("idle", false);
        }
        else if(hasBeenProvokedOnce && distanceToTarget > chaseRange)
        {
            print("Despawning");
            Destroy(this.gameObject);
        }
    }

    void EngageTarget() {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance && !deathHandler.GetIsKilledByAttack()) {
            ChaseTarget();
        } else
        if (distanceToTarget <= navMeshAgent.stoppingDistance) {
            AttackTarget();
        }

    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void AttackTarget() {
        deathHandler.SetIsKilledByAttack(true);
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void ChaseTarget() {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("run");

        navMeshAgent.SetDestination(target.position);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
