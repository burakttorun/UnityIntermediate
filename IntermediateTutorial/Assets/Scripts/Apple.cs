﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the derived class whis is
//also know as the Child class.
public class Apple : Fruit
{
    //This is the first constructor for the Apple class.
    //It calls the parent constructor immediately, even
    //before it runs.
    public Apple()
    {
        //Notice how Apple has access to the public variable
        //color, which is a part of the parent Fruit class.
        color = "red";
        Debug.Log("1st Apple Constructor Called");
    }

    //This is the second constructor for the Apple class.
    //It specifies which parent constructor will be called
    //using the "base" keyword.
    public Apple(string newColor) : base(newColor)
    {
        //Notice how this constructor doesn't set the color
        //since the base constructor sets the color that
        //is passed as an argument.
        Debug.Log("2nd Apple Constructor Called");
    }
    public new void Chop()
    {
        Debug.Log("The apple has been chopped.");
    }

    public new void SayHello()
    {
        Debug.Log("Hello, I am an apple.");
    }

   //This hides the Fruit version.
    new public void Yell()
    {
        Debug.Log("Apple version of the Yell() method");
    }

    //These methods are overrides and therefore
    //can override any virtual methods in the parent
    //class.
    public override void Ripeness()
    {
        base.Ripeness();
        Debug.Log("This Apple is ripe.");
    }
}
