using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10f;
    private float rotationSpeed = 50f;
    private float horizontalInput;

    private float maximumRotationAngle = 30.0f;
    private float rangeX = 4.0f;
    private float growthRate = 0.003f;

    public bool isGameOver = false;
    public bool isReachBoss = false;
    public int playerHealth = 280;


    public static float punchStrength = 1f;
    public static int powerUpLevel = 1;
    public static float distanceLimit = 50f;
    public static int thrustLevel = 1;
    public static int powerUpPrice=1;
    public static int thrustPrice=1;

    public static int wallet = 1000;

    private float basePoint = 400f;
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


    // Start is called before the first frame update
    void Start()
    {
        TurnGreen();
        playerAnim = GetComponent<Animator>();
        enemyController = GameObject.Find("EnemyBoss").GetComponent<EnemyController>();
        //SpawnWall();
    }
    private void FixedUpdate()
    {
        if (!isGameOver && !isReachBoss)
        {
            TransformBound();
            RotationBound();
            Movement();
            SpawnWall();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        playerAnim.SetBool("Static_b", true);
        playerAnim.SetFloat("Speed_f", 1f);

        //This is where we get player input
        horizontalInput = Input.GetAxis("Horizontal");

        //Move to player forward.     
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //We turn the player.
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizontalInput);

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
    void RotationBound()
    {
        if (transform.rotation.y < -maximumRotationAngle)
        {
            transform.rotation = new Quaternion(transform.rotation.x, -maximumRotationAngle, transform.rotation.z, transform.rotation.w);
        }
        else if (transform.rotation.y > maximumRotationAngle)
        {
            transform.rotation = new Quaternion(transform.rotation.x, maximumRotationAngle, transform.rotation.z, transform.rotation.w);
        }


    }
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

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally"))
        {
            PeopleCounter(other.GetComponent<Renderer>().material.color);

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
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            isGameOver = true;
            particleDeath.Play();
            Destroy(gameObject, 3f);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyFist"))
        {
            playerHealth -= 40;
            if (playerHealth < 0)
            {
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                isGameOver = true;
                Destroy(gameObject, 3f);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Wall1") && playerPower >= 20)
        {
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall1") && playerPower >= 15 && playerPower < 20)
        {
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
            playerPower = 15;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall1") && playerPower < 15)
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            isGameOver = true;
            Destroy(gameObject, 3f);
            particleDeath.Play();
        }
    }
}
