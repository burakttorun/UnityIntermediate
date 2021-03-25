using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private int cameraStartingPoint = 4;
    public float transitionDuration = 2f;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        offset = new Vector3(0, cameraStartingPoint + (GameManager.collectedStick / 2), -cameraStartingPoint - GameManager.collectedStick);
    }
    private void LateUpdate()
    { 
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);

            //Offset the camera behind the player by adding to the player's position.
            transform.position = Vector3.Lerp(startingPos, player.transform.position + offset, t);
            yield return 0;
        }

    }
}
