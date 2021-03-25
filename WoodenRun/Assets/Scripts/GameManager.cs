using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerController playerController;
    public static int collectedStick = 0;
    public static bool isGameActive = false;
    public int gold = 0;

    [SerializeField]
    AudioClip clapSound;
    [SerializeField]
     AudioClip booSound;
    AudioSource audioSource;
    private bool booSoundIsPlaying = false;
    private bool clapSoundIsPlaying = false;


    [SerializeField]
    TextMeshProUGUI diamondCounter;
    [SerializeField]
    TextMeshProUGUI walletCounter;
    [SerializeField]
    TextMeshProUGUI scoreValue;
   

    [SerializeField]
    GameObject gameOver;
    [SerializeField]
    GameObject preGame;
    [SerializeField]
    GameObject lastGame;
    private static int wallet;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        ReadingAttribute();
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedStick < 0)
        {
            GameOver();
        }
        // Debug.Log(collectedStick);
        if (playerController.playerIsJumped && !clapSoundIsPlaying)
        {
            clapSoundIsPlaying = true;
            audioSource.PlayOneShot(clapSound);
            StartCoroutine(EndGameCanvas());
        }
        ExportToUI();
    }
    public void MakeMoney()
    {
        wallet += gold*collectedStick;
        gold = 0;
        SaveAttribute();
        GameManager.isGameActive = false;
        RestartGame();

    }
    public void GameOver()
    {
        isGameActive = false;
        if (!booSoundIsPlaying)
        {
            booSoundIsPlaying = true;
            audioSource.PlayOneShot(booSound);
        }
        gameOver.SetActive(true);
        collectedStick = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        collectedStick = 0;
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

    void ExportToUI()
    {
        diamondCounter.text = gold.ToString();
        walletCounter.text = wallet.ToString();       
    }

    void SaveAttribute()
    {
        PlayerPrefs.SetInt("wallet", wallet);
        PlayerPrefs.Save();
    }

    void ReadingAttribute()
    {
        wallet = PlayerPrefs.GetInt("wallet");      
    }

}
