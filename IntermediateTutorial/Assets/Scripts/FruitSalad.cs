using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSalad : MonoBehaviour
{
    void Start()
    {
        // Inheritance();
        // Polymorphism();
        // MemberHiding();
        Overriding();
    }

    private static void Overriding()
    {
        Apple myApple = new Apple();

        //Notice that the Apple version of the methods
        //override the fruit versions. Also notice that
        //since the Apple versions call the Fruit version with
        //the "base" keyword, both are called.
        myApple.SayHello();
        myApple.Ripeness();

        //Overriding is also useful in a polymorphic situation.
        //Since the methods of the Fruit class are "virtual" and
        //the methods of the Apple class are "override", when we 
        //upcast an Apple into a Fruit, the Apple version of the 
        //Methods are used.
        Fruit myFruit = new Apple();
        myFruit.SayHello();
        myFruit.Ripeness();
    }

    private static void MemberHiding()
    {
        Fruit fruit = new Fruit();
        Apple apple = new Apple();
        //Notice how each Humanoid variable contains
        //a reference to a different class in the
        //inheritance hierarchy, yet each of them
        //calls the Humanoid Yell() method.
        fruit.Yell();
        apple.Yell();
    }

    private static void Polymorphism()
    {
        /*Notice here how the variable "myFruit" is of type
        Fruit but is being assigned a reference to an Apple. This
        works because of Polymorphism. Since an Apple is a Fruit,
        this works just fine. While the Apple reference is stored
        in a Fruit variable, it can only be used like a Fruit*/
        Fruit myFruit = new Apple();

        myFruit.SayHello();
        myFruit.Chop();

        //This is called downcasting. The variable "myFruit" which is 
        //of type Fruit, actually contains a reference to an Apple. Therefore,
        //it can safely be turned back into an Apple variable. This allows
        //it to be used like an Apple, where before it could only be used
        //like a Fruit.
        Apple myApple = (Apple)myFruit;

        myApple.SayHello();
        myApple.Chop();
    }

    private static void Inheritance()
    {
        //Let's illustrate inheritance with the 
        //default constructors.
        Debug.Log("Creating the fruit");
        Fruit myFruit = new Fruit();
        Debug.Log("Creating the apple");
        Apple myApple = new Apple();

        //Call the methods of the Fruit class.
        myFruit.SayHello();
        myFruit.Chop();

        //Call the methods of the Apple class.
        //Notice how class Apple has access to all
        //of the public methods of class Fruit.
        myApple.SayHello();
        myApple.Chop();

        //Now let's illustrate inheritance with the 
        //constructors that read in a string.
        Debug.Log("Creating the fruit");
        myFruit = new Fruit("yellow");
        Debug.Log("Creating the apple");
        myApple = new Apple("green");

        //Call the methods of the Fruit class.
        myFruit.SayHello();
        myFruit.Chop();

        //Call the methods of the Apple class.
        //Notice how class Apple has access to all
        //of the public methods of class Fruit.
        myApple.SayHello();
        myApple.Chop();
    }
}