using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10f;
    private float rotationSpeed = 75f;
    private float horizontalInput;
    

    // Start is called before the first frame update
    void Start()
    {
        TurnGreen();
        
    }
    private void FixedUpdate()
    {
        Movement();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TurnRed();  
        }
    }

    void TurnRed()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    void TurnGreen()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    void Movement()
    {
        //This is where we get player input
        horizontalInput = Input.GetAxis("Horizontal");
        

        //Move to vehicle forward.
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //We turn the vehicle.
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizontalInput);
    }
   
}
