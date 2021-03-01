using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController myPlayer = new PlayerController();
        myPlayer.Experience = 5;
        int x = myPlayer.Experience;
        TernaryOperator(myPlayer);
    }

     public void TernaryOperator(PlayerController myPlayer)
    {
        //This is an example Ternary Operation that chooses a message based on the variable "health".
        string message = myPlayer.Health > 0 ? "Player is Alive" : myPlayer.Health == 0 ? "Player is Barely Alive" : "Player is Dead";
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
