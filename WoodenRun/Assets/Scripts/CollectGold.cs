using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGold : ItemController
{
    private float rotateSpeed = 1;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.gold += 10;

            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
