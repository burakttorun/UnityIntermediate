using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController playerController;
    public static int collectedStick = 0;
    public static bool isGameActive = true;
    public int gold = 0;

    [SerializeField]
    AudioClip clapSound;
    [SerializeField]
     AudioClip booSound;
    AudioSource audioSource;
    private bool booSoundIsPlaying = false;
    private bool clapSoundIsPlaying = false;

   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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
        }

    }

    public void GameOver()
    {
        isGameActive = false;
        if (!booSoundIsPlaying)
        {
            booSoundIsPlaying = true;
            audioSource.PlayOneShot(booSound);
        }
       
        collectedStick = 0;
    }

   
}
