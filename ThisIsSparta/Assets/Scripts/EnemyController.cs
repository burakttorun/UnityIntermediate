using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    PlayerController playerController;
    NavMeshAgent agent;
    Animator bossAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        bossAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isReachBoss)
        {
            agent.SetDestination(playerController.transform.position);
            bossAnim.SetInteger("WeaponType_int", 10);
        }

    }
}
