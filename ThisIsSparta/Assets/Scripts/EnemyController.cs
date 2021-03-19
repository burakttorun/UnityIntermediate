using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    PlayerController playerController;
    NavMeshAgent agent;
    Animator bossAnim;
    Rigidbody enemyRb;
    private float distanceWithPlayer=3.1f;
    private float thrust = 10000;
    public int enemyHealth = 400;
    private float time;
    private float timeLimit=3f;
    
    [SerializeField]
    ParticleSystem particleExplosion;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        bossAnim = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (playerController.isReachBoss && !playerController.isGameOver)
        {
            agent.SetDestination(playerController.transform.position);
            if (transform.position.z - playerController.transform.position.z < distanceWithPlayer)
            {
                bossAnim.SetFloat("Speed_f", 0f);
            }
            bossAnim.SetInteger("WeaponType_int", 10);
        }

        if (playerController.playerHealth == 40)
        {
            particleExplosion.Play();
        }

        if (playerController.playerHealth < 0)
        {
           // particleExplosion.Stop();
            agent.SetDestination(transform.position);
            bossAnim.SetInteger("WeaponType_int",0);
            bossAnim.SetInteger("Animation_int", 6);
        }

        if (enemyHealth < 0)
        {
            bossAnim.SetBool("Death_b", true);
            bossAnim.SetInteger("DeathType_int", 1);
            agent.SetDestination(transform.position);
            
        }

        if (enemyHealth < 0)
        {
            time += Time.deltaTime;
            if (time < timeLimit)
            {
                thrust = 500;
            }
            else
                thrust = 0;
            enemyRb.AddForce(Vector3.forward *PlayerController.punchStrength * Time.deltaTime * thrust* playerController.playerPower, ForceMode.Impulse);
            gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFist"))
        {
            enemyHealth -= playerController.playerPower*3;
           
        }

        if (other.CompareTag("Limit"))
        {
            time = 3.5f;
        }
    }
}
