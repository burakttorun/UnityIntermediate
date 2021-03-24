using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private int cameraStartingPoint = 4;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        offset = new Vector3(0, cameraStartingPoint + (GameManager.collectedStick / 2), -cameraStartingPoint - GameManager.collectedStick);
    }
    private void LateUpdate()
    {
        //Offset the camera behind the player by adding to the player's position.
        //transform.position = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 1);
    }
}
