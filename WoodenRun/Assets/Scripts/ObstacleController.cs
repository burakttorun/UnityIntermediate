using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController :ItemController
{
    public GameObject stickPref;
    private Vector3 spawnPointForStickPref;
    private float spawnDistance = 0.7f;
    private void Start()
    {
        spawnPointForStickPref = new Vector3(transform.position.x, transform.position.y, transform.position.z-spawnDistance);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.isGameActive)
        {
            GameManager.collectedStick--;

            Instantiate(stickPref, spawnPointForStickPref, stickPref.transform.rotation);
            
        }
    }
}
