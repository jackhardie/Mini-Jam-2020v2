using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class ShadowAI : MonoBehaviour {

    [SerializeField] float chaseRange = 5f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float turnSPeed = 5f;
    NavMeshAgent navMeshAgent;
    Transform target;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<FirstPersonController>().transform;
    }

    void Update() {

        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (isProvoked) {
            EngageTarget();
        }

        if (distanceToTarget < chaseRange) {
            isProvoked = true;
            GetComponent<Animator>().SetBool("idle", false);
        }
        else {
            GetComponent<Animator>().SetBool("idle", true);
            isProvoked = false;
        }
    }

    void EngageTarget() {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance) {
            ChaseTarget();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance) {
            AttackTarget();
        }

    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSPeed);
    }

    private void AttackTarget() {
        GetComponent<Animator>().SetBool("attack", true);
        Debug.Log("Player Attacked and killed");
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
