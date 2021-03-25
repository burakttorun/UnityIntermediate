using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5f;
    private float rangeX = 2.5f;
    private float rotationSpeed = 100f;
 
    private float horizontalInput;
    private float translateAxisY = 0.5f;
    private float basePointAxisY = 1.5f;

    public bool playerIsJumped = false;
    float transitionDuration = 2;


    Animator playerAnim;
    GameManager gameManager;
   
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip collectSound;
    public AudioClip growthSound;
    public AudioClip boomSound;


    private bool boomSoundIsPlaying = false;

   public ParticleSystem particleDeath;
    [SerializeField]
    ParticleSystem[] particleFireworks;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.isGameActive)
        {
            if (!playerIsJumped)
            {
                Movement();
            }
           
            TransformBound();
        }
         
    }

    private void PlayerDeath()
    {
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);   
        particleDeath.Play();
        if (!boomSoundIsPlaying)
        {
            boomSoundIsPlaying = true;
            playerAudio.PlayOneShot(boomSound);
        }
             
        StartCoroutine(MakeInvisible());
        gameManager.GameOver();
    }
    IEnumerator MakeInvisible()
    {
        yield return new WaitForSeconds(4.0f);
        gameObject.SetActive(false);
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

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StickToCollect"))
        {
            PlayerPosY();
        }

        if (other.CompareTag("FinishLine"))
        {
            StartCoroutine(FreeFall());
            foreach (var item in particleFireworks)
            {
                item.Play(); //Fireworks effect starts when teammate catches the ball.
            }
        } 

        if (other.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(hurtSound,1f);

            if (GameManager.collectedStick <= 0)
            {
                PlayerDeath();
            }
        }
        if (other.CompareTag("Gold"))
        {
            playerAudio.PlayOneShot(collectSound, 0.1f);
        }
        if (other.CompareTag("StickToCollect"))
        {
            playerAudio.PlayOneShot(growthSound,0.5f);
        }


    }

    IEnumerator FreeFall()
    {
        playerIsJumped = true;
        speed = 0;
        playerAnim.SetBool("Jump_b", true);
        playerAudio.PlayOneShot(jumpSound, 1f);
        StartCoroutine(Transition());
        yield return new WaitForSeconds(0.5f);
        playerAnim.SetBool("Jump_b", false);
        playerAnim.SetFloat("Speed_f", 0.2f);

    }

    private void PlayerPosY()
    {
        transform.position = new Vector3(transform.position.x, basePointAxisY + (float)(translateAxisY * GameManager.collectedStick), transform.position.z);
    }

    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z + (GameManager.collectedStick*5));
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);

            //Offset the camera behind the player by adding to the player's position.
            transform.position = Vector3.Lerp(startingPos, target, t);
            yield return 0;
        }

    }


}
