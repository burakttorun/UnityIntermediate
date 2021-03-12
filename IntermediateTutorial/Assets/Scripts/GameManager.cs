using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController myPlayer = new PlayerController();
        // Properties(myPlayer);
        // TernaryOperator(myPlayer);
        // Statics();
        // MethodOverloading();
        // Lists();
        Dictionaries();
    }

    private static void Dictionaries()
    {
        //This is how you create a Dictionary. Notice how this takes
        //two generic terms. In this case you are using a string and a
        //BadGuy as your two values.
        Dictionary<string, BadGuy> badguys = new Dictionary<string, BadGuy>();

        BadGuy bg1 = new BadGuy("Harvey", 50);
        BadGuy bg2 = new BadGuy("Magneto", 100);

        //You can place variables into the Dictionary with the
        //Add() method.
        badguys.Add("gangster", bg1);
        badguys.Add("mutant", bg2);

        BadGuy magneto = badguys["mutant"];

        BadGuy temp = null;

        //This is a safer, but slow, method of accessing
        //values in a dictionary.
        if (badguys.TryGetValue("birds", out temp))
        {
            //success!
        }
        else
        {
            //failure!
        }
    }

    private static void Lists()
    {
        //This is how you create a list. Notice how the type
        //is specified in the angle brackets (< >).
        List<BadGuy> badguys = new List<BadGuy>();

        //Here you add 3 BadGuys to the List
        badguys.Add(new BadGuy("Harvey", 50));
        badguys.Add(new BadGuy("Magneto", 100));
        badguys.Add(new BadGuy("Pip", 5));

        badguys.Sort();

        foreach (BadGuy guy in badguys)
        {
            print(guy.name + " " + guy.power);
        }

        //This clears out the list so that it is
        //empty.
        badguys.Clear();
    }

    private static void Properties(PlayerController myPlayer)
    {
        myPlayer.Experience = 5;
        int x = myPlayer.Experience;
    }

    private static void MethodOverloading()
    {
        int y = Utilities.Add(3, 4);
        string z = Utilities.Add("Hello", "World");
    }

    private static void Statics()
    {
        EnemyController enemy1 = new EnemyController();
        EnemyController enemy2 = new EnemyController();
        EnemyController enemy3 = new EnemyController();

        //You can access a static variable by using the class name and the dot operator.
        int counter = EnemyController.enemyCount;

        //You can access a static method by using the class name
        //and the dot operator.
        int x = Utilities.Add(5, 6);
    }

    public void TernaryOperator(PlayerController myPlayer)
    {
        //This is an example Ternary Operation that chooses a message based on the variable "health".
        string message = myPlayer.Health > 0 ? "Player is Alive" : myPlayer.Health == 0 ? "Player is Barely Alive" : "Player is Dead";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Notice how you pass no parameter into this
            //extension method even though you had one in the
            //method declaration. The transform object that
            //this method is called from automatically gets
            //passed in as the first parameter.
            transform.ResetTransformation();

        }
        
    }
}
