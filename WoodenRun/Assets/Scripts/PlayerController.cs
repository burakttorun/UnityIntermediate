using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10f;
    private float rangeX = 2.5f;
    private float rotationSpeed = 100f;
    private float horizontalInput;
    private float translateAxisY = 0.5f;
    Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        TransformBound();
    }

    void Movement()
    {
       // if (Input.touchCount > 0)
        {
           // touch = Input.GetTouch(0);

           // if (touch.phase == TouchPhase.Moved)
            {
              //  transform.Translate(transform.rotation.x, transform.rotation.y + touch.deltaPosition.x * rotationSpeed, transform.rotation.z);
                //This is where we get player input
                horizontalInput = Input.GetAxis("Horizontal");
                //We turn the player.
                transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizontalInput);
            }

        }
        playerAnim.SetBool("Static_b", true);
        playerAnim.SetFloat("Speed_f", 1f);
        //Move to player forward.     
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y + (translateAxisY*GameManager.collectedStick)  + transform.position.z);

    }

    void TransformBound()
    {
        if (transform.position.x < -rangeX)
        {
            transform.position = new Vector3(-rangeX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > rangeX)
        {
            transform.position = new Vector3(rangeX, transform.position.y, transform.position.z);
        }
    }

    
}
