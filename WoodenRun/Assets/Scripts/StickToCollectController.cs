using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToCollectController : ItemController

{

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.collectedStick += 1;

            Destroy(gameObject);
        }
    }

}
