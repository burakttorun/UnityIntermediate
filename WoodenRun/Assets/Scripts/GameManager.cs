using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int collectedStick = 0;
    public static bool isGameActive = true;
    public int gold = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedStick < 0)
        {
            isGameActive = false;
        }
       // Debug.Log(collectedStick);

    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
