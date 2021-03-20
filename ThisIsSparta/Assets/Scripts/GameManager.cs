using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Camera[] cameras;

    PlayerController playerController;
    EnemyController enemyController;
    [SerializeField]
    TextMeshProUGUI humanCounter;
    [SerializeField]
    TextMeshProUGUI diamondCounter;
    [SerializeField]
    TextMeshProUGUI walletCounter;
    [SerializeField]
    TextMeshProUGUI powerUpPrice;
    [SerializeField]
    TextMeshProUGUI thrustPrice;
    [SerializeField]
    TextMeshProUGUI scoreValue;
    [SerializeField]
    TextMeshProUGUI highScoreValue;

    [SerializeField]
    Button powerUpLevel;
    [SerializeField]
    Button thrustLevel;

    [SerializeField]
    GameObject gameOver; 
    [SerializeField]
    GameObject preGame;
    [SerializeField]
    GameObject lastGame;
    [SerializeField]
    GameObject middleGame;


    [SerializeField]
    AudioClip clapSound;
    
    public AudioClip booSound;
    AudioSource audioSource;

    public GameObject scoreObj;
    
    public GameObject highScoreObj;
    
    public GameObject distanceMeter;

    public static bool isGameActive=false;

    public float score;
    public static float highScore;
    private bool clapSoundIsPlaying=false;

    public ProgressBar PlayerBar;

    public ProgressBar EnemyBar;

    RectTransform rt;

    private void Awake()
    {
       // ReadingAttribute();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyController = GameObject.Find("EnemyBoss").GetComponent<EnemyController>();
        audioSource = GetComponent<AudioSource>();
        rt = middleGame.GetComponent<RectTransform>();
        ReadingAttribute();

        Debug.Log(highScore);
    }
    private void Update()
    {
        
        if (playerController.isReachBoss)
        {
            cameras[0].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(true);
        }

        humanCounter.text = playerController.numberOfPeople.ToString();
        diamondCounter.text = playerController.diamond.ToString();
        walletCounter.text = PlayerController.wallet.ToString();
        powerUpLevel.GetComponentInChildren<Text>().text = "Level " + PlayerController.powerUpLevel.ToString();
        thrustLevel.GetComponentInChildren<Text>().text = "Level " + PlayerController.thrustLevel.ToString();
        powerUpPrice.text = PlayerController.powerUpPrice.ToString();
        thrustPrice.text = PlayerController.thrustPrice.ToString();

        if (Input.GetMouseButtonDown(2))
        {
            StartGame();
        }

        if (enemyController.enemyHealth <= 0)
        {
            StartCoroutine(EndGameCanvas());
            playerController.MakeScore();
            distanceMeter.SetActive(true);
            if (!clapSoundIsPlaying)
            {
                clapSoundIsPlaying = true;
                audioSource.PlayOneShot(clapSound);
            }
            
        }
        scoreValue.text = Mathf.FloorToInt(score).ToString()+" meter";
        highScoreValue.text = Mathf.FloorToInt(highScore).ToString()+" meter";
        PlayerBar.BarValue = playerController.playerHealth;
        EnemyBar.BarValue = enemyController.enemyHealth;

        if (playerController.isReachBoss)
        {

            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rt.rect.width);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rt.rect.height);
        }
        SaveAttribute();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        isGameActive = false;
    }

    public void StartGame()
    {
        isGameActive = true;
        preGame.SetActive(false);
    }

    IEnumerator EndGameCanvas()
    {
        yield return new WaitForSeconds(4.5f);
        lastGame.SetActive(true);
    }

    void SaveAttribute()
    {
        PlayerPrefs.SetInt("wallet", PlayerController.wallet);
        PlayerPrefs.SetInt("powerUpLevel", PlayerController.powerUpLevel);
        PlayerPrefs.SetInt("thrustLevel", PlayerController.thrustLevel);
        PlayerPrefs.SetInt("powerUpPrice", PlayerController.powerUpPrice);
        PlayerPrefs.SetInt("thrustPrice", PlayerController.thrustPrice);
        PlayerPrefs.SetFloat("highScore", highScore);

        PlayerPrefs.Save();
    }

    void ReadingAttribute()
    {
        PlayerController.wallet = PlayerPrefs.GetInt("wallet");
        PlayerController.powerUpLevel = PlayerPrefs.GetInt("powerUpLevel");
        PlayerController.thrustLevel = PlayerPrefs.GetInt("thrustLevel");
        PlayerController.powerUpPrice = PlayerPrefs.GetInt("powerUpPrice");
        PlayerController.thrustPrice = PlayerPrefs.GetInt("thrustPrice");
        highScore = PlayerPrefs.GetFloat("highScore");
    }

}
