using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the base class which is
//also known as the Parent class.
public class Fruit
{
    public string color;

    //This is the first constructor for the Fruit class
    //and is not inherited by any derived classes.
    public Fruit()
    {
        color = "orange";
        Debug.Log("1st Fruit Constructor Called");
    }

    //This is the second constructor for the Fruit class
    //and is not inherited by any derived classes.
    public Fruit(string newColor)
    {
        color = newColor;
        Debug.Log("2nd Fruit Constructor Called");
    }

    public void Chop()
    {
        Debug.Log("The " + color + " fruit has been chopped.");
    }

    public void SayHello()
    {
        Debug.Log("Hello, I am a fruit.");
    }
    //Base version of the Yell method
    public void Yell()
    {
        Debug.Log("Fruit version of the Yell() method");
    }

    //These methods are virtual and thus can be overriden
    //in child classes
    public virtual void Ripeness()
    {
        Debug.Log("This Fruit is ripe.");
    }
}
