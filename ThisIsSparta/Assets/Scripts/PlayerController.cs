using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float speed = 10f;
    private float rotationSpeed = 0.1f;
    //private float horizontalInput;
    //private float maximumRotationAngle = 30.0f;
    private float rangeX = 4.0f;
    private float growthRate = 0.003f;

    public bool isGameOver = false;
    public bool isReachBoss = false;
    public int playerHealth = 280;


    public static float punchStrength = 1f;
    public static int powerUpLevel = 1;
    public static float distanceLimit = 50f;
    public static int thrustLevel = 1;
    public static int powerUpPrice = 1;
    public static int thrustPrice = 1;

    public static int wallet = 0;

    private float basePoint = 378f;
    [SerializeField]
    GameObject wallPref;

    [SerializeField]
    public int diamond = 0;
    [SerializeField]
    public int numberOfPeople = 0;

    public int playerPower = 1;
    [SerializeField]
    ParticleSystem particleExplosion;

    [SerializeField]
    ParticleSystem particleDeath;


    Animator playerAnim;

    EnemyController enemyController;
    GameManager gameManager;

    [SerializeField]
    AudioClip boomSound;
    [SerializeField]
    AudioClip collectSound;
    AudioSource audioSource;
    private Touch touch;


    // Start is called before the first frame update
    void Start()
    {
        TurnGreen();
        playerAnim = GetComponent<Animator>();
        enemyController = GameObject.Find("EnemyBoss").GetComponent<EnemyController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        //SpawnWall();

    }
    private void FixedUpdate()
    {
        if (GameManager.isGameActive && !isReachBoss)
        {
            TransformBound();
            //RotationBound();
            Movement();
            SpawnWall();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isReachBoss)
        {
            playerAnim.SetInteger("WeaponType_int", 10);
            if (enemyController.enemyHealth <= 50)
            {
                particleExplosion.Play();
            }

        }
        else
            playerAnim.SetInteger("WeaponType_int", 0);

        if (enemyController.enemyHealth < 0)
        {
            playerAnim.SetInteger("WeaponType_int", 0);
            playerAnim.SetInteger("Animation_int", 6);
        }
    }

    void SpawnWall()
    {
        wallPref.transform.position = new Vector3(wallPref.transform.position.x, wallPref.transform.position.y, basePoint + distanceLimit);
    }

    public void UpgradeDistance()
    {

        if (wallet >= thrustPrice)
        {
            wallet -= thrustPrice;
            thrustPrice = (int)Mathf.Pow(2, thrustLevel);
            distanceLimit += 25f;
            thrustLevel++;

        }

    }
    public void PowerUp()
    {

        if (wallet >= powerUpPrice)
        {
            wallet -= powerUpPrice;
            powerUpPrice = (int)Mathf.Pow(2, powerUpLevel);
            punchStrength += 0.25f;
            powerUpLevel++;

        }

    }
    public void MakeMoney()
    {
        wallet += diamond;
        diamond = 0;
        GameManager.isGameActive = false;
        gameManager.RestartGame();

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
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                transform.Rotate(transform.rotation.x, transform.rotation.y + touch.deltaPosition.x * rotationSpeed, transform.rotation.z);
                //transform.rotation = new Quaternion(transform.rotation.x , transform.rotation.y + touch.azimuthAngle * rotationSpeed, transform.rotation.z, transform.rotation.w);
                //This is where we get player input
               // horizontalInput = Input.GetAxis("Horizontal");
                //We turn the player.
                //transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizontalInput);
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
   /* void RotationBound()
    {
        if (transform.rotation.y < -maximumRotationAngle)
        {
            transform.rotation = new Quaternion(transform.rotation.x, -maximumRotationAngle, transform.rotation.z, transform.rotation.w);
        }
        else if (transform.rotation.y > maximumRotationAngle)
        {
            transform.rotation = new Quaternion(transform.rotation.x, maximumRotationAngle, transform.rotation.z, transform.rotation.w);
        }


    }*/

    void PeopleCounter(Color other)
    {
        if (GetComponent<Renderer>().material.color.Equals(other))
        {
            numberOfPeople++;
            playerPower++;
            SizeUp();

        }
        else
           if (playerPower > 1)
        {
            SizeDown();
            playerPower--;
        }

    }
    void SizeUp()
    {
        transform.localScale += Vector3.one * (playerPower * growthRate);
    }
    void SizeDown()
    {
        transform.localScale -= Vector3.one * (playerPower * growthRate);
    }

    private void PlayerDeath()
    {
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        gameManager.GameOver();
        particleDeath.Play();
        audioSource.PlayOneShot(boomSound);
        audioSource.PlayOneShot(gameManager.booSound);
        StartCoroutine(MakeInvisible());
    }

    public void MakeScore()
    {
        gameManager.score = enemyController.transform.position.z - basePoint;
        if (gameManager.score > GameManager.highScore)
        {
            GameManager.highScore = gameManager.score;
            gameManager.highScoreObj.SetActive(true);
        }
        gameManager.scoreObj.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally"))
        {
            PeopleCounter(other.GetComponent<Renderer>().material.color);
            audioSource.PlayOneShot(collectSound);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("TeleporterRed"))
        {
            TurnRed();
        }
        if (other.CompareTag("TeleporterGreen"))
        {
            TurnGreen();
        }

        if (other.CompareTag("Diamond"))
        {
            diamond++;
            audioSource.PlayOneShot(collectSound);
            Destroy(other.gameObject);
        }


        if (other.CompareTag("Arena"))
        {
            speed = 0;
            playerAnim.SetFloat("Speed_f", 0f);
            isReachBoss = true;
        }

        if (other.CompareTag("Obstacle"))
        {
            PlayerDeath();
        }

    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyFist"))
        {
            playerHealth -= 40;
            if (playerHealth < 0)
            {
                PlayerDeath();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Wall1") && playerPower >= 20)
        {
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
            playerPower = 20;
            Destroy(collision.gameObject);
        }
       
        if (collision.gameObject.CompareTag("Wall1") && playerPower < 20)
        {
            PlayerDeath();
        }
    }

    IEnumerator MakeInvisible()
    {
        yield return new WaitForSeconds(4.0f);
        gameObject.SetActive(false);
    }

   
}
